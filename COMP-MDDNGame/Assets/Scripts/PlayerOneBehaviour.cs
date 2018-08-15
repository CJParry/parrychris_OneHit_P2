using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneBehaviour : MonoBehaviour {

	public bool grounded = false;
	public float jump;
	public float speed;
	float moveVelocity;

	// Use this for initialization
	void Start () {
	}

	void FixedUpdate(){

	}

	// Update is called once per frame
	void Update () {
		//jump

		if (Input.GetKeyDown (KeyCode.UpArrow)){
			if (grounded) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (
					GetComponent<Rigidbody2D> ().velocity.x, jump);
			}
		}
		moveVelocity = 0;
		//Horizontal movement
		if (Input.GetKey (KeyCode.LeftArrow)) {
			moveVelocity = -speed;	//move left
		}
		if (Input.GetKey (KeyCode.RightApple)) {
			moveVelocity = speed;	//move right
		}
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveVelocity, 
			GetComponent <Rigidbody2D> ().velocity.y);
	}

	void OnTriggerEnter2D(){
		grounded = true;
	}
	void OnTriggerExit2D(){
		grounded = false;
	}
}
