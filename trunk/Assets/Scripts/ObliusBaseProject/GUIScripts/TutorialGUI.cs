using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialGUI : MonoBehaviour
{

    string tutorialShownPlayerPrefsString = "TUTORIALSHOWN";


    public Button soundButton;
    public Sprite soundEnabledImage;
    public Sprite soundDisabledImage;



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCloseButtonClick()
    {
        SoundsManager.instance.PlayMenuButtonSound();
        Deactivate();
    }

    public void onHomeButtonClick()
    {
        Application.LoadLevel(Application.loadedLevel);
        Deactivate();
    }

    public void OnSoundButtonClick()
    {
        if (AudioListener.volume == 0)
        {
            soundButton.image.sprite = soundEnabledImage;
            AudioListener.volume = 1;
        }
        else
        {
            soundButton.image.sprite = soundDisabledImage;
            AudioListener.volume = 0;
        }
    }

    public void Activate()
    {
        PlayerPrefs.SetInt(tutorialShownPlayerPrefsString, 1);
        Time.timeScale = 0;
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public bool tutorialShown()
    {
        int getInt = PlayerPrefs.GetInt(tutorialShownPlayerPrefsString, 0);
        if (getInt == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void ShowIfNeverAppeared()
    {
        if (!tutorialShown())
        {
            Activate();
        }
    }

}
