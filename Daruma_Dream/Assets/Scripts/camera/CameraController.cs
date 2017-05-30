using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public haruMovement haru;

    public bool isFollowing;

    public float xOffset;
    public float yOffset;

	// Use this for initialization
	void Start () {

        haru = FindObjectOfType<haruMovement>();

        isFollowing = true;
		
	}
	
	// Update is called once per frame
	void Update () {

        if(isFollowing) transform.position = new Vector3(haru.transform.position.x + xOffset, haru.transform.position.y + yOffset, transform.position.z);
		
	}
}
