using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeCloseManager : MonoBehaviour
{
    public Animation anim;
    // Use this for initialization
    void Start()
    {

        StartCoroutine(Animation());
    }

    // Update is called once per frame
    IEnumerator Animation()
    {
        while (true)
        {
            float randomTimeClose = Random.Range(5, 8);
            yield return new WaitForSeconds(randomTimeClose);
            anim.Play();
        }
    }
}
