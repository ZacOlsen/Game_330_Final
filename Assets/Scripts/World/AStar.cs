using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class AStar {

	private static Heap open = new Heap(GridWorld.WIDTH * GridWorld.HEIGHT);

	public static Stack<Tile> Path (Tile start, Tile finish, Tile[,] grid) {

		if (start == finish) {
			return new Stack<Tile> ();
		}
			
		open.Add (start);
		start.goal = 0;
		start.fitness = EstimateHeuristic (start, finish);

		while (open.Count != 0) {

			Tile current = open.RemoveFirst ();
			current.closed = true;

			if (current == finish) {
				break;
			}

			List<Tile> neighbors = current.GetNeighbors();
			for (int i = 0; i < neighbors.Count; i++) {
				if (neighbors[i].closed) {
					continue;
				}

				float tempGoal = current.goal + DistBetween(current, neighbors[i]) * neighbors[i].weight;

				if (!open.Contains (neighbors[i])) {
					
					neighbors [i].prev = current;
					neighbors [i].goal = tempGoal;
					neighbors [i].fitness = neighbors[i].goal + EstimateHeuristic(neighbors[i], finish);
					open.Add (neighbors [i]);

				} else if (tempGoal < neighbors[i].goal) {
					neighbors [i].goal = tempGoal;
					neighbors [i].prev = current;
					neighbors [i].fitness = tempGoal + EstimateHeuristic (neighbors [i], finish);
					open.UpdateItem (neighbors [i]);
					//continue;
				}
			}
		}

		Stack<Tile> path = new Stack<Tile> ();

//		PrintPathRetrace (finish);
		if (finish.prev != null) {
			path = CreatePath (finish);
		}

//		CleanseMap (grid);

		return path;
	}

	private static void CleanseMap (Tile[,] grid) {
	
		open.Reset ();

		for (int x = 0; x < grid.GetLength (0); x++) {
			for (int y = 0; y < grid.GetLength (1); y++) {
				grid [x, y].Cleanse ();
			}
		}
	}

	//distance?
	private static float EstimateHeuristic (Tile current, Tile finish) {
		//return Mathf.Abs (current.x - finish.x) + Mathf.Abs (current.y - finish.y);
		return DistBetween(current, finish);
	}

	private static float DistBetween (Tile current, Tile neighbor) {
		//return Mathf.Pow(current.x - neighbor.x, 2) + Mathf.Pow(current.y - neighbor.y, 2);
		return Vector3.Distance(current.transform.position, neighbor.transform.position);
	}

	private static void PrintPathRetrace (Tile end) {

		while (end != null) {
			Debug.Log (end.x + ", " + end.y);
			end = end.prev;
		}
	}

	private static Stack<Tile> CreatePath (Tile finish) {

		Stack<Tile> path = new Stack<Tile> ();

		while (finish != null) {

			if (finish.prev) {
				Debug.DrawLine (finish.transform.position, finish.prev.transform.position, Color.red, 9999f);
			}

			path.Push (finish);
			finish = finish.prev;
		}

		Debug.Break ();

		return path;
	}
}
