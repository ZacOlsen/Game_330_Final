using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField] private int damage = 5;
	[SerializeField] private Vector2 cornerLL = Vector2.zero;
	[SerializeField] private Vector2 cornerUR = Vector2.zero;
	[SerializeField] private float tiltRange = 5f;

	void Update () {

		Collider colHit = null;
		SimonXInterface.SimonButtonType button = SimonXInterface.SimonButtonType.Button_LL;

		if (SimonXInterface.GetButtonDown (SimonXInterface.SimonButtonType.Button_LL)) {
			button = SimonXInterface.SimonButtonType.Button_LL;
			colHit = Shoot ();

		} else if (SimonXInterface.GetButtonDown (SimonXInterface.SimonButtonType.Button_LR)) {
			button = SimonXInterface.SimonButtonType.Button_LR;
			colHit = Shoot ();

		} else if (SimonXInterface.GetButtonDown (SimonXInterface.SimonButtonType.Button_UR)) {
			button = SimonXInterface.SimonButtonType.Button_UR;
			colHit = Shoot ();

		} else if (SimonXInterface.GetButtonDown (SimonXInterface.SimonButtonType.Button_UL)) {
			button = SimonXInterface.SimonButtonType.Button_UL;
			colHit = Shoot ();
		}

		if (colHit) {
			colHit.GetComponent<Enemy> ().TakeHit (button, damage);
		}
	}

	void FixedUpdate () {

		Vector3 v = SimonXInterface.GetDownVector ();
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

	private Collider Shoot () {

		RaycastHit hit;
		Physics.Raycast (transform.position, -Vector3.up, out hit, 15);

		if (hit.collider && hit.collider.CompareTag ("Enemy")) {
			return hit.collider;
		}

		return null;
	}
}
