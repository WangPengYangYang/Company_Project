using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class GUIElement : MonoBehaviour
{

    public float secondsToWaitToStart;


    public List<GUIEffect> spawnEffects;
    public List<GUIEffect> disappearEffects;


    public bool spawnStarted;


    protected float originalAlpha;
    protected CanvasRenderer canvasRenderer;


    // Use this for initialization
    void Awake()
    {
        canvasRenderer = GetComponent<CanvasRenderer>();
        originalAlpha = canvasRenderer.GetAlpha();
    }



    void OnEnable()
    {

    }

    public void disappear()
    {
        if (disappearEffects.Count == 0) { canvasRenderer.SetAlpha(0); }
        foreach (GUIEffect effect in disappearEffects)
        {
            effect.startEffect();
        }
    }

    public IEnumerator spawn()
    {
        canvasRenderer = GetComponent<CanvasRenderer>();

        yield return new WaitForSeconds(secondsToWaitToStart);

        foreach (GUIEffect effect in spawnEffects)
        {
            effect.Initialize();
            yield return StartCoroutine(effect.startEffect());


        }

    }


    void OnDisable()
    {
        foreach (GUIEffect effect in spawnEffects)
        {
            effect.reset();
        }
        foreach (GUIEffect effect in disappearEffects)
        {
            effect.reset();
        }
    }

}
