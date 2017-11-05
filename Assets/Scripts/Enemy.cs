﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum Colors {
	BLUE,
	YELLOW,
	GREEN,
	RED
}

public class Enemy : MonoBehaviour {

	public static List<Enemy> allEnemies = new List<Enemy>();

	[SerializeField] private int blueHP = 5;
	[SerializeField] private int yellowHP = 5;
	[SerializeField] private int greenHP = 5;
	[SerializeField] private int redHP = 5;

	[SerializeField] private float blueSpeed = 5;
	[SerializeField] private float yellowSpeed = 5;
	[SerializeField] private float greenSpeed = 5;
	[SerializeField] private float redSpeed = 5;

	[SerializeField] private int blueValue = 5;
	[SerializeField] private int yellowValue = 6;
	[SerializeField] private int greenValue = 7;
	[SerializeField] private int redValue = 8;

	[SerializeField] private int hp;
	[SerializeField] private float speed;
	[SerializeField] private int goldValue;
	[SerializeField] private int damage = 1;
	private const float ERROR_RANGE = .05f;

	public Colors color;

	private SpriteRenderer sr;
	public Tile currentTile = null;
	public Tile end = null;

	private Stack<Tile> path;

	void Start () {

		sr = GetComponent<SpriteRenderer> ();
		path = AStar.Path (currentTile, end, GridWorld.grid);

		switch (color) {

		case Colors.BLUE:
			goldValue = blueValue;
			break;

		case Colors.GREEN:
			goldValue = greenValue;
			break;

		case Colors.RED:
			goldValue = redValue;
			break;

		case Colors.YELLOW:
			goldValue = yellowValue;
			break;
		}

		SetColor ();
		allEnemies.Add (this);
	}

	void FixedUpdate () {

		if (path.Count != 0) {

			Vector3 tilePos = path.Peek ().transform.position;
			float dist = Vector3.Distance (transform.position, tilePos);

			transform.position = Vector3.Lerp (transform.position, tilePos, speed * Time.fixedDeltaTime / dist);

			if (dist <= ERROR_RANGE) {
				path.Pop ();
			}

		} else {
			
			GameObject.Find ("Player").GetComponent<PlayerController> ().TakeDamage (damage);
			Destroy (gameObject);
		}
	}

	void OnDestroy () {
		allEnemies.Remove (this);
	}

	public void Init (Tile start, Tile goal, Colors color) {

		currentTile = start;
		end = goal;
		this.color = color;
	}

	public void TakeHit (Colors color, int damage) {

		if (this.color == color) {

			hp -= damage;
			if (hp <= 0) {
				
				this.color--;
				if (this.color < 0) {
					GameObject.Find ("Player").GetComponent<PlayerController> ().AddGold (goldValue);
					Destroy (gameObject);
					return;
				}

				SetColor ();
			}
		}
	}

	public int TilesRemaining () {
		return path.Count;
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
