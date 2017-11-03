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

	[SerializeField] private int blueHP = 5;
	[SerializeField] private int yellowHP = 5;
	[SerializeField] private int greenHP = 5;
	[SerializeField] private int redHP = 5;

	[SerializeField] private float blueSpeed = 5;
	[SerializeField] private float yellowSpeed = 5;
	[SerializeField] private float greenSpeed = 5;
	[SerializeField] private float redSpeed = 5;

	[SerializeField] private int hp;
	[SerializeField] private float speed;
	private const float ERROR_RANGE = .05f;

	public Colors color;

	private SpriteRenderer sr;
	public Tile currentTile = null;
	public Tile end = null;

	private Stack<Tile> path;

	void Start () {

		sr = GetComponent<SpriteRenderer> ();
		path = AStar.Path (currentTile, end, GridWorld.grid);

		SetColor ();
	}

	void FixedUpdate () {

		if (path.Count != 0) {

			Vector3 tilePos = path.Peek ().transform.position;
			float dist = Vector3.Distance (transform.position, tilePos);

			transform.position = Vector3.Lerp(transform.position, tilePos, speed * Time.fixedDeltaTime / dist);

			if (dist <= ERROR_RANGE) {
				path.Pop ();
			}
		}
	}

	public void TakeHit (SimonXInterface.SimonButtonType button, int damage) {

		if ((int)color == (int)button) {

			hp -= damage;
			if (hp <= 0) {
				
				color--;
				if (color < 0) {
					Destroy (gameObject);
					return;
				}

				SetColor ();
			}
		}
	}

	private void SetColor () {

		switch (color) {

		case Colors.BLUE:
			sr.color = Color.blue;
			hp = blueHP;
			speed = blueSpeed;
			break;

		case Colors.GREEN:
			sr.color = Color.green;
			hp = greenHP;
			speed = greenSpeed;
			break;

		case Colors.RED:
			sr.color = Color.red;
			hp = redHP;
			speed = redSpeed;
			break;

		case Colors.YELLOW:
			sr.color = Color.yellow;
			hp = yellowHP;
			speed = yellowSpeed;
			break;
		}
	}
}
