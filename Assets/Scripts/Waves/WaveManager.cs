using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WaveManager : MonoBehaviour {

	public struct WaveSpawn {
		int spawnLocation;
		Colors color;
		float timeFromPrevSpawn;
	}

	[SerializeField] private string[] waveFiles;
	[SerializeField] private int waveNum;

	private LinkedList<WaveSpawn> waveSpawns;

	void Start () {


	}
	
	void Update () {
		
	}

	private void CreateWaveList () {

		string fileName = waveFiles [waveNum];
		StreamReader sr = new StreamReader ("Assets\\Wave Files\\" + fileName);

		while (true) {
			string line = sr.ReadLine ();

		}
	}
}
