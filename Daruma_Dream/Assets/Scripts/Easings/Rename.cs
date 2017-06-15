using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rename : MonoBehaviour {

    public Vector2 initValue;
    public float framesDuration;
    public float framesCounter;
    public Image myImage;
    public GameObject finalPosition;
    public AnimationCurve hyb;

    // Use this for initialization
    void Start()
    {

        myImage = GetComponent<Image>();
        initValue = myImage.rectTransform.position;
        framesDuration = 5f;
        framesCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {

            framesCounter += Time.deltaTime;

            myImage.rectTransform.position = Vector2.Lerp(initValue, finalPosition.transform.position, hyb.Evaluate(framesCounter / framesDuration));

            if(framesCounter >= framesDuration)
            {

                framesCounter = framesDuration;

            }
    }
}

