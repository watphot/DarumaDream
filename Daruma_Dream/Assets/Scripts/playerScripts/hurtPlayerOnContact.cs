using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hurtPlayerOnContact : MonoBehaviour {

    public int damageToGive;
    public haruMovement haru;

	// Use this for initialization
	void Start () {

        haru = FindObjectOfType<haruMovement>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {

            haru.ReciveDamage();

        }

    }
}
