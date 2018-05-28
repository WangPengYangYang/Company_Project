using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FadeText : MonoBehaviour {
    public float effectTime = 1;
    private void OnEnable()
    {
        StartCoroutine(routineEffect());
    }
    IEnumerator routineEffect()
    {
        Text text = GetComponent<Text>();
        Color a = Color.white;
        Color b = a;b.a = 0;
        float lerper = 0;
        float lerperTime = effectTime;
        while (true)
        {
            while (lerper <= 1)
            {
                lerper += Time.deltaTime / lerperTime;
                yield return new WaitForEndOfFrame();
                text.color = Color.Lerp(a, b, lerper);
            }
            lerper = 0;
            while (lerper <= 1)
            {
                lerper += Time.deltaTime / lerperTime;
                yield return new WaitForEndOfFrame();
                text.color = Color.Lerp(b, a, lerper);
            }
            lerper = 0;


        }

    }
}
