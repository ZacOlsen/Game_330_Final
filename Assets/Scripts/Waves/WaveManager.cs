using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WaveManager : MonoBehaviour {

	private struct WaveSpawn {
		public int spawnLocation;
		public Colors color;
		public float timeFromPrevSpawn;
	}

	[SerializeField] private string[] waveFiles = null;
	[SerializeField] private int waveNum = 0;

	[SerializeField] private Tile[] spawnLocations = null;
	[SerializeField] private Tile goalLocation = null;
	private float timeOfLastSpawn;

	[SerializeField] private GameObject enemy = null;
	private LinkedList<WaveSpawn> waveSpawns;

	void Start () {

		CreateWaveList ();
		timeOfLastSpawn = Time.time;
	}
	
	void FixedUpdate () {

		if (waveSpawns.Count > 0 && Time.time - timeOfLastSpawn >= waveSpawns.First.Value.timeFromPrevSpawn) {
			SpawnNextEnemy ();
			timeOfLastSpawn = Time.time;
		}
	}

	private void SpawnNextEnemy () {
	
		//Debug.Log ("spawn");

		WaveSpawn ws = waveSpawns.First.Value;
		waveSpawns.RemoveFirst ();

		Instantiate (enemy, spawnLocations [ws.spawnLocation - 1].transform.position, enemy.transform.rotation)
			.GetComponent<Enemy>().Init(spawnLocations[ws.spawnLocation - 1], goalLocation, ws.color);
	}

	private void CreateWaveList () {

		waveSpawns = new LinkedList<WaveSpawn> ();

		string fileName = waveFiles [waveNum];
		StreamReader sr = new StreamReader ("Assets\\Wave Files\\Map1\\" + fileName);

		while (sr.Peek() >= 0) {
			string line = sr.ReadLine ();
		//	Debug.Log (line);

			WaveSpawn ws = new WaveSpawn ();

			int start = 0;
			int end = line.IndexOf (" ");

			ws.spawnLocation = int.Parse (line.Substring (start, end));

			start = end + 1;
			end = line.IndexOf (" ", start);
			string col = line.Substring (start, end - start);

			switch (col) {

			case "blue":
				ws.color = Colors.BLUE;
				break;	

			case "green":
				ws.color = Colors.GREEN;
				break;

			case "yellow":
				ws.color = Colors.YELLOW;
				break;	

			case "red":
				ws.color = Colors.RED;
				break;	

			default:
				break;	
			}

			start = end + 1;
			end = line.Length;

			ws.timeFromPrevSpawn = float.Parse(line.Substring(start, end - start));
	
	//		Debug.Log (ws.spawnLocation + " " + ws.color + " " + ws.timeFromPrevSpawn);
			waveSpawns.AddLast (ws);
		}
	}
}
