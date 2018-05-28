using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseGUI : MonoBehaviour
{




	public Button soundButton;
	public Sprite soundEnabledImage;
	public Sprite soundDisabledImage;

	float timeScaleBeforePause;

	public void Update ()
	{
		if (SoundsManager.audioEnabled == false) {
			soundButton.image.sprite = soundDisabledImage;
		} else {
			soundButton.image.sprite = soundEnabledImage;
		}
	}

	public void OnCloseButtonClick ()
	{
		Deactivate (timeScaleBeforePause);
	}

	public void Activate ()
	{
		timeScaleBeforePause = Time.timeScale;
		Time.timeScale = 0;
		gameObject.SetActive (true);
	}

	public void Deactivate (float newTimeScale)
	{
		Time.timeScale = newTimeScale;
		gameObject.SetActive (false);
	}

	public void onHomeButtonClick ()
	{
		Deactivate (1);
		Application.LoadLevel (Application.loadedLevel);
	}

  

	public void OnSoundButtonClick ()
	{
		if (SoundsManager.audioEnabled == false) {
			SoundsManager.audioEnabled = true;
		} else {
			SoundsManager.audioEnabled = false;
		}
	}




}
