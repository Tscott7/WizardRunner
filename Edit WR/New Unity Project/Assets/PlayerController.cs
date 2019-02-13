using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{

	public static PlayerController player;

	void Awake ()
	{
		player = this;
		Physics.IgnoreLayerCollision (8, 10, true);
	}

	public KeyCode up;
	public KeyCode down;
	public KeyCode left;
	public KeyCode right;

	public KeyCode morphHuman;
	public KeyCode morphFrog;
	public KeyCode morphSnake;
	public KeyCode morphRino;

	public KeyCode jump;

	public int hp = 100;

	public float speed;
	public float climbrate;
	public float JumpForce = 200;
	float jumpTimer = 0;

	public GameObject PS;
	public Rigidbody rb;
	public GameObject DeathSkull;
	Camera cm;

	Vector3 facing;
	SpriteAnimator sa;

	TextMesh HPText;

	bool running = false;
	bool morphing = false;
	bool WallIsRight;
	bool dead = false;

	Vector3 desiredDirection;

	bool free = true;

	public bool grounded = true;
	public bool walled = false;

	Vector3 OGCameraLoc;

	float attack1Timer = 0;

	ConstantForce cf;

	BoxCollider boxCol;

	public enum Mode
	{
		human,
		snake,
		frog,
		rino,
	}

	public Mode mode = Mode.human;


	public List<Texture> HumanWalk;
	public List<Texture> HumanStand;
	public List<Texture> HumanMorph;
	public List<Texture> FrogWalk;
	public List<Texture> FrogStand;
	public List<Texture> FrogMorph;
	public List<Texture> SnakeWalkLeft;
	public List<Texture> SnakeWalkRight;
	public List<Texture> SnakeWalkUp;
	public List<Texture> SnakeStand;
	public List<Texture> SnakeStandUp;
	public List<Texture> SnakeMorph;
	public List<Texture> RinoWalkLeft;
	public List<Texture> RinoWalkRight;
	public List<Texture> RinoStandLeft;
	public List<Texture> RinoStandRight;
	public List<Texture> RinoMorph;

	public List<Texture> Death;

	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		cm = Camera.main;
		facing = new Vector3 (1, 0, 0);

		OGCameraLoc = cm.transform.position;

		sa = GetComponentInChildren<SpriteAnimator> ();
		//HPText = GetComponentInChildren<TextMesh> ();
		//Swish = transform.GetChild (2).gameObject;
		cf = gameObject.AddComponent<ConstantForce> ();

		boxCol = GetComponent<BoxCollider> ();
	}

	void FixedUpdate ()
	{
		if (!dead) {
			/* else if (mode == Mode.snake) {
			speed = 8;
			climbrate = 8;
			JumpForce = 100;
		}*/
			desiredDirection = Vector3.zero;
			if (Input.GetKey (up) && walled) {
			
				/*desiredDirection += (new Vector3 (0, 1, 0) * speed);
			facing = new Vector3 (0, 0, 1);
			//HitSphere.transform.localPosition = facing;
			print ("up");*/
			}
			if (Input.GetKey (down)) {
				/*desiredDirection += (new Vector3 (0, 0, -1) * speed);
			facing = new Vector3 (0, 0, -1);
			HitSphere.transform.localPosition = facing;
			print ("down");*/
			}
			if (Input.GetKey (left)) {
				desiredDirection += (new Vector3 (-1, 0, 0) * speed);
				facing = new Vector3 (-1, 0, 0);
				//HitSphere.transform.localPosition = facing;
				//print ("left");
			}
			if (Input.GetKey (right)) {
				desiredDirection += (new Vector3 (1, 0, 0) * speed);
				facing = new Vector3 (1, 0, 0);
			
				//print ("right");

			}
			if (Input.GetKey (jump) && grounded && jumpTimer > 0.3f) {
				Jump ();
				jumpTimer = 0;
				grounded = false;
				print ("jump");

			}
			if (Input.GetKey (jump) && walled && mode != Mode.snake) {
				desiredDirection += (new Vector3 (0, 1, 0) * climbrate);
				facing = new Vector3 (0, 0, 1);

				//print ("up");
			}
			if (Input.GetKey (up) && walled && mode == Mode.snake) {
				desiredDirection += (new Vector3 (0, 1, 0) * climbrate);
				facing = new Vector3 (0, 0, 1);

				//print ("up");
			}
			if (Input.GetKey (down) && walled && mode == Mode.snake) {
				desiredDirection += (new Vector3 (0, -1, 0) * climbrate);
				facing = new Vector3 (0, 0, 1);

				//print ("up");
			}
			/*if (mode == Mode.normal && (Input.GetKey (right) || Input.GetKey (left) || Input.GetKey (up)) && !running) {
			sa.startIE ();
			running = true;
		}*//* else {
			sa.stopIE ();
			running = false;
		} */
			/*else if (mode == Mode.normal && running) {
			sa.stopIE ();
			running = false;
		}*/
			if (Input.GetKeyDown (morphHuman) && mode != Mode.human && !morphing) {
				speed = 12;
				climbrate = 4;
				JumpForce = 350;
				StartCoroutine (Morph (Mode.human));
			}
			if (Input.GetKey (morphFrog) && mode != Mode.frog && !morphing) {
				speed = 6;
				climbrate = 6;
				JumpForce = 600;
				StartCoroutine (Morph (Mode.frog));
			}
			if (Input.GetKey (morphSnake) && mode != Mode.snake && !morphing) {
				speed = 8;
				climbrate = 8;
				JumpForce = 250;
				StartCoroutine (Morph (Mode.snake));
			}
			if (Input.GetKey (morphRino) && mode != Mode.rino && !morphing) {
				speed = 17;
				climbrate = 0;
				JumpForce = 0;
				StartCoroutine (Morph (Mode.rino));
			}




			if ((Input.GetKey (left) || Input.GetKey (right)) && !running && !morphing) {
				//running
				if (mode == Mode.human) {
					sa.textures = HumanWalk;
				}
				if (mode == Mode.frog) {
					sa.textures = FrogWalk;
				}
				if (mode == Mode.snake) {
				
					if (Input.GetKey (left)) {
						sa.textures = SnakeWalkLeft;
					} else if (Input.GetKey (right)) {
						sa.textures = SnakeWalkRight;
					}
				}
				if (mode == Mode.rino) {
					if (Input.GetKey (left)) {
						sa.textures = RinoWalkLeft;
					} else if (Input.GetKey (right)) {
						sa.textures = RinoWalkRight;
					}
				}
				running = true;

			}

			if (mode == Mode.snake && walled && !grounded) {
				rb.useGravity = false;
				if (WallIsRight) {
					transform.right = transform.up;
				} else {
					transform.right = -transform.up;
				}
		
				/*if (Input.GetKey (up) || Input.GetKey (down)) {
				sa.textures = SnakeWalkUp;
			} else {
				sa.textures = SnakeStandUp;
				rb.velocity = Vector3.zero;
			}*/
				/*sa.transform.localScale = new Vector3 (1, 2, 1);
			sa.transform.localPosition = new Vector3 (0, -0f, -0.5f);
			boxCol.center = new Vector3 (0, 0, 0);
			boxCol.size = new Vector3 (0.4f, 0.4f, 1);*/

			} else if (mode == Mode.snake && !walled && grounded) {
				transform.up = Vector3.up;
				/*sa.transform.localScale = new Vector3 (2, 1, 1);
			sa.transform.localPosition = new Vector3 (0, -0.1f, -0.5f);
			boxCol.center = new Vector3 (0, 0, 0);
			boxCol.size = new Vector3 (0.4f, 0.4f, 1);*/
				rb.useGravity = true;
			} else if (mode != Mode.snake) {
				transform.up = Vector3.up;
				rb.useGravity = true;
			} else {
				if (rb.useGravity == false) {
					Invoke ("turnGravityOn", 0.2f);
				}

			}

			if (!Input.GetKey (left) && !Input.GetKey (right) && running && !morphing) {
				//standing
				if (mode == Mode.human) {
					sa.textures = HumanStand;
				}
				if (mode == Mode.frog) {
					sa.textures = FrogStand;
				}
				if (mode == Mode.snake) {
				
					sa.textures = SnakeStand;

				
				}
				if (mode == Mode.rino) {
					sa.textures = RinoStandRight;
				}
				print ("stand");
				running = false;
			}


			if (free) {
				//cf.force = desiredDirection;
				transform.position += desiredDirection / 100;
				//HPText.text = hp.ToString ();
			}

			// transform the camera;

			cm.transform.position = new Vector3 (cm.transform.position.x, cm.transform.position.y, OGCameraLoc.z) * 0.9f + new Vector3 (transform.position.x, transform.position.y, -rb.velocity.magnitude * 0.3f) * 0.1f;
			//print ("transform the camera");


			attack1Timer -= Time.deltaTime;
			walled = false;
			jumpTimer += Time.deltaTime;
		}

	}

	void OnCollisionEnter (Collision col)
	{
		if (col.collider.transform.tag == "Floor") {
			grounded = true;
			running = false;
		}
		if (col.collider.transform.tag == "Enemy") {
			DIE ();
		}
		//print ("hit " + col.transform.name);
	}

	void OnCollisionExit (Collision col)
	{
		if (col.collider.transform.tag == "Floor") {
			grounded = false;
		}

	}

	void OnCollisionStay (Collision col)
	{
		if (col.collider.transform.tag == "Wall") {
			walled = true;
			if (col.transform.position.x > transform.position.x) {
				WallIsRight = true;
			} else {
				WallIsRight = false;
			}
		}
		if (col.collider.transform.tag == "Floor") {
			grounded = true;
		}
	}

	void Jump ()
	{
		rb.AddForce (new Vector3 (0, 1, 0) * JumpForce);
	}

	void turnGravityOn ()
	{
		rb.useGravity = true;
	}

	IEnumerator Morph (Mode ChangeToMode)
	{
		if (mode == Mode.human) {
			sa.textures = HumanMorph;
		}
		if (mode == Mode.frog) {
			sa.textures = FrogMorph;
		}
		if (mode == Mode.snake) {
			sa.textures = SnakeMorph;
		}
		if (mode == Mode.rino) {
			sa.textures = RinoMorph;
		}
		morphing = true;
		mode = ChangeToMode;

		yield return new WaitForSeconds ((float)4 / (float)sa.framerate);
		GameObject particle = Instantiate (PS) as GameObject;
		particle.transform.position = transform.position;
		running = false;
		morphing = false;
		if (mode == Mode.human) {
			sa.textures = HumanStand;
			sa.transform.localScale = Vector3.one * 2;
			sa.transform.localPosition = new Vector3 (0, 0.35f, -0.5f);
			boxCol.center = new Vector3 (0, 0.12f, 0);
			boxCol.size = new Vector3 (0.85f, 1.25f, 1);
		}
		if (mode == Mode.frog) {
			sa.textures = FrogStand;
			sa.transform.localScale = Vector3.one * 2;
			sa.transform.localPosition = new Vector3 (0, 0.35f, -0.5f);
			boxCol.center = new Vector3 (0, -0.12f, 0);
			boxCol.size = new Vector3 (1, 0.8f, 1);
		    
		}
		if (mode == Mode.snake) {
			sa.textures = SnakeStand;
			sa.transform.localScale = new Vector3 (2, 1, 1);
			sa.transform.localPosition = new Vector3 (0, -0.1f, -0.5f);
			boxCol.center = new Vector3 (0, 0, 0);
			boxCol.size = new Vector3 (0.4f, 0.4f, 1);
		    
		}
		if (mode == Mode.rino) {
			sa.textures = RinoStandRight;
			sa.transform.localScale = new Vector3 (4, 4, 1);
			sa.transform.localPosition = new Vector3 (0, 0, -0.5f);
			boxCol.center = new Vector3 (0, 0, 0);
			boxCol.size = new Vector3 (2.5f, 2.2f, 1);
		    
		}
	}

	void DIE ()
	{
		dead = true;
		sa.textures = Death;
		boxCol.isTrigger = true;
		rb.useGravity = false;
		rb.velocity = Vector3.zero;

		GameObject spawned = Instantiate (DeathSkull) as GameObject;
		spawned.transform.position = transform.position;

		Rigidbody rbb = spawned.GetComponent<Rigidbody> ();

		rbb.velocity = new Vector3 (Random.Range (0f, 10f), Random.Range (-5f, 5f), 0);
		rbb.AddTorque (0, 0, Random.Range (-10f, 10f));
		
		//Destroy (gameObject);

		Invoke ("destroyIt", 0.3f);
	}

	void destroyIt ()
	{
		//Destroy (gameObject);
		gameObject.SetActive (false);
	}

	/*public void Punch (PlayerController pc)
	{
		if (Input.GetKeyDown (punch)) {
			StartCoroutine (attack1 (pc));
		}
	}*/

	/*IEnumerator attack1 (PlayerController pc)
	{
		if (attack1Timer <= 0) {
			attack1Timer = 1;
			free = false;
			StartCoroutine (attack1Animation ());
			yield return new WaitForSeconds (0.2f);
			pc.hp -= 5;
			pc.rb.velocity += (facing + Vector3.up) * 2;
			free = true;
		}
	}*/

	/*IEnumerator attack1Animation ()
	{
		Swish.SetActive (true);
		//Swish.transform.localPosition = Vector3.up;
		float Dtime = 0.2f;
		float steps = 10;
		for (int i = 0; i < steps; i++) {
			Swish.transform.localPosition = Vector3.Lerp (Vector3.up, facing, i / steps);
			yield return new WaitForSeconds (Dtime / steps);
		}
		Swish.SetActive (false);
	}*/

}
