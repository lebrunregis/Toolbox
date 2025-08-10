using PlayerStateAnimator.Data;
using UnityEngine;
// Main controller
namespace PlayerStateAnimator.Runtime
{
    public class PlayerStateController : MonoBehaviour
    {
        public PlayerState CurrentState { get; private set; }
        private ICharacterState currentStateInstance;
        public FourWayLocomotionBlend FreeLocomotion;
        public ClimbingState WallClimbing;
        public LadderClimbingState LadderClimbing;

        // Animation clips
        public AnimationClip[] runClips;
        public AnimationClip[] climbClips;
        public AnimationClip[] ladderClimbClips;
        public AnimationClip breathingClip;
        public void TransitionTo(PlayerState newState)
        {
            if (CanTransition(newState))
            {
                CurrentState = newState;
                currentStateInstance?.Exit();
                currentStateInstance = GetStateInstance(newState);
                currentStateInstance.Enter();
            }
        }

        private ICharacterState GetStateInstance(PlayerState newState)
        {
            return newState switch
            {
                PlayerState.FreeLocomotion => FreeLocomotion,
                PlayerState.WallClimbing => WallClimbing,
                PlayerState.LadderClimbing => LadderClimbing,
                _ => FreeLocomotion,
            };
        }

        private bool CanTransition(PlayerState newState)
        {
            // Define allowed transitions here
            return CurrentState switch
            {
                PlayerState.FreeLocomotion => newState == PlayerState.WallClimbing || newState == PlayerState.LadderClimbing,
                PlayerState.WallClimbing => newState == PlayerState.FreeLocomotion,
                PlayerState.LadderClimbing => newState == PlayerState.FreeLocomotion,
                _ => false,
            };
        }


        private void Start()
        {
            FreeLocomotion = new FourWayLocomotionBlend(this, runClips, breathingClip);
            WallClimbing = new ClimbingState(this, climbClips, breathingClip);
            LadderClimbing = new LadderClimbingState(this, ladderClimbClips, breathingClip); ;
            TransitionTo(FreeLocomotion);
        }

        private void Update()
        {
            currentStateInstance?.Update();
        }

        public void TransitionTo(ICharacterState newState)
        {
            currentStateInstance?.Exit();
            currentStateInstance = newState;
            currentStateInstance.Enter();
        }
    }
}
