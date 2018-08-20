using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoBehaviour : MonoBehaviour {
	public GameObject enemy;
	private PlayerOneBehaviour enemyScript;
	public float jump;
	public float speed;
	public Collider2D[] attackHitboxes;
	public bool shieldUp = false;
	private bool onRightSide = false;
	private float moveVelocity;
	private bool grounded = true;
	//private bool facingLeft = false;
	private Rigidbody2D rb2d;


	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		enemyScript = enemy.GetComponent<PlayerOneBehaviour> ();

	}

	void Update(){

		//jump
		if (Input.GetKey (KeyCode.W)) {
			if (grounded) {
				rb2d.velocity = new Vector2 (
					rb2d.velocity.x, jump);
			}
		}
		else if (Input.GetKeyDown (KeyCode.LeftShift)) {

			LaunchAttack(attackHitboxes[1]);	//melee
		}
			
		//check if players have passed each other for flip
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


	void FixedUpdate(){
		//		Horizontal movement

		//					Alternative way
		//					float move = Input.GetAxis("HorizontalP1");
		//					rb2d.AddForce (Vector2.right * move * moveVelocity);

		//		if grounded apply force
		if (grounded) {

			moveVelocity = 0;

			if (Input.GetKey (KeyCode.A)) {
				moveVelocity = -speed;	//move left

			}
			if (Input.GetKey (KeyCode.D)) {
				moveVelocity = speed;	//move right
			}
			rb2d.velocity = new Vector2 (moveVelocity, 
				rb2d.velocity.y);


		}
	}

	private void LaunchAttack(Collider2D col){
		Collider2D[] cols = Physics2D.OverlapBoxAll(
			col.bounds.center, 
			col.bounds.extents, 
			0, 
			LayerMask.GetMask("Hitbox"));

		foreach (Collider2D c in cols) {
			if (c.transform.parent.parent == transform ){//|| enemyScript.shieldUp == true) {
				continue;
			}
			Debug.Log ("Player Two Wins!");

			GameOver ();
		}

	}

	void GameOver(){
		Application.LoadLevel(Application.loadedLevel);

	}

	//	// Update is called once per frame
	//	void Update () {
	//
	//		//jump
	//		if (Input.GetKeyDown (KeyCode.UpArrow)) {
	//			if (grounded) {
	//				rb2d.velocity = new Vector2 (
	//					rb2d.velocity.x, jump);
	//			}
	//		} else if (Input.GetKeyDown (KeyCode.RightShift)) {
	//			LaunchAttack(attackHitboxes[0]);	//melee
	//		}
	//
	//		//check if players have passed each other
	//		Vector3 position = transform.position;
	//
	//
	//		if (onRightSide == true) {
	//			if (position.x < enemyScript.transform.position.x) {
	//				Flip ();
	//			}
	//		} else {
	//			if (position.x >= enemyScript.transform.position.x) {
	//				Flip ();
	//			}
	//
	//		}
	////		//Melee attack
	////		if(Input.GetKeyDown(KeyCode.RightControl)){
	////			Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, 1.0f);
	////			//hitObjects[0].SendMessage("TakeDamage", 1, SendMessageOptions.DontRequireReceiver);	//change to purely take damage as 1 hit kill
	////		Debug.Log("Hit" + hitObjects[0].name);
	////		}
	//	}

	void OnTriggerEnter2D(Collider2D coll){
		//check if floor or other player
		if(coll.transform.tag.Contains("Ground")){
			grounded = true;
		}
	}
	void OnTriggerExit2D(Collider2D coll){
		//check if floor or other player
		if(coll.transform.tag.Contains("Ground")){

			grounded = false;
		}
	}

	void Flip(){
		//flip both charcters
		transform.Rotate(new Vector3(0,180,0));
		enemyScript.transform.Rotate(new Vector3(0,180,0));
		enemyScript.onRightSide = !enemyScript.onRightSide;
		onRightSide = !onRightSide;

	}
}
