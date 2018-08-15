using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneBehaviour : MonoBehaviour {
	public Vector2 movement;
	public bool grounded = true;
	Rigidbody2D rb2d;
	public Vector2 speed = new Vector2(5, 5);
	public int jumpHeight;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate(){
		
		rb2d.velocity = movement;
	}
	
	// Update is called once per frame
	void Update () {
		float inputX = Input.GetAxis ("HorizontalP1");
		float inputY = Input.GetAxis ("JumpP1");
		movement = new Vector2 (
			speed.x * inputX,
			speed.y * inputY * jumpHeight);
	}
}
