using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneBehaviour : MonoBehaviour {
	public GameObject enemy;
	private PlayerTwoBehaviour enemyScript;
	public float jump;
	public float speed;
	private float moveVelocity;
	private bool grounded = true;
	private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		enemyScript = enemy.GetComponent<PlayerTwoBehaviour> ();
	}

	void FixedUpdate(){
//		Horizontal movement

//					Alternative way
//					float move = Input.GetAxis("HorizontalP1");
//					rb2d.AddForce (Vector2.right * move * moveVelocity);

//		if grounded apply force
		if (grounded) {
	
			moveVelocity = 0;

			if (Input.GetKey (KeyCode.LeftArrow)) {
				moveVelocity = -speed;	//move left
			}
			if (Input.GetKey (KeyCode.RightArrow)) {
				moveVelocity = speed;	//move right
			}
			rb2d.velocity = new Vector2 (moveVelocity, 
				rb2d.velocity.y);
		}

	}

	// Update is called once per frame
	void Update () {
		//jump

		if (Input.GetKeyDown (KeyCode.UpArrow)){
			if (grounded) {
				rb2d.velocity = new Vector2 (
					rb2d.velocity.x, jump);
			}
		}


	}

	void OnTriggerEnter2D(){
		grounded = true;
	}
	void OnTriggerExit2D(){
		grounded = false;
	}
}
