using UnityEngine;
using System.Collections;

public class killWall : MonoBehaviour
{

	public float speed = 10;

	void FixedUpdate ()
	{
		transform.position += new Vector3 (1, 0, 0) * speed / 100;
	}

	void OnCollisionEnter (Collision col)
	{
		if (col.transform.tag == "Player") {
			Destroy (col.gameObject);
		}
	}
}
