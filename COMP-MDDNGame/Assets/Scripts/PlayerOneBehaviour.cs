using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerOneBehaviour : MonoBehaviour
{
	public GameObject enemy;
	public float jump;
	public float speed;
	public int dashSpeed;
	public float dashCooldown = 2;
    public float dashLength = 0.5f;


	private PlayerTwoBehaviour enemyScript;
	public Collider2D[] attackHitboxes;

	public bool onRightSide = true;
	public bool shieldUp = false;
	private float moveVelocity;
	private bool grounded = true;
    private bool dashing = false;
    private float startDashTime;
	private Rigidbody2D rb2d;
	private float nextDash = 1;
    private float dashStop;

	// Use this for initialization
	void Start ()
	{
		rb2d = GetComponent<Rigidbody2D> ();
		enemyScript = enemy.GetComponent<PlayerTwoBehaviour> ();
	}

	// Called every frame
	void Update ()
	{
		if (Input.GetKey (KeyCode.RightShift)) {
			LaunchAttack (attackHitboxes [0]);	
		}
		if (Input.GetKey (KeyCode.UpArrow)) {									//	jump
			Jump();

		} else if (Input.GetKeyUp (KeyCode.RightShift) && shieldUp == false) {    //melee
			GameObject ChildGameObject = this.gameObject.transform.GetChild (1).gameObject;
			ChildGameObject.GetComponent<SpriteRenderer> ().enabled = false;

		}
		else if (Input.GetKeyDown (KeyCode.RightShift) && shieldUp == false) {    //melee
			GameObject ChildGameObject = this.gameObject.transform.GetChild (1).gameObject;
			ChildGameObject.GetComponent<SpriteRenderer> ().enabled = true;
			//LaunchAttack (attackHitboxes [0]);	

		}
		 else if (Input.GetKeyDown (KeyCode.RightAlt)) {						//block
			Block();
		}

	}

	// Called every frame 
	void FixedUpdate ()
	{

        if (dashing)
        {
            GameObject ChildGameObject = this.gameObject.transform.GetChild(1).gameObject;
            ChildGameObject.GetComponent<SpriteRenderer>().enabled = true;
            LaunchAttack(attackHitboxes[0]);
            if (Time.time > dashStop)
            {
                ChildGameObject.GetComponent<SpriteRenderer>().enabled = false;
                dashing = false;
            }
            return;
        }
		if (grounded) {
			moveVelocity = 0;

			if (Input.GetKey (KeyCode.LeftArrow)) {
				moveVelocity = -speed;							  			//move left

			}
			if (Input.GetKey (KeyCode.RightArrow)) {
				moveVelocity = speed;										//move right

			}
			if (Input.GetKey (KeyCode.DownArrow) && Time.time > nextDash) {
				nextDash = Time.time + dashCooldown;
				Dash ();													//dash
				return;
			}
			rb2d.velocity = new Vector2 (moveVelocity, 
				rb2d.velocity.y);
		}
	}

	void OnTriggerEnter2D (Collider2D coll)
	{
		//check if floor or other player
		if (coll.transform.tag.Contains ("Ground") || coll.transform.tag.Contains ("Head")) {
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

	private void Block(){
		//toggle shield on/off
		shieldUp = !shieldUp;

		GameObject ChildGameObject = this.gameObject.transform.GetChild (0).gameObject;
		ChildGameObject.GetComponent<SpriteRenderer> ().enabled = shieldUp;
	}

    private void Dash()
    {
        if (dashing)
        {
            return;
        }
        if (shieldUp)
        {
            Block();
        }
        if (onRightSide)
        {
            rb2d.AddForce(new Vector2(-dashSpeed, 0));
        }
        else
        {
            rb2d.AddForce(new Vector2(dashSpeed, 0));
        }
        dashing = true;
        dashStop = Time.time + dashLength;
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
			Debug.Log ("Player One Wins!");
			GameOver ();
		}
	}

	private void GameOver ()
	{
		SceneManager.LoadScene ("FinalMainScene", LoadSceneMode.Single);
	}
}
