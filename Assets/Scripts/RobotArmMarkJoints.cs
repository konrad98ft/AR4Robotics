using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotArmMarkJoints : MonoBehaviour
{
    public delegate void InputChanged();
    public static InputChanged InputChangedDelegate;

    public Material hoverMaterial;
    public Material basicMaterial;

    private List<GameObject> robotArmJointsList;


    void Start()
    {
        robotArmJointsList = new List<GameObject>();
        GetRobotArmJoints(gameObject, robotArmJointsList);
        EraseJointsMarkings();
        InputChangedDelegate += RecieveInputsToMarkMovedJoints;

        ControlStateManager.ActualArmState = ControlStateManager.ArmState.JOINT1;
        ControlStateManager.ActualMainState = ControlStateManager.MainState.ARM;
        InputChangedDelegate?.Invoke();
    }

    private void RecieveInputsToMarkMovedJoints()
    {
        if (ControlStateManager.ActualMainState != ControlStateManager.MainState.ARM)
            return;

        if (ControlStateManager.ActualArmState == ControlStateManager.ArmState.JOINT1)
            SelectJoins(robotArmJointsList[0]);
        else if (ControlStateManager.ActualArmState == ControlStateManager.ArmState.JOINT2)
            SelectJoins(robotArmJointsList[1]);
        else if (ControlStateManager.ActualArmState == ControlStateManager.ArmState.JOINT3)
            SelectJoins(robotArmJointsList[2]);
        else if (ControlStateManager.ActualArmState == ControlStateManager.ArmState.JOINT4)
            SelectJoins(robotArmJointsList[3]);
        else if (ControlStateManager.ActualArmState == ControlStateManager.ArmState.JOINT5)
            SelectJoins(robotArmJointsList[4]);
        else if (ControlStateManager.ActualArmState == ControlStateManager.ArmState.JOINT6)
            SelectJoins(robotArmJointsList[5]);
    }

    private void SelectJoins(GameObject joint)
    {

        Renderer rend = joint.GetComponent<Renderer>();
        rend.material = hoverMaterial;
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
            }
            GetRobotArmJoints(child.gameObject, jointsList);
        }
    }

    private void EraseJointsMarkings()
    {
        foreach (GameObject joint in robotArmJointsList)
        {
            Renderer rend = joint.GetComponent<Renderer>();
            rend.material = basicMaterial;
        }
    }
}
