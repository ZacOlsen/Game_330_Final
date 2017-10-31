using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFromSimonScript : MonoBehaviour {

    public float GravityMagnitude = 9.81f;

	void FixedUpdate () {
        Physics.gravity = SimonXInterface.GetDownVector() * GravityMagnitude;
	}
}
