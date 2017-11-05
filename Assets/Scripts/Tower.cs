using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

	[SerializeField] private float range = 1.5f;
	[SerializeField] private int damage = 3;
	[SerializeField] private float coolDown = .5f;
	private float timeOfLastShot = float.MinValue;

	[SerializeField] private Transform bulletSpawn = null;
	[SerializeField] private GameObject bullet = null;

	[SerializeField] private Material blue = null;
	[SerializeField] private Material green = null;
	[SerializeField] private Material yellow = null;
	[SerializeField] private Material red = null;

	[SerializeField] private Colors color = Colors.BLUE;

	private MeshRenderer mesh;
	private List<Enemy> enemiesInRange = new List<Enemy> ();

	void Start () {

		mesh = GetComponent<MeshRenderer> ();
		SetColor ();
	}

	void FixedUpdate () {

		while (enemiesInRange.Count > 0 && enemiesInRange [0] == null) {
			enemiesInRange.RemoveAt (0);
		}

		for(int i = 0; i < Enemy.allEnemies.Count; i++){
			if (Vector3.SqrMagnitude (Enemy.allEnemies [i].transform.position - transform.position) <= range * range
				&& color == Enemy.allEnemies[i].color && !enemiesInRange.Contains(Enemy.allEnemies[i])) {
				
				enemiesInRange.Add (Enemy.allEnemies [i]);

				for (int j = enemiesInRange.Count - 1; j >= -1; j--) {
					if (j == -1) {
						enemiesInRange.Add (Enemy.allEnemies [i]);

					} else if (enemiesInRange [j].TilesRemaining () <= Enemy.allEnemies [i].TilesRemaining ()) {
						enemiesInRange.Insert (j + 1, Enemy.allEnemies [i]);
						break;
					}
				}

			} else {
				enemiesInRange.Remove (Enemy.allEnemies [i]);
			}
		}

		Shoot ();
	}

	void OnDrawGizmos () {

		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, range);
	}

	private void Shoot () {

		if (Time.time - timeOfLastShot > coolDown && enemiesInRange.Count > 0) {
			timeOfLastShot = Time.time;

			Instantiate (bullet, bulletSpawn.transform.position, Quaternion.identity)
				.GetComponent<Bullet>().Init(damage, enemiesInRange[0], color);

			Vector3 delta = enemiesInRange [0].transform.position - transform.position;
			transform.rotation = Quaternion.Euler(-90, Mathf.Atan2(delta.x, delta.z) * Mathf.Rad2Deg, 0);
		}
	}

	private void SetColor () {

		switch (color) {

		case Colors.BLUE:
			mesh.material = blue;
			break;

		case Colors.GREEN:
			mesh.material = green;
			break;

		case Colors.RED:
			mesh.material = red;
			break;

		case Colors.YELLOW:
			mesh.material = yellow;
			break;
		}
	}
}
