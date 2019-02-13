using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{

	public float Frequency = 1;

	public GameObject bullet;




	void Update ()
	{
		float rando = Random.Range (0f, 30f / Frequency);
		if (rando <= 1) {
			GameObject spawn = Instantiate (bullet) as GameObject;
			Rigidbody rb = spawn.GetComponent<Rigidbody> ();
			spawn.transform.position = transform.position + transform.right;
			rb.velocity = (PlayerController.player.transform.position - transform.position).normalized /*+ PlayerController.player.rb.velocity*/;
		}
	}
}
