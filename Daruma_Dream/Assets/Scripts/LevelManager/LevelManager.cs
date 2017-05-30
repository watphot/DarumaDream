using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public GameObject currentCheckPoint;

    private haruMovement haru;
	public GameObject haruRenderer;

	public GameObject deathParticle;

	public int pointsOnDeath;
	public float respawnDelay;

    private CameraController _camera;

	// Use this for initialization
	void Start () {

		haru = FindObjectOfType<haruMovement> ();
        _camera = FindObjectOfType<CameraController>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
   
    public void RespawnPlayer()
    {
		StartCoroutine ("RespawnHaru");
    }

	public IEnumerator RespawnHaru()
	{

		Instantiate (deathParticle, haru.transform.position, haru.transform.rotation);
		haru.enabled = false;
		haruRenderer.GetComponentInChildren<Renderer> ().enabled = false;
        _camera.isFollowing = false;
        haruRenderer.GetComponent<Rigidbody2D>().gravityScale = 0f;
        haruRenderer.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		ScoreManager.AddPoints (-pointsOnDeath);

		Debug.Log("Respawn");
		yield return new WaitForSeconds (respawnDelay);
        haruRenderer.GetComponent<Rigidbody2D>().gravityScale = 5f;
        haru.transform.position = currentCheckPoint.transform.position;
		haru.enabled = true;
        haru.MaximizeHealth();
        _camera.isFollowing = true;
        haruRenderer.GetComponentInChildren<Renderer> ().enabled = true;

	}

}
