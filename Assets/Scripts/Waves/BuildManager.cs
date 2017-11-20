using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour {

	[SerializeField] private PlayerController player = null;
	[SerializeField] private GameObject selector = null;

	[SerializeField] private BuyItem[] items = null;
	private int itemIndex;

	[SerializeField] private WaveManager wm = null;

	void Update () {

		if (SimonXInterface.GetButtonDown (SimonXInterface.SimonButtonType.Button_UL)) {

			itemIndex--;
			if (itemIndex < 0) {
				itemIndex = items.Length - 1;
			}

			SelectItem ();

		} else if (SimonXInterface.GetButtonDown (SimonXInterface.SimonButtonType.Button_LL)) {
		
			itemIndex++;
			if (itemIndex >= items.Length) {
				itemIndex = 0;
			}

			SelectItem ();
		
		} else if (SimonXInterface.GetButtonDown (SimonXInterface.SimonButtonType.Button_UR)) {
			AttemptToBuild ();

		} else if (SimonXInterface.GetButtonDown (SimonXInterface.SimonButtonType.Button_LR)) {
			wm.NextWave ();
			gameObject.SetActive (false);
		}
	}

	private void SelectItem () {
		selector.transform.position = items [itemIndex].transform.position;
	}

	private void AttemptToBuild () {

		Collider col = player.ShootRay ();
		if (col && col.CompareTag ("Tile")) {
			Tile t = col.GetComponent<Tile> ();
			
			if (itemIndex == items.Length - 1 && t.blocker) {
				Destroy (t.blocker);
				t.walkable = true;
				return;
			}

			if (t.walkable) {

				t.walkable = false;

				bool pathPossible = true;
				Tile[] spawnLocs = wm.GetSpawnLocations ();
				for (int i = 0; i < spawnLocs.Length; i++) {

					if (AStar.Path (spawnLocs [i], wm.GetGoalLocation (), GridWorld.grid).Count == 0) {
						pathPossible = false;
						break;
					}
				}

				if (pathPossible && player.TakeGold (items [itemIndex].GetCost ())) {
					t.blocker = Instantiate (items [itemIndex].GetItem (), t.transform.position, 
						Quaternion.Euler(-90f, 0, 0));

				} else {
					t.walkable = true;
				}
			}
		}
	}
}
