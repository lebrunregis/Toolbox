using DebugBehaviour.Runtime;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ActiveRagdoll.Runtime
{
    public class ActiveRagdoll : VerboseMonoBehaviour
    {
        public int solverIterations = 8;
        public int solverVelocityIterations = 8;
        public float maxAngularVelocity = 20f;
        public GameObject animatedBody;
        public GameObject physicsBody;
        private readonly Dictionary<String, Transform> animatedTransformsDictionary = new();
        private readonly Dictionary<String, ConfigurableJoint> configurableJointDictionary = new();
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {

            SetupAnimatedBody();
            SetupPhysicsBody();
            SetupJoints();
        }

        private void Update()
        {
            foreach (ConfigurableJoint j in configurableJointDictionary.Values)
            {
                j.targetRotation = transform.rotation;
            }
        }

        private void SetupJoints()
        {
            Transform[] animatedTransforms = animatedBody.GetComponentsInChildren<Transform>();
            Transform[] physicsTransforms = physicsBody.GetComponentsInChildren<Transform>();
           
            foreach (Transform t in animatedTransforms)
            {
                animatedTransformsDictionary.Add(t.gameObject.name, t);
            }
            ConfigurableJoint joint;
            foreach (Transform t in physicsTransforms)
            {
                GameObject gameObject = t.gameObject;
                if (animatedTransformsDictionary.ContainsKey(gameObject.name))
                {
                    joint = gameObject.AddComponent<ConfigurableJoint>();
                    configurableJointDictionary.Add(gameObject.name, joint);
                }
            }
        }

        private void SetupAnimatedBody()
        {
            Animator animator = animatedBody.GetComponentInChildren<Animator>();
            if (animator != null)
            {
                animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
            }
            MeshRenderer renderer = animatedBody.GetComponentInChildren<MeshRenderer>();
            if (renderer != null)
            {
                renderer.enabled = false;
            }

        }

        private void SetupPhysicsBody()
        {
            Rigidbody[] rigidbodies = physicsBody.GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody rb in rigidbodies)
            {
                rb.solverIterations = solverIterations;
                rb.solverVelocityIterations = solverVelocityIterations;
                rb.maxAngularVelocity = maxAngularVelocity;
            }
        }

    }
}
