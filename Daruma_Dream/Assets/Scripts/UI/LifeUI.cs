using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeUI : MonoBehaviour {

    #region publicVariables

    public Image[] images;
    public Sprite heartFull;
    public Sprite heartEmpty;

    #endregion

    //SetLifeUI is called once per frame
    public void SetLifeUI(int curHealth, int maxHealth)
    {

        //images = GetComponentsInChildren<Image>();

        for (int i = 0; i < images.Length; i++)
        {

            if (i < curHealth)
            {

                images[i].sprite = heartFull;

            }

            else images[i].sprite = heartEmpty;

        }

        for (int e = 0; e < images.Length; e++)
        {

            if (e < maxHealth)
            {

                images[e].enabled = true;

            }

            else images[e].enabled = false;

        }

    }

}
