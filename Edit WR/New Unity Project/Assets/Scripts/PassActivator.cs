using UnityEngine;
using System.Collections;

public class PassActivator : MonoBehaviour
{

	public GameObject[] villagers;

	GameObject Player;

	void Start ()
	{
		Player = PlayerController.player.gameObject;
	}

	void Update ()
	{
		if (transform.position.x < Player.transform.position.x) {
			foreach (GameObject vill in villagers) {
				vill.SetActive (true);
			}
			Destroy (gameObject);
		}

	}
}
