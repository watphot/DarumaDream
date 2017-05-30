using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fail : MonoBehaviour {

    public Vector2 initValue;
    public Vector2 finalValue;
    public float framesDuration;
    public float framesCounter;
    public bool change;
    public Image myImage;
    public AnimationCurve hyb;

    // Use this for initialization
    void Start()
    {

        myImage = GetComponent<Image>();
        change = false;
        initValue = myImage.rectTransform.sizeDelta;
        finalValue = new Vector2 (120, 120);
        framesDuration = 0.3f;
        framesCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {


        if (!change)
        {
            framesCounter += Time.unscaledDeltaTime;
            myImage.rectTransform.sizeDelta = Vector2.Lerp(initValue, finalValue, hyb.Evaluate(framesCounter/ framesDuration));
            if (framesCounter >= framesDuration)
            {
                framesCounter = framesDuration;
                change = !change;
            }

        }

        else
        {
            framesCounter -= Time.unscaledDeltaTime;
            myImage.rectTransform.sizeDelta = Vector2.Lerp(finalValue, initValue, hyb.Evaluate(framesDuration - framesCounter)/framesDuration);
            if (framesCounter <= 0)
            {
                framesCounter = 0;
                change = !change;
            }
        } 
    }
}
