using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

    private PlayerMovement player;

    void Awake()
    {

        player = GetComponentInParent<PlayerMovement>();

    }

    void OnTriggerStay2D(Collider2D collision)
    {

        player.grounded = true;

    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        player.grounded = true;

    }

    void OnTriggerExit2D(Collider2D collision)
    {

        player.grounded = false;

    }

}
