using UnityEngine;
using System.Collections;

public class Pulsate : GUIEffect
{


    Vector3 finalSize;
    Rect elementRect;

    public float enlargmentVelocity;
    public float maxDilatationOffset;
    protected bool reachedMaxDilatation;

    Vector2 originalSize;

    void Awake()
    {
        finalSize = this.transform.localScale;
    }

    // Use this for initialization
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        if (isExecuting)
        {
            if (!reachedMaxDilatation)
            {
                Vector3 scaleWhenMaxDilatated = finalSize;
                scaleWhenMaxDilatated.x += maxDilatationOffset;
                scaleWhenMaxDilatated.y += maxDilatationOffset;

                transform.localScale = Vector2.MoveTowards(transform.localScale, scaleWhenMaxDilatated, enlargmentVelocity * Time.deltaTime);

                if (transform.localScale.x == scaleWhenMaxDilatated.x)
                {
                    reachedMaxDilatation = true;
                }
            }
            else
            {
                transform.localScale = Vector2.MoveTowards(transform.localScale, finalSize, enlargmentVelocity * Time.deltaTime);

                if (transform.localScale.x == finalSize.x)
                {
                    reachedMaxDilatation = false;
                }
            }
        }

    }

    void OnEnable()
    {
        base.Start();
        isFinished = true;
    }


    public override IEnumerator startEffect()
    {

        reachedMaxDilatation = false;
        isFinished = false;
        isExecuting = true;
        yield return null;
    }


}
