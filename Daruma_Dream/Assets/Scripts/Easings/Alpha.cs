using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alpha : MonoBehaviour {

    public float iniAlpha;
    public float finalAlpha;
    public float framesDuration;
    public float framesCounter;
    public Image myImage;
    public AnimationCurve hyb;

    // Use this for initialization
    void Start()
    {

        myImage = GetComponent<Image>();
        iniAlpha = myImage.color.a;
        framesDuration = 3.5f;
        framesCounter = 0;
        Time.timeScale = 1;

    }

    // Update is called once per frame
    void Update()
    {

        framesCounter += Time.deltaTime;
                
        myImage.color = new Color(myImage.color.r, myImage.color.g, myImage.color.b, Mathf.Lerp(iniAlpha, finalAlpha, hyb.Evaluate(framesCounter / framesDuration)));

        if(framesCounter >= framesDuration)
        {

            framesCounter = framesDuration;

        }
    }
}
