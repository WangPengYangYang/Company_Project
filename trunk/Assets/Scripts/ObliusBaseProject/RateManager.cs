using UnityEngine;
using System.Collections;

public class RateManager : MonoBehaviour
{
    public static RateManager instance;

    public string androidAppStoreUrl;
    public string iOSAppStoreUrl;


    void Awake()
    {
        instance = this;
    }


    public void rateGame()
    {

        string urlToOpen ="";

#if UNITY_ANDROID
        urlToOpen = androidAppStoreUrl;
#endif

#if UNITY_IOS
        urlToOpen = iOSAppStoreUrl;
#endif

        Application.OpenURL(urlToOpen);
    }

}
