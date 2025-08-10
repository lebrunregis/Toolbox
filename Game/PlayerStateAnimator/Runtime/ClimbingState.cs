using PlayerStateAnimator.Data;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
namespace PlayerStateAnimator.Runtime
{
    public class ClimbingState : ICharacterState
    {
        private readonly PlayerStateController controller;
        private PlayableGraph graph;
        private AnimationLayerMixerPlayable layerMixer;
        private AnimationMixerPlayable moveMixer;
        private AnimationClipPlayable breathingPlayable;
        private float breathingWeight = 0.2f;

        public ClimbingState(PlayerStateController controller, AnimationClip[] moveClips, AnimationClip breathingClip)
        {
            this.controller = controller;

            var animator = controller.GetComponent<Animator>();
            graph = PlayableGraph.Create("ClimbWithBreathing");
            graph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);

            var output = AnimationPlayableOutput.Create(graph, "AnimOut", animator);

            // Layer mixer with two layers
            layerMixer = AnimationLayerMixerPlayable.Create(graph, 2);
            output.SetSourcePlayable(layerMixer);

            // Layer 0: 4-way climb blend
            moveMixer = AnimationMixerPlayable.Create(graph, moveClips.Length);
            for (int i = 0; i < moveClips.Length; i++)
            {
                var clipPlayable = AnimationClipPlayable.Create(graph, moveClips[i]);
                graph.Connect(clipPlayable, 0, moveMixer, i);
                moveMixer.SetInputWeight(i, 0f);
            }
            graph.Connect(moveMixer, 0, layerMixer, 0);
            layerMixer.SetInputWeight(0, 1f);

            // Layer 1: breathing overlay
            breathingPlayable = AnimationClipPlayable.Create(graph, breathingClip);
            graph.Connect(breathingPlayable, 0, layerMixer, 1);
            layerMixer.SetLayerAdditive(1, true);
            layerMixer.SetInputWeight(1, breathingWeight);

            graph.Play();
        }

        public void Enter() { }

        public void Update()
        {
            // Blend climb directions based on input
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            float speed = Mathf.Sqrt(horizontal * horizontal + vertical * vertical);
            int directionIndex = GetClimbDirectionIndex(horizontal, vertical);
            moveMixer.SetInputWeight(directionIndex, 1f);
            moveMixer.SetInputWeight((directionIndex + 1) % moveMixer.GetInputCount(), 0f);

            // Adjust breathing based on speed
            breathingWeight = Mathf.Lerp(0.5f, 0.1f, speed);
            layerMixer.SetInputWeight(1, breathingWeight);
        }

        public void Exit()
        {
            graph.Destroy();
        }

        private int GetClimbDirectionIndex(float horizontal, float vertical)
        {
            if (Mathf.Abs(horizontal) > Mathf.Abs(vertical))
            {
                return horizontal > 0 ? 1 : 3; // Right or Left
            }
            else
            {
                return vertical > 0 ? 0 : 2; // Up or Down
            }
        }
    }

}
