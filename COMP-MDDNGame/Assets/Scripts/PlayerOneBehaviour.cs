using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneBehaviour : MonoBehaviour {
	Rigidbody2D rb2d;
	public int speed;
	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate(){
		float horizontal = Input.GetAxis ("Horizontal");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
