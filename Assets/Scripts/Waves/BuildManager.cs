using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour {

	[SerializeField] private GameObject player = null;
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
	
		} else if (SimonXInterface.GetButtonDown (SimonXInterface.SimonButtonType.Button_LR)) {
			wm.NextWave ();
			gameObject.SetActive (false);
		}
	}

	private void SelectItem () {
		selector.transform.position = items [itemIndex].transform.position;
	}
}
