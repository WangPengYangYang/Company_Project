using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour {
    Slider slide;

    void Start () {
        slide = GetComponent<Slider>();
        StartCoroutine(SlideRoutine());

    }
    private void OnEnable()
    {
        StartCoroutine(SlideRoutine());

    }
    IEnumerator SlideRoutine() { // the slider just moves left and right
        while (true)
        {
            float val = 0;
            float lerperTime = 1.25f;
            while (val <= 1)
            {
                val += Time.deltaTime / lerperTime;
             if(slide!=null)   slide.value =val;
                yield return new WaitForEndOfFrame();

            }
            while (val > 0)
            {
                val -= Time.deltaTime / lerperTime;
                slide.value = val;
                yield return new WaitForEndOfFrame();
            }

        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
