using UnityEngine;
using System.Collections;

public class GUIEffect : MonoBehaviour {

    public  GUIEffect startAfterThisEffect;
    public float secondsToWaitToStart;

    public virtual RectTransform rect
    {
        get;
        set;
    }

    public virtual bool isExecuting
    {
        get;
        set;
    }

    public virtual bool isFinished
    {
        get;
        set;
    }

    public virtual void Awake()
    {
       // rect = getRect();
    }

    public virtual void Start()
    {

    }

    // Use this for initialization
    public virtual void Initialize () {
        rect = getRect();
	}
	
	// Update is called once per frame
	void Update () {

        if (isExecuting)
        {
            EffectUpdate();
        }

    }

    void OnDisable()
    {
        isExecuting = false;
    }

    public virtual void EffectUpdate()
    {

    }

    public virtual IEnumerator startEffect()
    {      

        yield return new WaitForSeconds(secondsToWaitToStart);
        yield return StartCoroutine(effect());
    }

    public virtual void reset()
    {

    }

    public virtual IEnumerator effect()
    {
        yield return null;
    }

    public virtual RectTransform getRect()
    {
        return GetComponent<RectTransform>();
    }


 

}
