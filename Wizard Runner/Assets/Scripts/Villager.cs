using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Villager : MonoBehaviour
{

	public float speed = 10;
	public float frequency = 1;
	public float jump = 10;
	float forkTimer = 0;

	public List <Texture> forkless;
	public List <Texture> forked;

	public GameObject bullet;

	public bool jumpOver = false;

	float jumpTimer = 0;
	ConstantForce cf;
	Rigidbody rb;
	SpriteAnimator sa;

	void Start ()
	{
		cf = gameObject.AddComponent<ConstantForce> ();
		rb = gameObject.GetComponent<Rigidbody> ();
		sa = gameObject.GetComponentInChildren<SpriteAnimator> ();
	}

	void Update ()
	{
		if (jumpOver) {
			//cf.force = (PlayerController.player.transform.position - transform.position).normalized * speed + Vector3.up * jump;
			transform.position += transform.up * jump / 100;
			rb.useGravity = false;
			if (jumpTimer <= 0) {
				jumpOver = false;
				rb.useGravity = true;
			}
			//Invoke ("turnJumpOverOff", 0.1f);
		} else {
			//cf.force = (PlayerController.player.transform.position - transform.position).normalized * speed;
		}

		cf.force = (PlayerController.player.transform.position - transform.position).normalized * speed;

		float rando = Random.Range (0f, 30f / frequency);
		if (rando <= 1) {
			GameObject spawn = Instantiate (bullet) as GameObject;
			Rigidbody rb = spawn.GetComponent<Rigidbody> ();
			spawn.transform.position = transform.position + transform.right + transform.up;
			rb.velocity = (PlayerController.player.transform.position - transform.position) + PlayerController.player.rb.velocity;

			forkTimer = 0.5f;
			sa.textures = forkless;
		}
		if (transform.position.y < -10) {
			Destroy (gameObject);
		}
		if (forkTimer <= 0 && forkTimer > -1) {
			sa.textures = forked;
			forkTimer = -2;
		}
		jumpTimer -= Time.deltaTime;
		forkTimer -= Time.deltaTime;
	}
	/*void OnCollisionEnter (Collision col)
	{
		if (col.collider.tag == "Wall" || col.collider.tag == "Enemy") {
			jumpOver = true;
			jumpTimer = 1;
			print ("jump over");
		}
	}*/

	void OnCollisionStay (Collision col)
	{
		if (col.collider.tag == "Wall" || col.collider.tag == "Enemy" /*col.transform.position.y > transform.position.y - 0.2f*/) {
			jumpOver = true;
			jumpTimer = 0.1f;
			print ("jump over");
		}
	}

	void turnJumpOverOff ()
	{
		jumpOver = false;
	}

}
