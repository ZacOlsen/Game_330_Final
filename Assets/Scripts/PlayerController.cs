using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField] private Vector2 cornerLL = Vector2.zero;
	[SerializeField] private Vector2 cornerUR = Vector2.zero;
	[SerializeField] private float tiltRange = 5f;

	void Update () {

		if (SimonXInterface.GetButtonDown (SimonXInterface.SimonButtonType.Button_LL)) {

			RaycastHit hit;
			Physics.Raycast (transform.position, -Vector3.up, out hit, 15);

			if (hit.collider != null && hit.collider.CompareTag("Enemy")) {
				hit.collider.GetComponent<Enemy> ().TakeHit (SimonXInterface.SimonButtonType.Button_LL);
			}

		} else if (SimonXInterface.GetButtonDown (SimonXInterface.SimonButtonType.Button_LR)) {

		} else if (SimonXInterface.GetButtonDown (SimonXInterface.SimonButtonType.Button_UR)) {

		} else if (SimonXInterface.GetButtonDown (SimonXInterface.SimonButtonType.Button_UL)) {

		} 
	}

	void FixedUpdate () {

		Vector3 v = SimonXInterface.GetUpVector ();
		v.y = 0;
		
		if (v.magnitude > tiltRange) {

			Vector3 addPos = v;

			if ((v.x > 0 && transform.position.x > cornerUR.x) || 
				(v.x < 0 && transform.position.x < cornerLL.x)) {
				addPos.x = 0;
			}

			if ((v.z > 0 && transform.position.z > cornerUR.y) || 
				(v.z < 0 && transform.position.z < cornerLL.y)) {
				addPos.z = 0;
			}
				
			transform.position += addPos;
		}
	}
}
