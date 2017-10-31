using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputCheckExample : MonoBehaviour {

    Text myText;

	// Use this for initialization
	void Start () {
        myText = GetComponent<Text>();	
	}
	
	// Update is called once per frame
	void Update () {
        string textToPrint = "Pressed Buttons:\n";

        if(SimonXInterface.GetButton(SimonXInterface.SimonButtonType.Button_UL))
        {
            textToPrint += "Upper Left\n";
        }

        if (SimonXInterface.GetButton(SimonXInterface.SimonButtonType.Button_UR))
        {
            textToPrint += "Upper Right\n";
        }

        if (SimonXInterface.GetButton(SimonXInterface.SimonButtonType.Button_LL))
        {
            textToPrint += "Lower Left\n";
        }

        if (SimonXInterface.GetButton(SimonXInterface.SimonButtonType.Button_LR))
        {
            textToPrint += "Lower Right\n";
        }

        myText.text = textToPrint;
	}
}
