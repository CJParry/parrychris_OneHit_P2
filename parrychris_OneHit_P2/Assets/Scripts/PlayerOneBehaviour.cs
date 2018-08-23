using UnityEngine;

using UnityEngine.SceneManagement;

public class PlayerOneBehaviour : MonoBehaviour
{

    //Public variables can be set in Unity Inspector
    public int dashSpeed;
    public float dashCooldown = 2;
    public float dashLength = 2;

    public float jump;
    public float playerSpeed;

    public GameObject enemy;
    public Camera cam;
    public Canvas canvas;
    public Collider2D[] attackHitboxes;
    public Animator animator;

    private Rigidbody2D rb2d;
    private PlayerTwoBehaviour enemyScript;

    private bool grounded = true;
    private bool dashing = false;
    private bool shieldUp = false;

    //Is the player on the right hand side of the other player?
    private bool onRightSide = true;
    //Used to calculate movement vector for horizontal movement
    private float moveVelocity;

    //Time the players next dash is available
    private float nextDash = 1;
     //Time the player will stop dashing
    private float dashStop;
    
    // Use this for initialization
    void Start()
    {
        //Initializes the players RigidBody, Animator and EnemyScript
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemyScript = enemy.GetComponent<PlayerTwoBehaviour>();
    }

    // Called every frame
    void Update()
    {
        //Check if player is on ground, set variable if so
        checkGrounded();

        //  Jump
        if (Input.GetKey(KeyCode.UpArrow))             
        {   //sets animator variables to true
            animator.SetBool("isJumping", true);        
            animator.SetBool("Grounded", false);
            Jump();
        }
        //Melee
        //There are 3 melee cases - KeyUp, KeyDown, Key
        else if (Input.GetKeyUp(KeyCode.RightShift) && shieldUp == false)
        {    //KeyUp is when releasing melee, turning off animation
            animator.SetBool("Jab_attack", false);
        }
        else if (Input.GetKeyDown(KeyCode.RightShift) && shieldUp == false)
        {    //KeyDown is beginning of melee animation and launchAttack
            animator.SetBool("Jab_attack", true);    
            LaunchAttack(attackHitboxes[0]);
        }
        else if (Input.GetKey(KeyCode.RightShift))
        {    //Key is when holding attack
             //May remove this option in future
            LaunchAttack(attackHitboxes[0]);
        }
        //Block 
        else if (Input.GetKeyDown(KeyCode.RightAlt))
        {                       
            Block();
        }
        //Restart button (for development)
        else if (Input.GetKey(KeyCode.Delete))
        {
            GameOver();
        }
        //Check && flip players if they have passed each other
        CheckFlip();
    }

   

    // Called every frame 
    void FixedUpdate()
    {
        if (dashing)
        {
            LaunchAttack(attackHitboxes[0]);
            if (Time.time > dashStop)
            {
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
                moveVelocity = -playerSpeed;                                      //move left

            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                moveVelocity = playerSpeed;                                       //move right

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

    private void CheckFlip()
    {
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
        //GameObject child = canvas.transform.GetChild(0).gameObject;
       // child.gameObject.GetComponent<Text>().enabled = true;
        SceneManager.LoadScene("FinalMainScene", LoadSceneMode.Single);
    }

    public bool getOnRightSide()
    {
        return onRightSide;
    }

    public bool getShieldUp()
    {
        return shieldUp;
    }

    public void setOnRightSide()
    {
        onRightSide = !onRightSide;
    }

    public void setShieldUp(bool shield)
    {
        shieldUp = shield;
    }
}
