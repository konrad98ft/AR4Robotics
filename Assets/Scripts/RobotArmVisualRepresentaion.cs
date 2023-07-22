using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotArmVisualRepresentaion : MonoBehaviour
{
    public delegate void ArmMove(float increment);
    public static ArmMove ArmMoveDelegate;

    private List<GameObject> robotArmJointsList;

    private const int JOINTS_NUMBER = 6;
    public Quaternion[] InitialJointsRotations = new Quaternion[JOINTS_NUMBER];
    private int[] AxesDirection = { -1, -1, 1, -1, 1, -1 };

    private float increment = 0.0f;

    private float[] JointMinAngles = { -100, -100, -100, -100, 100, -100 };
    private float[] JointMaxAngles = { 100, 100, 100, 100, 100, 100 };
    private List<float> startAngles = new List<float>();


    void Start()
    {
        robotArmJointsList = new List<GameObject>();
        GetRobotArmJoints(gameObject, robotArmJointsList);

        ArmMoveDelegate += MoveJoints;

        InvokeRepeating(nameof(ExecuteMovement), 0.025f, 0.025f);
    }

    private void Update()
    {
        ArmMoveDelegate?.Invoke(1f);
    }

    private void MoveJoints(float inc)
    {
        increment = inc;
    }

    private void ExecuteMovement()
    {
        if (ControlStateManager.ActualMainState != ControlStateManager.MainState.ARM)
            return;

        if (ControlStateManager.ActualArmState == ControlStateManager.ArmState.JOINT1)
            MoveJoint(robotArmJointsList[0], increment, 0);
        else if (ControlStateManager.ActualArmState == ControlStateManager.ArmState.JOINT2)
            MoveJoint(robotArmJointsList[1], increment, 1);
        else if (ControlStateManager.ActualArmState == ControlStateManager.ArmState.JOINT3)
            MoveJoint(robotArmJointsList[2], increment, 2);
        else if (ControlStateManager.ActualArmState == ControlStateManager.ArmState.JOINT4)
            MoveJoint(robotArmJointsList[3], increment, 3);
        else if (ControlStateManager.ActualArmState == ControlStateManager.ArmState.JOINT5)
            MoveJoint(robotArmJointsList[4], increment, 4);
        else if (ControlStateManager.ActualArmState == ControlStateManager.ArmState.JOINT6)
            MoveJoint(robotArmJointsList[5], increment, 5);
    }

    private void MoveJoint(GameObject joint, float increment, int jointNumber)
    {
        float newAngle = joint.transform.eulerAngles.z + increment;
        float checkAngle = joint.transform.eulerAngles.y + increment;

        if (checkAngle - startAngles[jointNumber] > JointMaxAngles[jointNumber])
            return;
        if (checkAngle - startAngles[jointNumber] < JointMinAngles[jointNumber])
            return;

        joint.transform.eulerAngles = new Vector3(joint.transform.eulerAngles.x, joint.transform.eulerAngles.y, newAngle);
    }

    private void GetRobotArmJoints(GameObject gameObject, List<GameObject> jointsList)
    {
        if (gameObject == null)
            return;

        foreach (Transform child in gameObject.transform)
        {
            if (child == null)
                continue;

            if (child.gameObject.name.Contains("Joint"))
            {
                jointsList.Add(child.gameObject);
                startAngles.Add(child.eulerAngles.y);
            }
            GetRobotArmJoints(child.gameObject, jointsList);
        }
    }
}
