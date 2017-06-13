using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ninjaController : MonoBehaviour {

    public float speed;
    public Rigidbody2D myRigidBody2D;

    public GameObject enemyDeatEffect;
    public GameObject impactEffect;

    public int pointsForKill;

    public float rotationSpeed;

    public haruMovement haru;
    public int damagetogive;

	// Use this for initialization
	void Start () {

        myRigidBody2D = GetComponent<Rigidbody2D>();
        haru = FindObjectOfType<haruMovement>();

        if (haru.transform.localScale.x < 0)
        {
            speed = -speed;
            rotationSpeed = -rotationSpeed;
        }
	}
	
	// Update is called once per frame
	void Update () {

        myRigidBody2D.velocity = new Vector2(speed, myRigidBody2D.velocity.y);
        myRigidBody2D.angularVelocity = rotationSpeed;
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Enemy")
        {

            /*Instantiate(enemyDeatEffect, other.transform.position, other.transform.rotation);
            Destroy(other.gameObject);
            ScoreManager.AddPoints(pointsForKill);*/
            other.GetComponent<enemyHealthManager>().GiveDamage(damagetogive);

        }

        if(other.tag == "Boss")
        {

            /*Instantiate(enemyDeatEffect, other.transform.position, other.transform.rotation);
            Destroy(other.gameObject);
            ScoreManager.AddPoints(pointsForKill);*/
            other.GetComponent<BossHealhtManager>().GiveDamage(damagetogive);

        }

        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);

    }
}
