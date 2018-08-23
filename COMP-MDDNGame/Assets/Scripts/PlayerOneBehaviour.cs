using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class PlayerOneBehaviour : MonoBehaviour
{
    public GameObject enemy;
    public float jump;
    public float speed; 
    public int dashSpeed;
    public float dashCooldown = 2;
    public float dashLength = 2;
    public float gameOverWait = 3;
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
    public Camera cam;
    public Canvas canvas;
    public bool gameOver = false;
    private float gameOverTime;

    public Animator animator;


    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemyScript = enemy.GetComponent<PlayerTwoBehaviour>();
    }

    // Called every frame
    void Update()
    {
        checkGrounded();
        FixedCameraFollowSmooth();
        if (Input.GetKey(KeyCode.Delete))
        {
            GameOver();
        }
        if (Input.GetKey(KeyCode.RightShift))
        {
            LaunchAttack(attackHitboxes[0]);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {                                   //  jump
            animator.SetBool("isJumping", true);        //sets animator varialbe isJumping to true
            animator.SetBool("Grounded", false);
            Jump();


        }
        else if (Input.GetKeyUp(KeyCode.RightShift) && shieldUp == false)
        {    //melee

            animator.SetBool("Jab_attack", false);    //set animator variable Jab_attack to false

        }
        else if (Input.GetKeyDown(KeyCode.RightShift) && shieldUp == false)
        {    //melee

            animator.SetBool("Jab_attack", true);    //set animator variable Jab_attack to true

            //  LaunchAttack (attackHitboxes [1]);  

        }
        else if (Input.GetKeyDown(KeyCode.RightAlt))
        {                       //block 
            Block();


        }

        //check if players have passed each other
        Vector3 position = transform.position;

        if (onRightSide == true)
        {
            if (position.x < enemyScript.transform.position.x)
            {
                Flip();
            }
        }
        else
        {
            if (position.x >= enemyScript.transform.position.x)
            {
                Flip();
            }
        }
    }

    // Called every frame 
    void FixedUpdate()
    {
        if (gameOver)
        {
            GameOver();
        }
        if (dashing)
        {
           // GameObject ChildGameObject = this.gameObject.transform.GetChild(1).gameObject;
            LaunchAttack(attackHitboxes[0]);
            if (Time.time > dashStop)
            {
                // ChildGameObject.GetComponent<SpriteRenderer>().enabled = false;
                dashing = false;
                grounded = true;

                animator.SetBool("Dash_Attack", false);     //set dash attack variable in animator to false

            }
            return;
        }
        if (grounded)
        {
            moveVelocity = 0;

            animator.SetBool("Grounded", true); //sets Grounded variable in Animator to true


            if (Input.GetKey(KeyCode.LeftArrow))
            {
                moveVelocity = -speed;                                      //move left

            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                moveVelocity = speed;                                       //move right

            }
            if (Input.GetKey(KeyCode.DownArrow) && Time.time > nextDash && !dashing)
            {
                nextDash = Time.time + dashCooldown;
                Dash();                                                 //dash

                animator.SetBool("Dash_Attack", true);  //Set dash attack variable in animator to true

                return;
            }
            rb2d.velocity = new Vector2(moveVelocity,
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

    void OnTriggerExit2D(Collider2D coll)
    {
        //check if floor or other player
        if (coll.transform.tag.Contains("Ground") || coll.transform.tag.Contains("Head"))
        {
            grounded = false;
        }
    }

    private void checkGrounded()
    {
        Debug.Log("Y value = " + gameObject.transform.position.y);
        if (gameObject.transform.position.y < 1.35)
        {
            animator.SetBool("Grounded", true);
            grounded = true;
        }
    }

    private void Block()
    {
        //toggle shield on/off
        shieldUp = !shieldUp;

        animator.SetBool("Block", shieldUp);    //sets animator variable Block to true


        GameObject ChildGameObject = this.gameObject.transform.GetChild(0).gameObject;
        ChildGameObject.GetComponent<SpriteRenderer>().enabled = shieldUp;
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
        // startDashTime = Time.time;
    }

    private void Jump()
    {
        //check player is grounded before jumping
        if (grounded)
        {
            rb2d.velocity = new Vector2(
                rb2d.velocity.x, jump);

            animator.SetBool("isGrounded", true);   //set isGrounded variable to true in animator
            animator.SetBool("isJumping", false);   //set isJumping Variable to false in animator

        }
    }

    private void LaunchAttack(Collider2D col)
    {
       
            Collider2D[] cols = Physics2D.OverlapBoxAll(
                col.bounds.center,
                col.bounds.extents,
                0,
                LayerMask.GetMask("Hitbox"));

        foreach (Collider2D c in cols)
        {
            if (c.transform.parent.parent == transform || enemyScript.shieldUp == true)
            {
                continue;
            }
            Debug.Log("Player One Wins!");
            GameOver();
        }
    }

    void Flip()
    {
        //flip both charcters
        transform.Rotate(new Vector3(0, 180, 0));
        enemyScript.transform.Rotate(new Vector3(0, 180, 0));
        enemyScript.onRightSide = !enemyScript.onRightSide;
        onRightSide = !onRightSide;
    }

    private void GameOver()
    {
        if (!gameOver)
        {
            SetGameOver();
             enemyScript.SetGameOver();
            GameObject child = canvas.transform.GetChild(0).gameObject;

            child.GetComponent<Text>().enabled = true;
        }
        else if (Time.time > gameOverTime + gameOverWait)
        {
            SceneManager.LoadScene("FinalMainScene", LoadSceneMode.Single);
        }
    }

    public void SetGameOver()
    {
        gameOver = true;
        gameOverTime = Time.time;

    }

//     Follow Two Transforms with a Fixed-Orientation Camera
        public void FixedCameraFollowSmooth()
        {
            if (cam.transform.position.z < -15.5)
            {
                cam.transform.SetPositionAndRotation(new Vector3(cam.transform.position.x, cam.transform.position.y, -15.4f), cam.transform.rotation);
            }
            if (cam.transform.position.z > -8.4)
            {
                  cam.transform.SetPositionAndRotation(new Vector3(cam.transform.position.x, cam.transform.position.y, -8.4f), cam.transform.rotation);
            }
            Transform t1 = this.gameObject.transform;
            Transform t2 = enemy.gameObject.transform;
            // How many units should we keep from the players
            float zoomFactor = 0.7f;
            float followTimeDelta = 0.2f;

            // Midpoint we're after
            Vector3 midpoint = (t1.position + t2.position) / 2f;
            // Distance between objects
            float distance = (t1.position - t2.position).magnitude;
            //midpoint.y = midpoint.y + 1.8f;


            // Move camera a certain distance
            Vector3 cameraDestination = midpoint - cam.transform.forward * distance * zoomFactor;
            cameraDestination.y += 1f;


            //Move the camera from original position to cameraDestination
            cam.transform.position = Vector3.Slerp(cam.transform.position, cameraDestination, followTimeDelta);

            // Snap when close enough to prevent annoying slerp behavior
            if ((cameraDestination - cam.transform.position).magnitude <= 0.05f)
                cam.transform.position = cameraDestination;


        }
}
