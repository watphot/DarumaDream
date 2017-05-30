using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Cone : MonoBehaviour {

    public Magician magician;

    public bool isLeft = false;

    void Awake()
    {
        magician = gameObject.GetComponentInParent<Magician>();
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (isLeft)
            {
                magician.Attack(false); //Becouse player is at left
            }
            else
            {
                magician.Attack(true);
            }
        }
    }
}
