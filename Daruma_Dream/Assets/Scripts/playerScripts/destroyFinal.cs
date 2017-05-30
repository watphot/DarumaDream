using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyFinal : MonoBehaviour {

	private ParticleSystem _myParticleSystem;

	// Use this for initialization
	void Start () {

		_myParticleSystem = GetComponent<ParticleSystem> ();

	}
	
	// Update is called once per frame
	void Update () {

		if (_myParticleSystem.isPlaying)
			return;

		Destroy (gameObject);
		
	}
}
