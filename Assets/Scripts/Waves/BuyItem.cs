using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyItem : MonoBehaviour {

	[SerializeField] private GameObject item = null;
	[SerializeField] private int cost = 20;

	void Start () {
		GetComponentInChildren<Text> ().text = cost + "G";
	}

	public int GetCost () {
		return cost;
	}

	public GameObject GetItem () {
		return item;
	}
}
