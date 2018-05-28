using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TextBumperEffect : MonoBehaviour {

    // this is just a effect that bumps the text if it's different. You can do the bump with an external call if needed and not when the text changes
    
 public float bumpvalue = 0.15f;



    Text text;
    public bool onEnableTrigger;

    void Awake()
    {
        text = GetComponent<Text>();
        savedText = text.text;
        normalTextScale = text.transform.localScale;
    }

    string savedText;
    Vector3 normalTextScale;

	
	// Update is called once per frame
	void Update () {
        if (onEnableTrigger)
        return;

        if (savedText != text.text) {

            savedText = text.text;
            StopAllCoroutines();
            StartCoroutine(triggerBumpEffect());
        }


	
	}

    void OnEnable() {
        if (onEnableTrigger)
        {
            StopAllCoroutines();

            StartCoroutine(triggerBumpEffect());
        }

    }


    IEnumerator triggerBumpEffect()
    {
        float lerper = 0;
        float lerperTime = 0.15f;
        Vector3 startingScale = normalTextScale;
        Vector3 bumpScale = normalTextScale + new Vector3(bumpvalue, bumpvalue, bumpvalue);

        while (lerper <= 1)
        {
            lerper += Time.deltaTime / lerperTime;
            text.transform.localScale = Vector3.Lerp(startingScale, bumpScale, lerper);

            yield return new WaitForEndOfFrame();

        }
        lerper = 0;
        while (lerper <= 1)
        {
            lerper += Time.deltaTime / lerperTime;
            text.transform.localScale = Vector3.Lerp(bumpScale, startingScale, lerper);

            yield return new WaitForEndOfFrame();

        }




    }




}
