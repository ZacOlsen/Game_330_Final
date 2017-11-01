using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

	[SerializeField] private bool[] colors = null;	

	private SpriteRenderer sr;

	void Start () {

		sr = GetComponentInChildren<SpriteRenderer> ();

		for (int i = 0; i < colors.Length; i++) {
			if (colors [i]) {

				switch (i) {
				case 0:
					sr.color = Color.blue;
					break;
				case 1:
					sr.color = Color.yellow;
					break;
				case 2:
					sr.color = Color.green;
					break;
				default:
					sr.color = Color.red;
					break;
				}
			}
		}
	}

	void FixedUpdate () {
	}

	public void TakeHit (SimonXInterface.SimonButtonType color) {

		colors [(int)color] = false;

		bool hasLife = false;
		for (int i = 0; i < colors.Length; i++) {
			if (colors [i]) {
				hasLife = true;
				break;
			}
		}

		if (!hasLife) {
			Destroy (gameObject);
		}
	}
}
