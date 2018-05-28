using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    int direction;
    public Transform objectParent;
    // Update is called once per frame
    private void OnEnable()
    {
        direction = Random.Range(0, 1 + 1);
        if (direction == 0)
        {
            direction = -1;
        }
    }

    void Update () {
        if (transform.parent == objectParent)
        {
            transform.Rotate(0, 0, (direction * 200) * Time.deltaTime);
        }
	}
}
