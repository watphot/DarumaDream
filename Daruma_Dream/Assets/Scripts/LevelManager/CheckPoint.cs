﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    public LevelManager MyLevelManager;

    // Update is called once per frame
    void Start () {

        MyLevelManager = FindObjectOfType<LevelManager>();

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if(other.tag == "Player")
        {

            MyLevelManager.currentCheckPoint = gameObject;

        }

    }
}
