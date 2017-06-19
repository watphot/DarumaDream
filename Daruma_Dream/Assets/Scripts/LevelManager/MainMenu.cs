using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    public string startLevel;

    public string levelSelect;

    public GameObject activeToGamePlay;

    public Rename rename;

    public Rename rename2;

    public Rename rename3;

    public Rename rename4;

    //public int playerLives;

    public void NewGame()
    {

        //Application.LoadLevel(startLevel);

        StartCoroutine(startOutNewGame());

    }

    public void LevelSelect()
    {

        StartCoroutine(startOutLevelSelect());

    }

    public void Options()
    {

        StartCoroutine(startOutOptions());

    }

    public void Options2()
    {

        StartCoroutine(startOutOptions2());

    }

    public void QuitGame()
    {

        StartCoroutine(startOutQuitGame());

    }

    public IEnumerator startOutNewGame()
    {

        activeToGamePlay.SetActive(true);

        yield return new WaitForSeconds(3.5f);

        Application.LoadLevel(startLevel);

    }

    public IEnumerator startOutLevelSelect()
    {

        activeToGamePlay.SetActive(true);

        yield return new WaitForSeconds(3.5f);

        Application.LoadLevel(levelSelect);

    }

    public IEnumerator startOutOptions()
    {

        yield return new WaitForSeconds(0);

        rename.enabled = true;
        rename2.enabled = true;
        rename3.enabled = false;
        rename4.enabled = false;

        rename.framesCounter = 0;
        rename2.framesCounter = 0;
        rename3.framesCounter = 0;
        rename4.framesCounter = 0;

    }

    public IEnumerator startOutOptions2()
    {

        yield return new WaitForSeconds(0);

        rename.enabled = false;
        rename2.enabled = false;
        rename3.enabled = true;
        rename4.enabled = true;

        rename.framesCounter = 0;
        rename2.framesCounter = 0;
        rename3.framesCounter = 0;
        rename4.framesCounter = 0;

    }

    public IEnumerator startOutQuitGame()
    {

        activeToGamePlay.SetActive(true);

        yield return new WaitForSeconds(3.5f);

        Application.Quit();

    }

}
