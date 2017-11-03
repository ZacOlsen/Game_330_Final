using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

	public enum Colors {
		BLUE,
		YELLOW,
		GREEN,
		RED
	}

	[SerializeField] private float speed = 5;
	private const float ERROR_RANGE = .05f;

	public Colors color;

	private SpriteRenderer sr;
	public Tile current = null;
	public Tile end = null;

	private Stack<Tile> path;

	void Start () {

		sr = GetComponent<SpriteRenderer> ();
		path = AStar.Path (current, end, GridWorld.grid);

		SetColor ();
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

	public void TakeHit (SimonXInterface.SimonButtonType button) {

		if ((int)color == (int)button) {
			color--;
		}

		if (color < 0) {
			Destroy (gameObject);
			return;
		}

		SetColor ();
	}

	private void SetColor () {

		switch (color) {

		case Colors.BLUE:
			sr.color = Color.blue;
			break;
		case Colors.GREEN:
			sr.color = Color.green;
			break;
		case Colors.RED:
			sr.color = Color.red;
			break;
		case Colors.YELLOW:
			sr.color = Color.yellow;
			break;
		}
	}
}
