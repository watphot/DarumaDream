using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour {

    public float moveSpeed;
    public bool moveRight;
    private Rigidbody2D _myRB2D;

    public Transform wallCheck;
    public float wallCheckRadius;
    public LayerMask whatIsWall;
    private bool hittingWall;

    private bool atEdge;
    public Transform edgeCheck;

	// Use this for initialization
	void Start () {

        _myRB2D = GetComponent<Rigidbody2D>();


    }
	
	// Update is called once per frame
	void Update () {

        hittingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);

        atEdge = Physics2D.OverlapCircle(edgeCheck.position, wallCheckRadius, whatIsWall);

        if (hittingWall || !atEdge) moveRight = !moveRight;

        if (moveRight)
        {
            transform.localScale = new Vector3(-10f, 10f, 10f);
            _myRB2D.velocity = new Vector2(moveSpeed, _myRB2D.velocity.y);
        }
        else
        {
            transform.localScale = new Vector3(10f, 10f, 10f);
            _myRB2D.velocity = new Vector2(-moveSpeed, _myRB2D.velocity.y);
        }
	}
}
