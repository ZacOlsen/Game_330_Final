using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour {

	private struct WaveSpawn {
		public int spawnLocation;
		public Colors color;
		public float timeFromPrevSpawn;
	}

	[SerializeField] private string[] waveFiles = null;
	[SerializeField] private int waveNum = -1;

	[SerializeField] private Tile[] spawnLocations = null;
	[SerializeField] private Tile goalLocation = null;
	private float timeOfLastSpawn;

	[SerializeField] private GameObject enemy = null;
	private LinkedList<WaveSpawn> waveSpawns = new LinkedList<WaveSpawn> ();
	public static bool inWave = false;
	[SerializeField] private GameObject bm = null;

	[SerializeField] private Text waveText = null;

	void FixedUpdate () {

		if (waveSpawns.Count > 0) {

			if (Time.time - timeOfLastSpawn >= waveSpawns.First.Value.timeFromPrevSpawn) {
				SpawnNextEnemy ();
				timeOfLastSpawn = Time.time;
			}

		} else if (inWave && Enemy.allEnemies.Count == 0) {
			inWave = false;
			bm.SetActive (true);
		}
	}

	public void NextWave () {

		waveNum++;
		if (waveNum < waveFiles.Length) {
			inWave = true;
			timeOfLastSpawn = Time.time;
			CreateWaveList ();
		} else {
			string name = SceneManager.GetActiveScene ().name;
			SceneManager.LoadScene ("Level " + (int.Parse(name[name.Length - 1].ToString()) + 1));
		}
	}

	public Tile[] GetSpawnLocations () {
		return spawnLocations;
	}

	public Tile GetGoalLocation () {
		return goalLocation;
	}

	private void SpawnNextEnemy () {

		WaveSpawn ws = waveSpawns.First.Value;
		waveSpawns.RemoveFirst ();

		Instantiate (enemy, spawnLocations [ws.spawnLocation - 1].transform.position, enemy.transform.rotation)
			.GetComponent<Enemy>().Init(spawnLocations[ws.spawnLocation - 1], goalLocation, ws.color);
	}

	private void CreateWaveList () {

		waveText.text = "Wave: " + (waveNum + 1);
		waveSpawns = new LinkedList<WaveSpawn> ();

		string fileName = waveFiles [waveNum];

		string name = SceneManager.GetActiveScene ().name;
		StreamReader sr = new StreamReader ("Assets\\Wave Files\\Map" + name[name.Length - 1] + "\\" + fileName);

		while (sr.Peek() >= 0) {
			string line = sr.ReadLine ();

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
	
			waveSpawns.AddLast (ws);
		}
	}
}
