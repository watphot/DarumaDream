using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public int screenHeight;
    public int screenWidth;
    public bool isFullscreenActive;
    public Dropdown dropdown;
    public Toggle toggle;
    public Dropdown graphicsDropdown;
    public Slider musicVolume;
    public Slider sfxVolume;
    public Slider masterVolume;

    public AudioMixer master;

    void Awake()
    {
        screenWidth = Screen.currentResolution.width;
        screenHeight = Screen.currentResolution.height;
        isFullscreenActive = Screen.fullScreen;
        toggle.isOn = isFullscreenActive;
    }
    void Update()
    {

        switch (dropdown.value)
        {
            case 0:
                ScreenRes1();
                break;

            case 1:
                ScreenRes2();
                break;

            case 2:
                ScreenRes3();
                break;

            case 3:
                ScreenRes4();
                break;
        }

        switch (graphicsDropdown.value)
        {
            case 0:
                FastestSettings();
                break;

            case 1:
                SimpleSettings();
                break;

            case 2:
                BeautifulSettings();
                break;

            case 3:
                FantasticSettings();
                break;
        }
    }
    public void ScreenRes1()
    {
        screenWidth = 1280;
        screenHeight = 720;
        UpdateScreenRes();
    }
    public void ScreenRes2()
    {
        screenWidth = 1366;
        screenHeight = 768;
        UpdateScreenRes();
    }
    public void ScreenRes3()
    {
        screenWidth = 1600;
        screenHeight = 900;
        UpdateScreenRes();
    }
    public void ScreenRes4()
    {
        screenWidth = 1920;
        screenHeight = 1080;
        UpdateScreenRes();
    }
    public void Fullscreen()
    {
        if (isFullscreenActive == false)
        {
            isFullscreenActive = true;
            toggle.isOn = true;
            UpdateScreenRes();
        }
        else if (isFullscreenActive == true)
        {
            isFullscreenActive = false;
            toggle.isOn = false;
            UpdateScreenRes();
        }
    }

    public void FastestSettings()
    {
        QualitySettings.SetQualityLevel(0);
    }

    public void SimpleSettings()
    {
        QualitySettings.SetQualityLevel(2);
    }

    public void BeautifulSettings()
    {
        QualitySettings.SetQualityLevel(4);
    }

    public void FantasticSettings()
    {
        QualitySettings.SetQualityLevel(5);
    }

    public void SetMusicVol(float musiclvl)
    {
        master.SetFloat("Music", musiclvl);
    }

    public void SetSFXVol(float sfxlvl)
    {
        master.SetFloat("SFX", sfxlvl);
        //Poner UN SFX para que suene

    }
    public void SetMasterVol(float masterlvl)
    {
        master.SetFloat("Master", masterlvl);
    }
    public void UpdateScreenRes()
    {
        Screen.SetResolution(screenWidth, screenHeight, isFullscreenActive);
    }
}