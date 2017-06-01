using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordAttack : MonoBehaviour {
    
    public int damagetogive;
    public GameObject impactEffect;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Sword")
        {

            /*Instantiate(enemyDeatEffect, other.transform.position, other.transform.rotation);
            Destroy(other.gameObject);
            ScoreManager.AddPoints(pointsForKill);*/
            GetComponent<enemyHealthManager>().GiveDamage(damagetogive);

        }

        Instantiate(impactEffect, transform.position, transform.rotation);
        //Destroy(gameObject);

    }
}
