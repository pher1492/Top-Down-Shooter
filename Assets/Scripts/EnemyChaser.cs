using UnityEngine;
using System.Collections;

[RequireComponent (typeof (NavMeshAgent))]

public class EnemyChaser : Entity {

	// Components
	private NavMeshAgent agent;
	public Transform target;

	void Start () {
		agent = GetComponent<NavMeshAgent> ();
	}

	void Update () {
		agent.SetDestination (target.position);
	}
}
