using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridWorld : MonoBehaviour {

	public const int WIDTH = 7;
	public const int HEIGHT = 9;
	public static Tile[,] grid;

	void Start () {

		grid = new Tile[WIDTH, HEIGHT];

		for (int x = 0; x < WIDTH; x++) {
			for (int y = 0; y < HEIGHT; y++) {

				grid [x, y] = transform.GetChild(x + y * WIDTH).GetComponent<Tile>();
				grid [x, y].x = x;
				grid [x, y].y = y;
				grid [x, y].transform.SetParent (transform);
			}
		}
	}

	public static bool InBounds (int x, int y) {
		return x >= 0 && y >= 0 && x < grid.GetLength (0) && y < grid.GetLength (1);
	}

	public static bool CanTravelTo(int x, int y) {
		return InBounds (x, y) && grid [x, y].walkable;
	}
}
