using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

    #region publicVariables

    #region move

    public float maxSpeed;
    public float playerSpeed;
    public float jumpSpeed;

    public bool grounded;
    public bool doubleJump;

    #endregion

    #region playerStates

    public int curHealth;

    public int maxHealth;

    public LifeUI lifeUI;

    #endregion

    #endregion

    #region privateVariables

    private Animator anim;
    private Rigidbody2D myRigidbody;

    #endregion

    #region awakeVariables

    void Awake ()
    {

        playerSpeed = 50f;
        maxSpeed = 5f;
        anim = GetComponent<Animator>();
        curHealth = maxHealth;
        myRigidbody = GetComponent<Rigidbody2D>();

        //lifeUI.SetLifeUI(curHealth);

	}

    #endregion

    // Update is called once per frame
    void FixedUpdate ()
    {

        #region animation

        anim.SetBool("Grounded", grounded);
        anim.SetFloat("Speed", Mathf.Abs(myRigidbody.velocity.x));

        if (Input.GetAxis("Horizontal") < -0.1f)
        {
           
            transform.localScale = new Vector3(-1, 1, 1);

        }

        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            
            transform.localScale = new Vector3(1, 1, 1);

        }

        #endregion

        #region movement

        Vector3 easyVelocity = myRigidbody.velocity;

        easyVelocity.y = myRigidbody.velocity.y;
        easyVelocity.z = 0.0f;
        easyVelocity.x *= 0.75f;

        float h = Input.GetAxis("Horizontal");

        //Fake friction
        if (grounded)
        {

            myRigidbody.velocity = easyVelocity;

        }

        //Moving thhe player
        myRigidbody.AddForce((Vector2.right * playerSpeed) * h);

        //Limiting the speed of the player
        if (myRigidbody.velocity.x > maxSpeed)
        {

            myRigidbody.velocity = new Vector2(maxSpeed, myRigidbody.velocity.y);

        }

        if (myRigidbody.velocity.x < -maxSpeed)
        {

            myRigidbody.velocity = new Vector2(-maxSpeed, myRigidbody.velocity.y);

        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (grounded)
            {
                myRigidbody.AddForce(Vector2.up * jumpSpeed);
                doubleJump = true;
            }
            else
            {
                if (doubleJump)
                {                    
                    //Clean other forces for do double jump
                    myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 0);
                    myRigidbody.AddForce(Vector2.up * jumpSpeed);
                    doubleJump = false;
                }
            }
        }

        //Stop the movement with key is not pressed
        /*if (Input.GetAxis("Horizontal") == 0 && grounded)
        {

            myRigidbody.velocity = new Vector3(0, 0, 0);

        }*/

        #endregion

        #region health

        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }

        if (curHealth <= 0)
        {
            //DIE
        }
        lifeUI.SetLifeUI(curHealth, maxHealth);

        if (Input.GetKeyDown(KeyCode.C))
        {
            ReciveDamage();
        }

        #endregion

    }

    public void MaximizeHealth()
    {
        maxHealth += 1;
    }

    public void ReciveDamage()
    {
        curHealth -= 1;
    }
}
