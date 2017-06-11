using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyerEnemyMove : MonoBehaviour {

    private haruMovement haru;
    public float moveSpeed;
    public float playerRange;
    public LayerMask playerLayer;
    public bool playerInRange;
    public bool facingAway;
    public bool followOnLookAway;

	// Use this for initialization
	void Start () {

        haru = FindObjectOfType<haruMovement>();
		
	}
	
	// Update is called once per frame
	void Update () {

        playerInRange = Physics2D.OverlapCircle(transform.position, playerRange, playerLayer);

        if (!followOnLookAway)
        {

            if (playerInRange)
            {

                transform.position = Vector3.MoveTowards(transform.position, haru.transform.position, moveSpeed * Time.deltaTime);
                return;
            }

        }

        if ((haru.transform.position.x < transform.position.x && haru.transform.localScale.x < 0) || (haru.transform.position.x > transform.position.x && haru.transform.localScale.x > 0))
        {

            facingAway = true;

        }
        else
        {
            facingAway = false;

        }

        if (playerInRange && facingAway)
        {

            transform.position = Vector3.MoveTowards(transform.position, haru.transform.position, moveSpeed * Time.deltaTime);

        }


    }

    void OnDrawGizmosSelected()
    {

        Gizmos.DrawSphere(transform.position, playerRange);

    }
}
