using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridWorld : MonoBehaviour {

	[SerializeField] private GameObject gridSquare = null;

	public const int WIDTH = 7;
	public const int HEIGHT = 9;
	private bool[,] grid;

	void Start () {

		grid = new bool[WIDTH, HEIGHT];

		for (int x = 0; x < WIDTH; x++) {
			for (int y = 0; y < HEIGHT; y++) {

				Instantiate (gridSquare, new Vector3 (x, 0, y), Quaternion.identity).transform.SetParent (transform);
			}
		}
	}
}
