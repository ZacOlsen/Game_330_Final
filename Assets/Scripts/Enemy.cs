using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

	[SerializeField] private float speed = 5;
	private const float ERROR_RANGE = .05f;

	[SerializeField] private bool[] colors = null;	

	private SpriteRenderer sr;
	public Tile current = null;
	public Tile end = null;

	private Stack<Tile> path;

	void Start () {

		sr = GetComponent<SpriteRenderer> ();

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

		path = AStar.Path (current, end, GridWorld.grid);
	}

	void FixedUpdate () {

		if (path.Count != 0) {

			Vector3 tilePos = path.Peek ().transform.position;
			float dist = Vector3.Distance (transform.position, tilePos);

			transform.position = Vector3.Lerp(transform.position, tilePos, 1 / dist * speed);

			if (dist <= ERROR_RANGE) {
				path.Pop ();
			}
		}
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
