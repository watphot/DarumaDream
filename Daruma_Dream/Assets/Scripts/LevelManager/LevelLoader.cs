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

        if (Input.GetKeyDown(KeyCode.K) && _playerInZone)
        {

            Application.LoadLevel(levelToLoad);

        }
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {

            _playerInZone = true;

        }

    }

    void OnTriggerEXit2D(Collider2D other)
    {

        if (other.tag == "Player")
        {

            _playerInZone = false;

        }

    }
}
