using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static int score;
    private Text _mytext;


	// Use this for initialization
	void Start () {

        _mytext = GetComponent<Text>();

        score = 0;
	}
	
	// Update is called once per frame
	void Update () {

        if(score < 0) score = 0;

        _mytext.text = "" + score;
		
	}

    public static void AddPoints(int pointsToAdd)
    {

        score += pointsToAdd;

    }

    public static void Reset()
    {

        score = 0;

    }
}
