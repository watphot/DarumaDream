using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol1 : MonoBehaviour {

    public float moveSpeed;
    public bool moveRight;
    private Rigidbody2D _myRB2D;

    public Transform wallCheck;
    public float wallCheckRadius;
    public LayerMask whatIsWall;
    private bool hittingWall;

    private bool atEdge;
    public Transform edgeCheck;

    private float ySize;

	// Use this for initialization
	void Start () {

        _myRB2D = GetComponent<Rigidbody2D>();

        ySize = transform.localScale.y;

    }
	
	// Update is called once per frame
	void Update () {

        hittingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);

        atEdge = Physics2D.OverlapCircle(edgeCheck.position, wallCheckRadius, whatIsWall);

        if (hittingWall || !atEdge) moveRight = !moveRight;

        if (moveRight)
        {
            transform.localScale = new Vector3(-ySize, transform.localScale.y, transform.localScale.z); //El -19 se podria cambiar des de fuera, haciendo la imagen tan grande como se quiera y dejando el empty a 1.
            _myRB2D.velocity = new Vector2(moveSpeed, _myRB2D.velocity.y);
        }
        else
        {
            transform.localScale = new Vector3(ySize, transform.localScale.y, transform.localScale.z);
            _myRB2D.velocity = new Vector2(-moveSpeed, _myRB2D.velocity.y);
        }
	}
}
