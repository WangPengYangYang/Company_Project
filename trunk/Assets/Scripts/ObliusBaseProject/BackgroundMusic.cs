using UnityEngine;
using System.Collections;

public class BackgroundMusic : MonoBehaviour {
    public static BackgroundMusic instance;
    public AudioSource audio;
    void Awake() {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
            }
	// Use this for initialization
	void Start () {
        audio.Play();
	}
	
}
