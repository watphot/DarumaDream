using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderZoner : MonoBehaviour {

    public haruMovement haru;

	// Use this for initialization
	void Start () {

        haru = FindObjectOfType<haruMovement>();
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.name == "Haru")
        {
            haru.onLader = true;

        }

    }

    void OnTriggerExit2D(Collider2D other)
    {

        if (other.name == "Haru")
        {
            haru.onLader = false;

        }

    }

}
