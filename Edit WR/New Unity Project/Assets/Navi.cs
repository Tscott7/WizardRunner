using UnityEngine;
using System.Collections;

public class Navi : MonoBehaviour {
	public Transform Player;
	UnityEngine.AI.NavMeshAgent Agent;

	// Use this for initialization
	void Start () {
		Agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		Agent.SetDestination (Player.position);
	}
}
