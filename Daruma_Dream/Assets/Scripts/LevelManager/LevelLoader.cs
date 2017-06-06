using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour {

    private bool _playerInZone;

    public string levelToLoad;

	// Use this for initialization
	void Start () {

        _playerInZone = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {

            Debug.Log("Nya");

        }

    }
}
