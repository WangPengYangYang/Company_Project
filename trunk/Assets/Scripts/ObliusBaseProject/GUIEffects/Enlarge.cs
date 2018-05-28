using UnityEngine;
using System.Collections;

public class Enlarge : GUIEffect {


    Vector3 finalSize;
    Rect elementRect;

    public float enlargmentVelocity;


    public float maxDilatationOffset;
    protected bool reachedMaxDilatation;


    void Awake()
    {
        finalSize = this.transform.localScale;
    }

	// Use this for initialization
	void Start () {       

    }
	
	// Update is called once per frame
	void Update () {

        if (isExecuting)
        {
            if (!reachedMaxDilatation)
            {
                Vector3 scaleWhenMaxDilatated = finalSize;
                scaleWhenMaxDilatated.x += maxDilatationOffset;
                scaleWhenMaxDilatated.y += maxDilatationOffset;

                transform.localScale = Vector2.MoveTowards(transform.localScale, scaleWhenMaxDilatated, enlargmentVelocity * Time.deltaTime);

                if(transform.localScale.x == scaleWhenMaxDilatated.x)
                {
                    reachedMaxDilatation = true;
                }
            }
            else
            {
                transform.localScale = Vector2.MoveTowards(transform.localScale, finalSize, enlargmentVelocity * Time.deltaTime);

                if(transform.localScale.x == finalSize.x)
                {
                    isExecuting = false;
                    isFinished = true;
                }
            }
        }
	
	}

    void OnEnable()
    {
        base.Start();
        
        transform.localScale = new Vector2(0, 0);
        transform.localScale = new Vector2(0, 0);
    }


    public override IEnumerator startEffect()
    {
        transform.localScale = new Vector2(0, 0);
        reachedMaxDilatation = false;
        isFinished = false;
        isExecuting = true;
        yield return null;
    }


}
