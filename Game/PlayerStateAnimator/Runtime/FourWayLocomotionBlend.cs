using PlayerStateAnimator.Data;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
namespace PlayerStateAnimator.Runtime
{
    [RequireComponent(typeof(Animator))]
    public class FourWayLocomotionBlend : ICharacterState
    {
        private readonly PlayerStateController controller;
        public AnimationClip idle;
        public AnimationClip walkForward;
        public AnimationClip walkBackward;
        public AnimationClip walkLeft;
        public AnimationClip walkRight;
        public AnimationClip runForward;
        public AnimationClip runBackward;
        public AnimationClip runLeft;
        public AnimationClip runRight;

        private PlayableGraph playableGraph;
        private AnimationMixerPlayable mixer;
        private AnimationLayerMixerPlayable layerMixer;
        private readonly float breathingWeight = 0.2f;
        private enum ClipIndex
        {
            Idle = 0,
            WalkForward = 1,
            WalkBackward = 2,
            WalkLeft = 3,
            WalkRight = 4,
            RunForward = 5,
            RunBackward = 6,
            RunLeft = 7,
            RunRight = 8,
        }

        public FourWayLocomotionBlend(PlayerStateController controller, AnimationClip[] ladderClips, AnimationClip breathingClip)
        {
            this.controller = controller;
            playableGraph = PlayableGraph.Create("FourWayLocomotionBlend");
            playableGraph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);

            AnimationPlayableOutput output = AnimationPlayableOutput.Create(playableGraph, "Animation", controller.GetComponent<Animator>());
            // Layer 0: walk/run blend
            AnimationClip[] src = { idle, walkForward, walkBackward, walkLeft, walkRight, runForward, runBackward, runLeft, runRight };
            mixer = AnimationMixerPlayable.Create(playableGraph, src.Length);
            output.SetSourcePlayable(mixer);
            for (int i = 0; i < src.Length; i++)
            {
                var clipPlayable = AnimationClipPlayable.Create(playableGraph, src[i]);
                playableGraph.Connect(clipPlayable, 0, mixer, i);
                mixer.SetInputWeight(i, 0f);
            }

            // Layer 1: breathing overlay
            AnimationClipPlayable breathingPlayable = AnimationClipPlayable.Create(playableGraph, breathingClip);
            playableGraph.Connect(breathingPlayable, 0, layerMixer, 1);

            layerMixer.SetLayerAdditive(1, true); // additive overlay
            layerMixer.SetInputWeight(1, breathingWeight);
            playableGraph.Play();
        }

        public void Update()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            float forward = Mathf.Max(0f, v);
            float backward = Mathf.Max(0f, -v);
            float right = Mathf.Max(0f, h);
            float left = Mathf.Max(0f, -h);

            float magnitude = Mathf.Sqrt(forward * forward + backward * backward + right * right + left * left);

            float normalized = Mathf.Clamp01(magnitude);

            float walkWeight = 1f - normalized;
            float runWeight = normalized;

            // Reset all weights
            for (int i = 0; i <= (int)ClipIndex.RunRight; i++)
            {
                mixer.SetInputWeight(i, 0f);
            }

            if (magnitude > 0f)
            {
                // Blend each direction between walk and run using normalized magnitude
                mixer.SetInputWeight((int)ClipIndex.WalkForward, forward * walkWeight);
                mixer.SetInputWeight((int)ClipIndex.RunForward, forward * runWeight);

                mixer.SetInputWeight((int)ClipIndex.WalkBackward, backward * walkWeight);
                mixer.SetInputWeight((int)ClipIndex.RunBackward, backward * runWeight);

                mixer.SetInputWeight((int)ClipIndex.WalkLeft, left * walkWeight);
                mixer.SetInputWeight((int)ClipIndex.RunLeft, left * runWeight);

                mixer.SetInputWeight((int)ClipIndex.WalkRight, right * walkWeight);
                mixer.SetInputWeight((int)ClipIndex.RunRight, right * runWeight);
            }
            else
            {
                mixer.SetInputWeight((int)ClipIndex.Idle, 1f);
            }
        }


        public void Enter()
        {

        }

        public void Exit()
        {

        }
    }
}
