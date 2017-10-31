using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

	private NavMeshAgent agent;

	void Start () {

		agent = GetComponent<NavMeshAgent> ();
		agent.destination = GameObject.Find ("Dest").transform.position;
	}

	void FixedUpdate () {

		agent.velocity = agent.velocity.normalized * agent.speed;
	}
}
