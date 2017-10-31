using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonXInterface : MonoBehaviour {

    public enum SimonButtonType
    {
        Button_UL,
        Button_UR,
        Button_LL,
        Button_LR
    }

    static bool[] SimonButtonPressed;
    static bool[] SimonButtonPressedLastFrame;

    static Transform SimonXTransform;

    public SimonXControlScript TargetSimonX;

    // Use this for initialization
    void Start () {
        SimonButtonPressed = new bool[System.Enum.GetValues(typeof(SimonButtonType)).Length];
        SimonButtonPressedLastFrame = new bool[System.Enum.GetValues(typeof(SimonButtonType)).Length];
		SimonXTransform = TargetSimonX.transform;
    }
	
	// Update is called once per frame
	void Update () {
        UpdateButtonStateFromSimonX(SimonButtonType.Button_UL, 1);
        UpdateButtonStateFromSimonX(SimonButtonType.Button_UR, 0);
        UpdateButtonStateFromSimonX(SimonButtonType.Button_LL, 3);
        UpdateButtonStateFromSimonX(SimonButtonType.Button_LR, 2);

        SimonXTransform = TargetSimonX.transform;
    }

    void UpdateButtonStateFromSimonX(SimonButtonType buttonType, int buttonIndex)
    {
        SimonButtonPressedLastFrame[(int)buttonType] = SimonButtonPressed[(int)buttonType];
        SimonButtonPressed[(int)buttonType] = TargetSimonX.IsButtonDepressed(buttonIndex);
    }
        
    public static bool GetButton(SimonButtonType buttonType)
    {
        return SimonButtonPressed[(int)buttonType];
    }

    public static bool GetButtonDown(SimonButtonType buttonType)
    {
        return SimonButtonPressed[(int)buttonType] && !SimonButtonPressedLastFrame[(int)buttonType];
    }

    public static bool GetButtonUp(SimonButtonType buttonType)
    {
        return !SimonButtonPressed[(int)buttonType] && SimonButtonPressedLastFrame[(int)buttonType];
    }

    public static Vector3 GetDownVector()
    {
        return -SimonXTransform.up;
    }

    public static Vector3 GetUpVector()
    {
        return SimonXTransform.up;
    }

    public static Vector3 GetForwardVector()
    {
        return -SimonXTransform.forward;
    }

    public static Vector3 GetBackwardVector()
    {
        return -SimonXTransform.forward;
    }

    public static Vector3 GetRightVector()
    {
        return -SimonXTransform.right;
    }

    public static Vector3 GetLeftVector()
    {
        return -SimonXTransform.right;
    }
}
