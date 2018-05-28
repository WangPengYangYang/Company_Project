using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using Oblius.Assets.AllInOneAdnetworks;

public class OneMoreChanceGUI : MonoBehaviour {
   
	public UnityEvent onRewardVideoSuccess;
	public UnityEvent onRewardVideoFail;

	void Activate()
    {
        gameObject.SetActive(true);
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void OnOneMoreChanceButtonClick()
    {
        SoundsManager.instance.PlayMenuButtonSound();

		AdNetworksManager.instance.ShowRewardedVideo ((bool value) => {
			if(value){
				onRewardVideoSuccess.Invoke();
			}else{
				onRewardVideoFail.Invoke();
			}
		});

        Deactivate();
    }

    public void OnGameOverButtonClick()
    {
        SoundsManager.instance.PlayMenuButtonSound();

        Deactivate();
        ObliusGameManager.instance.oneMoreChanceUsed = true;
        ObliusGameManager.instance.GameOver(0);
    }
}
