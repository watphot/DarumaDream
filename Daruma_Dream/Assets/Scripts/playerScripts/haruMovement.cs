using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class haruMovement : MonoBehaviour {

    public float moveSpeed;
    public float jumpHeight;
    public Rigidbody2D _myrigidbody2D;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool _grounded;
    private float _moveVelocity;

    private bool _doubleJump;

    public static int curHealth;
    public int maxHealth;
    public LifeUI lifeUI;

    public Transform firePoint;
    public GameObject ninjaStart;

    public float shotDelay;
    private float shotDelayCounter;

    public float knockBack;
    public float knockBackLenght;
    public float knockBackCount;
    public bool knockFromRight;

    [SerializeField]
    private Animator _anim;
    [SerializeField]
    private LevelManager _myLevelManager;

    public float swordCounter;
    public bool change;

    // Use this for initialization
    void Awake ()
    {
        _myrigidbody2D = GetComponent<Rigidbody2D>();
        curHealth = maxHealth;
        _anim = GetComponent<Animator>();
        _myLevelManager = FindObjectOfType<LevelManager>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {

        _grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }

        if (curHealth <= 0)
        {
            _myLevelManager.RespawnPlayer();
        }
        lifeUI.SetLifeUI(curHealth, maxHealth);

        if (Input.GetKeyDown(KeyCode.C))
        {
            ReciveDamage();
        }

    }

    void Update()
    {

        if (_grounded)
        {

            _doubleJump = false;

        }

        _anim.SetBool("Grounded", _grounded);

        if (Input.GetKeyDown(KeyCode.Space) && _grounded)
        {
            _myrigidbody2D.velocity = new Vector2(_myrigidbody2D.velocity.x, jumpHeight);
        }

        if (Input.GetKeyDown(KeyCode.Space) && !_doubleJump && !_grounded)
        {
            _myrigidbody2D.velocity = new Vector2(_myrigidbody2D.velocity.x, jumpHeight);
            _doubleJump = true;
        }

        _moveVelocity = 0f;

        if (Input.GetKey(KeyCode.D))
        {
            //_myrigidbody2D.velocity = new Vector2(moveSpeed, _myrigidbody2D.velocity.y);

            _moveVelocity = moveSpeed;

        }

        if (Input.GetKey(KeyCode.A))
        {
            //_myrigidbody2D.velocity = new Vector2(-moveSpeed, _myrigidbody2D.velocity.y);

            _moveVelocity = -moveSpeed;    

        }

        if (knockBackCount <= 0)
        {
            _myrigidbody2D.velocity = new Vector2(_moveVelocity, _myrigidbody2D.velocity.y);
        }
        else
        {
            if (knockFromRight)
                _myrigidbody2D.velocity = new Vector2(-knockBack, knockBack);
            if(!knockFromRight)
                _myrigidbody2D.velocity = new Vector2(knockBack, knockBack);
            knockBackCount -= Time.deltaTime;
        }

        _anim.SetFloat("Speed", Mathf.Abs(_myrigidbody2D.velocity.x));

        if (_myrigidbody2D.velocity.x > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        else if (_myrigidbody2D.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            Instantiate(ninjaStart, firePoint.position, firePoint.rotation);
            shotDelayCounter = shotDelay;
        }

        if (Input.GetKey(KeyCode.Return))
        {
            shotDelayCounter -= Time.deltaTime;

            if (shotDelayCounter <= 0)
            {
                shotDelayCounter = shotDelay;
                Instantiate(ninjaStart, firePoint.position, firePoint.rotation);
            }

        }

        if (_anim.GetBool("Attack"))
        {
            swordCounter += Time.deltaTime;
            if (swordCounter >= 0.2)
            {
                _anim.SetBool("Attack", false);
                swordCounter = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.V) && swordCounter == 0)
        {

            _anim.SetBool("Attack", true);

        }

    }

    public void MaximizeHealth()
    {
        curHealth = maxHealth;
    }

    public void ReciveDamage()
    {
        curHealth -= 1;
    }
}
