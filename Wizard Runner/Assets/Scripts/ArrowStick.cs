using UnityEngine;
using System.Collections;

public class ArrowStick : MonoBehaviour
{
	public bool Mesh = true;
	public bool UpdateFix = true;
	public bool hitAll = true;
	public bool noChild = true;
	public bool arrowFly = true;
	Quaternion savedRotation;
	Rigidbody rbb;

	bool hit = false;

	void Start ()
	{
		rbb = gameObject.GetComponent<Rigidbody> ();
	}

	void OnCollisionEnter (Collision col)
	{
		if ((col.transform.tag == "Wall" || hitAll || col.transform.tag == "Floor") && hit == false) {
			if (Mesh) {
				gameObject.GetComponent<MeshCollider> ().isTrigger = true;
			} else {
				gameObject.GetComponent<CapsuleCollider> ().isTrigger = true;
			}
			rbb.constraints = RigidbodyConstraints.FreezePosition;
			rbb.angularVelocity = Vector3.zero;
			rbb.velocity = Vector3.zero;
			//rbb.velocity = Vector3.zero;
			transform.position = (transform.position * 2 + col.contacts [0].point) * 0.333f;
			if (!noChild) {
				transform.SetParent (col.transform);
			}
			//rbb.constraints = RigidbodyConstraints.FreezePosition;

			//savedRotation = transform.localRotation;
			hit = true;
			//rbb.constraints = RigidbodyConstraints.FreezeRotation;
			//rbb.velocity = Vector3.zero;
			//rbb.constraints = RigidbodyConstraints.FreezeRotationX;
			//rbb.constraints = RigidbodyConstraints.FreezeRotationY;
			//rbb.constraints = RigidbodyConstraints.FreezeRotationZ;

			//rbb.velocity = (col.contacts [0].point + transform.position * (-1)).normalized * rbb.velocity.magnitude;
			//StartCoroutine (stickit (0.8f, 15, 0.3f, rbb, col.transform));
			//rbb.constraints = RigidbodyConstraints.FreezeAll;
			//gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
			print ("stuck");
			//Destroy (this);

		}
	}

	void Update ()
	{
		if (UpdateFix && hit) {
			transform.localRotation = savedRotation;
		}
		if (arrowFly && !hit)
			transform.forward = rbb.velocity;
	}

	IEnumerator stickit (float clamp, int steps, float length, Rigidbody rb, Transform parent)
	{
		//rb.constraints = RigidbodyConstraints.FreezeRotationX;
		//rb.constraints = RigidbodyConstraints.FreezeRotationY;
		//rb.constraints = RigidbodyConstraints.FreezeRotationZ;
		//rb.velocity = rb.velocity * (-1);
		for (int i = 0; i < steps; i++) {
			rb.velocity = rb.velocity * clamp;

			yield return new WaitForSeconds (length / steps);
		}
		transform.SetParent (parent);
		rb.constraints = RigidbodyConstraints.FreezeAll;
		Destroy (this);
	}
}
