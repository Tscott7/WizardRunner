using UnityEngine;
using System.Collections;

public class Destructable : MonoBehaviour
{

	public GameObject Brick;

	void OnCollisionEnter (Collision col)
	{
		if (col.collider.tag == "Player") {
			if (PlayerController.player.mode == PlayerController.Mode.rino) {
				Death ();
			}
		}
	}

	void Death ()
	{
		//Destroy (gameObject);	
		/*Rigidbody rb = gameObject.GetComponent<Rigidbody> ();
		rb.isKinematic = false;
		rb.useGravity = true;
		rb.AddForce (new Vector3 (0, 0, 1) * Random.Range (50f, 90f));*/
		for (int i = 0; i < 7; i++) {
			GameObject spawn = Instantiate (Brick) as GameObject;
			Vector3 rando = Random.onUnitSphere;
			spawn.transform.position = transform.position + rando;
			spawn.GetComponent<Rigidbody> ().velocity = rando * 5;
			spawn.transform.Rotate (rando * Random.Range (0, 180));

		}
		Destroy (gameObject);	
	}
}
