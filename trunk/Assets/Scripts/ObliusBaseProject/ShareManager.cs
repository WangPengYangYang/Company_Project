using UnityEngine;
using System.Collections;

public class ShareManager : MonoBehaviour {

    public string shareMessage;
    public static ShareManager instance;

    void Awake()
    {
        instance = this;
    }
    public void share()
    {
        StartCoroutine(NativeShare.ShareScreenshotWithText(shareMessage));
    }

}
