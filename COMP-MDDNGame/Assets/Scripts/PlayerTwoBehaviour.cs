using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTwoBehaviour : MonoBehaviour
{
	public GameObject enemy;
	public float jump;
	public float speed;

	public int dashSpeed;
	public float dashCooldown = 2;

	public Collider2D[] attackHitboxes;

	public bool onRightSide = false;
	public bool shieldUp = false;

	private PlayerOneBehaviour enemyScript;
	private Rigidbody2D rb2d;
	private float moveVelocity;
	private bool grounded = true;
	private float nextDash = 1;

	// Use this for initialization
	void Start ()
	{
		rb2d = GetComponent<Rigidbody2D> ();
		enemyScript = enemy.GetComponent<PlayerOneBehaviour> ();
	}

	void Update ()
	{
		if (Input.GetKey (KeyCode.W)) {									//	jump
			Jump();
		} else if (Input.GetKeyDown (KeyCode.LeftShift) && shieldUp == false) {  //melee
			LaunchAttack (attackHitboxes [1]);	
		} else if (Input.GetKeyDown (KeyCode.E)) {							//block
			Block();
		}
		//check if players have passed each other
					Vector3 position = transform.position;
			
					if (onRightSide == true) {
						if (position.x < enemyScript.transform.position.x) {
							Flip ();
						}
					} else {
						if (position.x >= enemyScript.transform.position.x) {
							Flip ();
						}
			
					}
	}

	void Block(){
		//toggle shield on/off
		shieldUp = !shieldUp;

		GameObject ChildGameObject = this.gameObject.transform.GetChild (0).gameObject;
		ChildGameObject.GetComponent<SpriteRenderer> ().enabled = shieldUp;
	}

	void Dash ()
	{
		if (onRightSide) {
			rb2d.AddForce (new Vector2 (-dashSpeed, 0));
		} else {
			rb2d.AddForce (new Vector2 (dashSpeed, 0));
		}
	}

	void Jump(){
		//check player is grounded before jumping
		if (grounded) {
			rb2d.velocity = new Vector2 (
				rb2d.velocity.x, jump);
		}
	}

	void FixedUpdate ()
	{
		if (grounded) {
			moveVelocity = 0;

			if (Input.GetKey (KeyCode.A)) {
				moveVelocity = -speed;	//move left

			}
			if (Input.GetKey (KeyCode.D)) {
				moveVelocity = speed;	//move right

			}
			if (Input.GetKey (KeyCode.S) && Time.time > nextDash) {
				nextDash = Time.time + dashCooldown;
				Dash ();					//dash
				return;
			}
			rb2d.velocity = new Vector2 (moveVelocity, 
				rb2d.velocity.y);
		}
	}

	private void LaunchAttack (Collider2D col)
	{
		Collider2D[] cols = Physics2D.OverlapBoxAll (
			col.bounds.center, 
			col.bounds.extents, 
			0, 
			LayerMask.GetMask ("Hitbox"));

		foreach (Collider2D c in cols) {
			if (c.transform.parent.parent == transform || enemyScript.shieldUp == true) {
				continue;
			}
			Debug.Log ("Player Two Wins!");
			GameOver ();
		}
	}

	void GameOver ()
	{
		SceneManager.LoadScene ("MainScene", LoadSceneMode.Single);
	}

	void OnTriggerEnter2D (Collider2D coll)
	{
		//check if floor or other player
		if (coll.transform.tag.Contains ("Ground")) {
			grounded = true;
		}
	}

	void OnTriggerExit2D (Collider2D coll)
	{
		//check if floor or other player
		if (coll.transform.tag.Contains ("Ground")) {
			grounded = false;
		}
	}

	void Flip ()
	{
		//flip both charcters
		transform.Rotate (new Vector3 (0, 180, 0));
		enemyScript.transform.Rotate (new Vector3 (0, 180, 0));
		enemyScript.onRightSide = !enemyScript.onRightSide;
		onRightSide = !onRightSide;

	}
}











//
//
//
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//
//public class PlayerTwoBehaviour : MonoBehaviour
//{
//	public GameObject enemy;
//	private PlayerOneBehaviour enemyScript;
//	public int dashSpeed;
//	public float jump;
//	public float speed;
//	public Collider2D[] attackHitboxes;
//	public bool shieldUp = false;
//	private bool onRightSide = false;
//	private float moveVelocity;
//	private bool grounded = true;
//	//private bool facingLeft = false;
//	private Rigidbody2D rb2d;
//
//	private float nextDash = 1;
//	public float dashCooldown = 2;
//	public float dashTime = 1;
//	private bool dashing;
//
//
//	// Use this for initialization
//	void Start ()
//	{
//		rb2d = GetComponent<Rigidbody2D> ();
//		enemyScript = enemy.GetComponent<PlayerOneBehaviour> ();
//
//	}
//
//	void Update ()
//	{
//
//		//jump
//		if (Input.GetKey (KeyCode.W)) {
//			if (grounded) {
//				rb2d.velocity = new Vector2 (
//					rb2d.velocity.x, jump);
//			}
//		} else if (Input.GetKeyDown (KeyCode.LeftShift) && shieldUp == false) {
//
//			LaunchAttack (attackHitboxes [1]);	//melee
//		} else if (Input.GetKeyDown (KeyCode.E)) {
//			
//			shieldUp = !shieldUp;		
//
//			GameObject ChildGameObject = this.gameObject.transform.GetChild (0).gameObject;
//			ChildGameObject.GetComponent<SpriteRenderer> ().enabled = shieldUp;
//
//		} 
//		
//			
//		//check if players have passed each other for flip
//		Vector3 position = transform.position;
//
//
//		if (onRightSide) {
//			if (position.x < enemyScript.transform.position.x) {
//				Flip ();
//			}
//		} else {
//			if (position.x >= enemyScript.transform.position.x) {
//				Flip ();
//			}
//		
//		}
//
//	}
//
//	void Dash ()
//	{
//		if (onRightSide) {
//			rb2d.AddForce (new Vector2 (-dashSpeed, 0));
//		} else {
//			rb2d.AddForce (new Vector2 (dashSpeed, 0));
//		}
//	}
//
//
//	void FixedUpdate ()
//	{
//		//		Horizontal movement
//
//		//					Alternative way
//		//					float move = Input.GetAxis("HorizontalP1");
//		//					rb2d.AddForce (Vector2.right * move * moveVelocity);
//
//		//		if grounded apply force
//		if (grounded) {
//
//			moveVelocity = 0;
//
//			if (Input.GetKey (KeyCode.A)) {
//				moveVelocity = -speed;	//move left
//
//			}
//			if (Input.GetKey (KeyCode.D)) {
//				moveVelocity = speed;	//move right
//			}
//			if (Input.GetKey (KeyCode.Q) && Time.time > nextDash) {
//				nextDash = Time.time + dashCooldown;
//				Dash ();					//dash
//				return;
//			}
//			rb2d.velocity = new Vector2 (moveVelocity, 
//				rb2d.velocity.y);
//
//
//		}
//	}
//
//	private void LaunchAttack (Collider2D col)
//	{
//		Collider2D[] cols = Physics2D.OverlapBoxAll (
//			                    col.bounds.center, 
//			                    col.bounds.extents, 
//			                    0, 
//			                    LayerMask.GetMask ("Hitbox"));
//
//		foreach (Collider2D c in cols) {
//			if (c.transform.parent.parent == transform || enemyScript.shieldUp == true) {
//				continue;
//			}
//			Debug.Log ("Player Two Wins!");
//
//			GameOver ();
//		}
//
//	}
//
//	void GameOver ()
//	{
//		//Application.LoadLevel(Application.loadedLevel);
//		SceneManager.LoadScene ("MainScene", LoadSceneMode.Single);
//
//	}
//
//	//	// Update is called once per frame
//	//	void Update () {
//	//
//	//		//jump
//	//		if (Input.GetKeyDown (KeyCode.UpArrow)) {
//	//			if (grounded) {
//	//				rb2d.velocity = new Vector2 (
//	//					rb2d.velocity.x, jump);
//	//			}
//	//		} else if (Input.GetKeyDown (KeyCode.RightShift)) {
//	//			LaunchAttack(attackHitboxes[0]);	//melee
//	//		}
//	//
//	//		//check if players have passed each other
//	//		Vector3 position = transform.position;
//	//
//	//
//	//		if (onRightSide == true) {
//	//			if (position.x < enemyScript.transform.position.x) {
//	//				Flip ();
//	//			}
//	//		} else {
//	//			if (position.x >= enemyScript.transform.position.x) {
//	//				Flip ();
//	//			}
//	//
//	//		}
//	////		//Melee attack
//	////		if(Input.GetKeyDown(KeyCode.RightControl)){
//	////			Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, 1.0f);
//	////			//hitObjects[0].SendMessage("TakeDamage", 1, SendMessageOptions.DontRequireReceiver);	//change to purely take damage as 1 hit kill
//	////		Debug.Log("Hit" + hitObjects[0].name);
//	////		}
//	//	}
//
//	void OnTriggerEnter2D (Collider2D coll)
//	{
//		//check if floor or other player
//		if (coll.transform.tag.Contains ("Ground")) {
//			grounded = true;
//		}
//	}
//
//	void OnTriggerExit2D (Collider2D coll)
//	{
//		//check if floor or other player
//		if (coll.transform.tag.Contains ("Ground")) {
//
//			grounded = false;
//		}
//	}
//
//
//}
