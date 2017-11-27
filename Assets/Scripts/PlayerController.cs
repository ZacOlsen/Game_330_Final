using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	[SerializeField] private int damage = 5;
	[SerializeField] private Vector2 cornerLL = Vector2.zero;
	[SerializeField] private Vector2 cornerUR = Vector2.zero;
	[SerializeField] private float tiltRange = 5f;
	[SerializeField] private float speed = 1f;

	[SerializeField] private int totalGold = 0;
	[SerializeField] private int health = 40;

	[SerializeField] private Text healthText = null;
	[SerializeField] private Text goldText = null;

	[SerializeField] private SimonXControlScript sxcs = null;

	[SerializeField] private Color highlite;
	[SerializeField] private Text defeat = null;

	private SpriteRenderer target;

	void Start () {
		UpdateSchmeckles ();
	}

	void Update () {

		Collider colHit = null;
		SimonXInterface.SimonButtonType button = SimonXInterface.SimonButtonType.Button_LL;

		if (SimonXInterface.GetButtonDown (SimonXInterface.SimonButtonType.Button_LL)) {
			button = SimonXInterface.SimonButtonType.Button_LL;
			colHit = ShootRay ();

		} else if (SimonXInterface.GetButtonDown (SimonXInterface.SimonButtonType.Button_LR)) {
			button = SimonXInterface.SimonButtonType.Button_LR;
			colHit = ShootRay ();

		} else if (SimonXInterface.GetButtonDown (SimonXInterface.SimonButtonType.Button_UR)) {
			button = SimonXInterface.SimonButtonType.Button_UR;
			colHit = ShootRay ();

		} else if (SimonXInterface.GetButtonDown (SimonXInterface.SimonButtonType.Button_UL)) {
			button = SimonXInterface.SimonButtonType.Button_UL;
			colHit = ShootRay ();
		}

		if (Input.GetAxisRaw ("Vertical") == 0 && Input.GetAxisRaw ("Horizontal") == 0) {
			sxcs.Recenter ();
		}

		if (colHit && colHit.CompareTag ("Enemy")) {
			colHit.GetComponent<Enemy> ().TakeHit ((Colors)((int)button), damage);
		}
	}

	void FixedUpdate () {

		if (!WaveManager.inWave) {
			Collider col = ShootRay ();

			if (col) {
				SpriteRenderer sr = col.GetComponent<SpriteRenderer> ();
				if (sr != target) {
					if (target) {
						target.color = Color.white;
					}
					target = sr;
					target.color = highlite;
				}
			}
		} else {
			target.color = Color.white;
		}

		Vector3 v = SimonXInterface.GetDownVector ();
		v.y = 0;
		
		if (v.magnitude > tiltRange) {

			Vector3 addPos = v * speed;

			if ((v.x > 0 && transform.position.x > cornerUR.x) || 
				(v.x < 0 && transform.position.x < cornerLL.x)) {
				addPos.x = 0;
			}

			if ((v.z > 0 && transform.position.z > cornerUR.y) || 
				(v.z < 0 && transform.position.z < cornerLL.y)) {
				addPos.z = 0;
			}
				
			transform.position += addPos;
		}
	}

	public void AddGold (int gold) {
		
		totalGold += gold;
		UpdateSchmeckles ();
	}

	public bool TakeGold (int gold) {

		if (totalGold >= gold) {
			totalGold -= gold;
			UpdateSchmeckles ();
			return true;
		}

		return false;
	}

	private void UpdateSchmeckles () {
		goldText.text = "Schmeckles: " + totalGold;
	}

	public void TakeDamage (int damage) {

		health -= damage;
		healthText.text = "Health: " + health;

		if (health <= 0) {
			defeat.enabled = true;
		}
	}

	public Collider ShootRay () {

		RaycastHit hit;
		Physics.Raycast (transform.position, -Vector3.up, out hit, 15);

		return hit.collider;
	}
}
