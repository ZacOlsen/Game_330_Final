using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	[SerializeField] private Material blue = null;
	[SerializeField] private Material green = null;
	[SerializeField] private Material yellow = null;
	[SerializeField] private Material red = null;

	[SerializeField] private int damage = 0;
	[SerializeField] private float speed = 10;
	[SerializeField] private Colors color = Colors.BLUE;
	[SerializeField] private Enemy target;

	private const float ERROR_RANGE = .05f;

	private MeshRenderer mesh;

	void Start () {

		mesh = GetComponent<MeshRenderer> ();
		SetColor ();
	}
	
	void FixedUpdate () {

		if (target) {
			float dist = Vector3.Distance (transform.position, target.transform.position);
			transform.position = Vector3.Lerp (transform.position, target.transform.position, 
				speed * Time.fixedDeltaTime / dist);

			if (dist <= ERROR_RANGE) {
				target.TakeHit (color, damage);
				Destroy (gameObject);
			}

		} else {
			Destroy (gameObject);
		}
	}

	public void Init (int damage, Enemy target, Colors color) {

		this.damage = damage;
		this.target = target;
		this.color = color;
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
