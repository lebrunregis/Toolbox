using PlayerStateAnimator.Data;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
namespace PlayerStateAnimator.Runtime
{
    public class LadderClimbingState : ICharacterState
    {
        private readonly PlayerStateController controller;
        private PlayableGraph graph;
        private AnimationLayerMixerPlayable layerMixer;
        private AnimationMixerPlayable ladderMixer;
        private AnimationClipPlayable breathingPlayable;
        private float breathingWeight = 0.2f;

        public LadderClimbingState(PlayerStateController controller, AnimationClip[] ladderClips, AnimationClip breathingClip)
        {
            this.controller = controller;

            var animator = controller.GetComponent<Animator>();
            graph = PlayableGraph.Create("LadderClimbWithBreathing");
            graph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);

            var output = AnimationPlayableOutput.Create(graph, "AnimOut", animator);

            // Layer mixer with two layers
            layerMixer = AnimationLayerMixerPlayable.Create(graph, 2);
            output.SetSourcePlayable(layerMixer);

            // Layer 0: ladder climb blend
            ladderMixer = AnimationMixerPlayable.Create(graph, ladderClips.Length);
            for (int i = 0; i < ladderClips.Length; i++)
            {
                var clipPlayable = AnimationClipPlayable.Create(graph, ladderClips[i]);
                graph.Connect(clipPlayable, 0, ladderMixer, i);
                ladderMixer.SetInputWeight(i, 0f);
            }
            graph.Connect(ladderMixer, 0, layerMixer, 0);
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
            // Blend ladder directions based on input
            float vertical = Input.GetAxis("Vertical");
            int directionIndex = vertical > 0 ? 0 : 1; // Up or Down
            ladderMixer.SetInputWeight(directionIndex, 1f);
            ladderMixer.SetInputWeight((directionIndex + 1) % ladderMixer.GetInputCount(), 0f);

            // Adjust breathing based on speed
            breathingWeight = Mathf.Lerp(0.5f, 0.1f, Mathf.Abs(vertical));
            layerMixer.SetInputWeight(1, breathingWeight);
        }

        public void Exit()
        {
        }
    }

}
