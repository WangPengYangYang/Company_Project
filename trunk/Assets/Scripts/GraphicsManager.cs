using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsManager : MonoBehaviour
{
    public static GraphicsManager instance;

    public GameObject hitParticle, specialHitRing;
    public GameObject specialArrowParticle;

    public GameObject plusOneEffect; // the +1 that pops up

    public BgColorTemplate[] colorTemplates; 

    public SpriteRenderer AlphaFade;

    public float changeBGTimer = 5f; // the time when the current background color remains unchanged
    public float bgLerperTime = 10f; // the time that is needed to fade the entire background

    private void Awake()
    {
        instance = this;
    }

    public void SpawnPlusOneEffect(Vector3 pos) // give the position an spawn the + 1 effect
    {
        GameObject objToUse = plusOneEffect;
        GameObject plusone = Instantiate(objToUse, pos, objToUse.transform.rotation);
        plusone.transform.position = pos;
        Destroy(plusone, 3);
    }
    private void Start()
    {
        StartCoroutine(BGColorFade());
    }

    IEnumerator BGColorFade()
    {
        if (colorTemplates.Length < 2) { Debug.LogError("not enough color templates"); yield return null; }

        BgColorTemplate currentTemplate = colorTemplates[Random.Range(0, colorTemplates.Length)];


        // give a starting color template
        AlphaFade.color = currentTemplate.colorBottom;
        Camera.main.backgroundColor = currentTemplate.colorTop;

        float lerper = 0;


        while (true)
        {
            yield return new WaitForSeconds(changeBGTimer);


            // chose a random template
            BgColorTemplate newTemplate = colorTemplates[Random.Range(0, colorTemplates.Length)];

            while (newTemplate == currentTemplate)
            {//continue to choose to avoid the same template 
                 newTemplate = colorTemplates[Random.Range(0, colorTemplates.Length)];
                yield return new WaitForEndOfFrame();
            }

            while (lerper <= 1) // finally fade
            {
                yield return new WaitForEndOfFrame();

                lerper += Time.deltaTime / bgLerperTime;
                AlphaFade.color = Color.Lerp(currentTemplate.colorBottom, newTemplate.colorBottom, lerper);
                Camera.main.backgroundColor = Color.Lerp(currentTemplate.colorTop, newTemplate.colorTop, lerper);

            }
            lerper = 0;
            currentTemplate = newTemplate; // save the template in variable to avoid to choose it later
        }

    }
}