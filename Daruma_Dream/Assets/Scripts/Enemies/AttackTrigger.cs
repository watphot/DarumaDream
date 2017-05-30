using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {

    public int dmg;
    public Magician mag;

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.CompareTag("Enemy"))
        {
            mag.Damage(dmg);
        }

        /*if (!col.isTrigger && col.CompareTag("Enemy"))
        {
            mag.Damage(dmg);
            //col.SendMessageUpwards("Damage", dmg);
        }*/
    }

}
