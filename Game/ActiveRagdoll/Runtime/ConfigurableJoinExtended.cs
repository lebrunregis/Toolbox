using UnityEngine;

namespace ActiveRagdoll.Runtime
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(ConfigurableJoint))]
    public class ConfigurableJoinExtended : MonoBehaviour
    {
        private Rigidbody connectedBody;
        private ConfigurableJoint joint;
        private Quaternion initialLocalRotation;
        private GameObject target;
        public float positionSpring = 1000f;
        public float positionDamper = 10f;
        public float maximumForce = 50f;
        public float jointLimit = 45f;
        public float jointBounciness = 90f;

        public void Initialize(GameObject targetObject, Rigidbody parent)
        {
            target = targetObject;
            connectedBody = parent;

            // Store the local rotation difference between this joint and the target at start
            initialLocalRotation = Quaternion.Inverse(transform.localRotation) * target.transform.localRotation;
        }

        private void Start()
        {
            joint = GetComponent<ConfigurableJoint>();

            if (joint == null)
                joint = gameObject.AddComponent<ConfigurableJoint>();

            // Set up the joint like a character joint
            ConfigurableJointExtensions.SetupAsCharacterJoint(joint);
            joint.connectedBody = connectedBody;

            // Set drive settings
            JointDrive drive = new()
            {
                positionSpring = positionSpring,
                positionDamper = 10f,
                maximumForce = 50f
            };

            joint.rotationDriveMode = RotationDriveMode.Slerp;
            joint.slerpDrive = drive;

            // Optional: limit settings if needed
            SoftJointLimit softJointLimit = new()
            {
                limit = jointLimit,
                bounciness = jointBounciness
            };

            joint.angularYLimit = softJointLimit;
            joint.angularZLimit = softJointLimit;
        }

        private void Update()
        {
            // Get the target rotation in joint local space
            Quaternion targetLocalRotation = Quaternion.Inverse(transform.parent.rotation) * target.transform.rotation;

            // Apply offset from the initial local rotation difference
            Quaternion targetJointRotation = targetLocalRotation * Quaternion.Inverse(initialLocalRotation);

            // Apply the rotation to the joint
            ConfigurableJointExtensions.SetTargetRotationLocal(joint, targetJointRotation, initialLocalRotation);
        }

    }
}
