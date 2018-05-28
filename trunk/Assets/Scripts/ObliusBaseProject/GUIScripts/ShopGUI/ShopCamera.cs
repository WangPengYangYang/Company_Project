using UnityEngine;
using System.Collections;

public class ShopCamera : MonoBehaviour {


    public static ShopCamera instance;
    public Camera currentCamera;

    void Awake()
    {
        instance = this;
        currentCamera = GetComponent<Camera>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CameraActive(bool activated)
    {
        if (activated)
        {
            currentCamera.enabled = true;
        }
        else
        {
            currentCamera.enabled = false;
        }
    }



}
