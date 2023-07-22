using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ControlStateManager 
{
    public enum MainState
    {
        NONE,
        BASE,
        ARM
    }

    public enum ArmState
    {
        NONE,
        JOINT1,
        JOINT2,
        JOINT3,
        JOINT4,
        JOINT5,
        JOINT6
    }

    public static MainState ActualMainState;
    public static ArmState ActualArmState;

}
