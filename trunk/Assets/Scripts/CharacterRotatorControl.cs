using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRotatorControl : MonoBehaviour {
    public float speedMultiplier = 1; // rotate the character pivot around
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 0, 360 * Time.deltaTime* speedMultiplier);
	}
}
