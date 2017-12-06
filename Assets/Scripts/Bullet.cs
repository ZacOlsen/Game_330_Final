using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	[SerializeField] private int damage = 0;
	[SerializeField] private float speed = 10;
	[SerializeField] private Colors color = Colors.BLUE;
	[SerializeField] private Enemy target;

	private const float ERROR_RANGE = .05f;

	void FixedUpdate () {

		if (target) {
			float dist = Vector3.Distance (transform.position, target.transform.position);
			transform.position = Vector3.Lerp (transform.position, target.transform.position, 
				speed * Time.fixedDeltaTime / dist);

			transform.LookAt (target.transform);

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
}
