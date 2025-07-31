using UnityEngine;

namespace ActiveRagdoll.Runtime
{
    public class ConfigurableJointExtended : ConfigurableJoint
    {

        // Based on https://gist.github.com/mstevenson/7b85893e8caf5ca034e6


        /// <summary>
        /// Sets a joint's targetRotation to match a given local rotation.
        /// The joint transform's local rotation must be cached on Start and passed into this method.
        /// </summary>
        public void SetTargetRotationLocal(Quaternion targetLocalRotation, Quaternion startLocalRotation)
        {
            if (configuredInWorldSpace)
            {
                Debug.LogError("SetTargetRotationLocal should not be used with joints that are configured in world space. For world space joints, use SetTargetRotation.", this);
            }
            SetTargetRotationInternal(targetLocalRotation, startLocalRotation, Space.Self);
        }

        /// <summary>
        /// Sets a joint's targetRotation to match a given world rotation.
        /// The joint transform's world rotation must be cached on Start and passed into this method.
        /// </summary>
        public void SetTargetRotation(Quaternion targetWorldRotation, Quaternion startWorldRotation)
        {
            if (!configuredInWorldSpace)
            {
                Debug.LogError("SetTargetRotation must be used with joints that are configured in world space. For local space joints, use SetTargetRotationLocal.", this);
            }
            SetTargetRotationInternal(targetWorldRotation, startWorldRotation, Space.World);
        }

         void SetTargetRotationInternal(Quaternion newTargetRotation, Quaternion startRotation, Space space)
        {
            // Calculate the rotation expressed by the joint's axis and secondary axis
            var right = axis;
            var forward = Vector3.Cross(axis, secondaryAxis).normalized;
            var up = Vector3.Cross(forward, right).normalized;
            Quaternion worldToJointSpace = Quaternion.LookRotation(forward, up);

            // Transform into world space
            Quaternion resultRotation = Quaternion.Inverse(worldToJointSpace);

            // Counter-rotate and apply the new local rotation.
            // Joint space is the inverse of world space, so we need to invert our value
            if (space == Space.World)
            {
                resultRotation *= startRotation * Quaternion.Inverse(newTargetRotation);
            }
            else
            {
                resultRotation *= Quaternion.Inverse(newTargetRotation) * startRotation;
            }

            // Transform back into joint space
            resultRotation *= worldToJointSpace;

            // Set target rotation to our newly calculated rotation
            targetRotation = resultRotation;
        }

        /// <summary>
        /// Adjust ConfigurableJoint settings to closely match CharacterJoint behaviour
        /// </summary>
        public void SetupAsCharacterJoint()
        {
            xMotion = ConfigurableJointMotion.Locked;
            yMotion = ConfigurableJointMotion.Locked;
            zMotion = ConfigurableJointMotion.Locked;
            angularXMotion = ConfigurableJointMotion.Limited;
            angularYMotion = ConfigurableJointMotion.Limited;
            angularZMotion = ConfigurableJointMotion.Limited;
            breakForce = Mathf.Infinity;
            breakTorque = Mathf.Infinity;
            rotationDriveMode = RotationDriveMode.Slerp;
            JointDrive jointDrive = slerpDrive;
            //jointDrive.mode= JointDriveMode.Position;
            jointDrive.maximumForce = Mathf.Infinity;
            slerpDrive = jointDrive;
        }
    }
}
