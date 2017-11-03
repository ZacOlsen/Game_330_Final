//#define DIAGONALS

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour, IComparable<Tile> {
	
	public bool walkable = true;
	public float weight = 1;  // The cost for moving through this tile

	public int x;
	public int y;

	private SpriteRenderer sr;

	public Tile prev;

	public float goal = float.MaxValue;
	public float fitness = float.MaxValue;

	public int heapIndex;
	public bool closed;

	void Start () {
		sr = GetComponentInChildren<SpriteRenderer> ();
	}

	void FixedUpdate () {

		if (!walkable) {
			sr.color = Color.black;
		} else {
			sr.color = Color.white;
		}
	}

	public void Cleanse () {

		heapIndex = 0;
		prev = null;
		goal = float.MaxValue;
		fitness = float.MaxValue;
	}

	public int CompareTo (Tile tile) {

		float f = fitness - tile.fitness;

		if (f < 0) {
			return 1;

		} else if (f > 0) {
			return -1;
		}

		return 0;
	}

	public List<Tile> GetNeighbors () {

		List<Tile> neighbors = new List<Tile> ();

		if (GridWorld.CanTravelTo(x + 1, y)) {
			neighbors.Add (GridWorld.grid [x + 1, y]);
		}

		if (GridWorld.CanTravelTo(x, y + 1)) {
			neighbors.Add (GridWorld.grid [x, y + 1]);
		}

		if (GridWorld.CanTravelTo(x - 1, y)) {
			neighbors.Add (GridWorld.grid [x - 1, y]);
		}

		if (GridWorld.CanTravelTo(x, y - 1)) {
			neighbors.Add (GridWorld.grid [x, y - 1]);
		}

		#if DIAGONALS
		if (x < GridWorld.grid.GetLength(0) - 1 && y < GridWorld.grid.GetLength(1) - 1 && GridWorld.grid[x + 1, y + 1].walkable
			&& GridWorld.grid[x, y + 1].walkable && GridWorld.grid[x + 1, y].walkable) {
			neighbors.Add (GridWorld.grid[x + 1, y + 1]);
		}

		if (x > 0 && y < GridWorld.grid.GetLength(1) - 1 && GridWorld.grid[x - 1, y + 1].walkable
			&& GridWorld.grid[x, y + 1].walkable && GridWorld.grid[x - 1, y].walkable) {
			neighbors.Add (GridWorld.grid[x - 1, y + 1]);
		}

		if (x > 0 && y > 0 && GridWorld.grid[x - 1, y - 1].walkable
			&& GridWorld.grid[x, y - 1].walkable && GridWorld.grid[x - 1, y].walkable) {
			neighbors.Add (GridWorld.grid[x - 1, y - 1]);
		}

		if (x < GridWorld.grid.GetLength(0) - 1 && y > 0 && GridWorld.grid[x + 1, y - 1].walkable
			&& GridWorld.grid[x, y - 1].walkable && GridWorld.grid[x + 1, y].walkable) {
			neighbors.Add (GridWorld.grid[x + 1, y - 1]);
		}
		#endif

		return neighbors;
	}

	public void SetSprite (Sprite sprite) {
		sr.sprite = sprite;
	}

	public String toString() {
		String str = "";
		str += "(" + x + ", " + y + ") " + " " + weight;
		return str;
	}
}
