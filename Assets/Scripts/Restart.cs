using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour {
	
	void Update () {

		if (SimonXInterface.GetButtonDown (SimonXInterface.SimonButtonType.Button_LL) ||
		    SimonXInterface.GetButtonDown (SimonXInterface.SimonButtonType.Button_LR) ||
		    SimonXInterface.GetButtonDown (SimonXInterface.SimonButtonType.Button_UL) ||
		    SimonXInterface.GetButtonDown (SimonXInterface.SimonButtonType.Button_UR)) {
			SceneManager.LoadScene ("Level 1");
		}
	}
}
