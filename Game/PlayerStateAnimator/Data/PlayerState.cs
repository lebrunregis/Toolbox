
using System;

namespace PlayerStateAnimator.Runtime
{
    [Flags]
    public enum PlayerState
    {
        Idle = 1 << 0,
        FreeLocomotion = 1 << 1,
        CombatLocomotion = 1 << 2,
        WallClimbing = 1 << 3,
        LadderClimbing = 1 << 4,
        Swimming = 1 << 5,
    }
}
