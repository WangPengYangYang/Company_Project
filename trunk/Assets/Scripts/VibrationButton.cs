using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VibrationButton : MonoBehaviour {
    public static VibrationButton instance;
    string PlayerPrefSavedVibration = "Vibration_Mode";
    public static bool vibrationEnabled = true;
   public Image buttonImage;
    public Sprite[] vibrationSprites;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
       int value = PlayerPrefs.GetInt(PlayerPrefSavedVibration, 1);
        if (value == 0) { vibrationEnabled = false; } else 
        { vibrationEnabled = true; }
    }
    private void Update()
    {
        if (vibrationEnabled) { buttonImage.sprite = vibrationSprites[0]; } else {
            buttonImage.sprite = vibrationSprites[1];
        }
    }
    public void ToggleVibrationMode()
    {
        vibrationEnabled = !vibrationEnabled;
        int val = 0;
        if (vibrationEnabled)
        {
            val = 1;

                Handheld.Vibrate();
        
        }
        PlayerPrefs.SetInt(PlayerPrefSavedVibration, val);
    }

}
