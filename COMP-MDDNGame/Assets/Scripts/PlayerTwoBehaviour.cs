//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//
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
    public float dashLength = 2;

	private PlayerOneBehaviour enemyScript;
	public Collider2D[] attackHitboxes;

	public bool onRightSide = false;
	public bool shieldUp = false;
	private float moveVelocity;
	private bool grounded = true;
    private bool dashing = false;
    private float startDashTime;
	private Rigidbody2D rb2d;
	private float nextDash = 1;
    private float dashStop;
    private bool onHead = false;

	// Use this for initialization
	void Start ()
	{
		rb2d = GetComponent<Rigidbody2D> ();
		enemyScript = enemy.GetComponent<PlayerOneBehaviour> ();
	}

	// Called every frame
	void Update ()
	{
        checkGrounded();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            LaunchAttack(attackHitboxes[0]);
        }
		if (Input.GetKey (KeyCode.W)) {									//	jump
			Jump ();
		}
		else if (Input.GetKeyUp (KeyCode.LeftShift) && shieldUp == false) {    //melee
			GameObject ChildGameObject = this.gameObject.transform.GetChild (1).gameObject;
			ChildGameObject.GetComponent<SpriteRenderer> ().enabled = false;

		}
		else if (Input.GetKeyDown (KeyCode.LeftShift) && shieldUp == false) {    //melee
			GameObject ChildGameObject = this.gameObject.transform.GetChild (1).gameObject;
			ChildGameObject.GetComponent<SpriteRenderer> ().enabled = true;
		//	LaunchAttack (attackHitboxes [1]);	

		}
		else if (Input.GetKeyDown (KeyCode.E)) {						//block
			Block();
		}

		//check if players have passed each other
		Vector3 position = transform.position;

		if (onRightSide == true) {
			if (position.x < enemyScript.transform.position.x) {
				Flip ();
			}
		} else {
            if (position.x >= enemyScript.transform.position.x)
            {
                Flip();
            }
		}
	}

	// Called every frame 
	void FixedUpdate ()
	{
        if(dashing){
            GameObject ChildGameObject = this.gameObject.transform.GetChild(1).gameObject;
            ChildGameObject.GetComponent<SpriteRenderer>().enabled = true;
            LaunchAttack (attackHitboxes [0]);  
            if(Time.time > dashStop){
                ChildGameObject.GetComponent<SpriteRenderer>().enabled = false;
                dashing = false;
                grounded = true;
            }
            return;
        }
		if (grounded) {
			moveVelocity = 0;

			if (Input.GetKey (KeyCode.A)) {
				moveVelocity = -speed;							  			//move left

			}
			if (Input.GetKey (KeyCode.D)) {
				moveVelocity = speed;										//move right

			}
			if (Input.GetKey (KeyCode.S) && Time.time > nextDash && !dashing) {
				nextDash = Time.time + dashCooldown;
				Dash ();													//dash
				return;
			}
			rb2d.velocity = new Vector2 (moveVelocity, 
				rb2d.velocity.y);
		}
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        //check if floor or other player
        if (coll.transform.tag.Contains("Ground") || coll.transform.tag.Contains("Head"))
        {
            grounded = true;
        }
    }

	void OnTriggerExit2D (Collider2D coll)
	{
		//check if floor or other player
        if (coll.transform.tag.Contains ("Ground") || coll.transform.tag.Contains ("Head")) {
			grounded = false;
		}
	}

    private void checkGrounded()
    {
        Debug.Log("Y value = " + gameObject.transform.position.y);
        if (gameObject.transform.position.y < 1.35)
        {
            grounded = true;

        }
    }

	private void Block(){
		//toggle shield on/off
		shieldUp = !shieldUp;

		GameObject ChildGameObject = this.gameObject.transform.GetChild (0).gameObject;
		ChildGameObject.GetComponent<SpriteRenderer> ().enabled = shieldUp;
	}

	private void Dash ()
    {
        if (dashing)
        {
            return;
        }
        if (shieldUp)
        {
            Block();
        }
      
		if (onRightSide) {
			rb2d.AddForce (new Vector2 (-dashSpeed, 0));
		} else {
			rb2d.AddForce (new Vector2 (dashSpeed, 0));
		}
        dashing = true;
        dashStop = Time.time + dashLength;
       // startDashTime = Time.time;
        Debug.Log("In dash");
	}

	private void Jump(){
		//check player is grounded before jumping
		if (grounded) {
			rb2d.velocity = new Vector2 (
				rb2d.velocity.x, jump);
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

	void Flip ()
	{
		//flip both charcters
		transform.Rotate (new Vector3 (0, 180, 0));
		enemyScript.transform.Rotate (new Vector3 (0, 180, 0));
		enemyScript.onRightSide = !enemyScript.onRightSide;
		onRightSide = !onRightSide;
	}

	private void GameOver ()
	{
		SceneManager.LoadScene ("FinalMainScene", LoadSceneMode.Single);
	}
}
