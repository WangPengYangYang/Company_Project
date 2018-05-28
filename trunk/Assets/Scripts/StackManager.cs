using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StackManager : MonoBehaviour {

    public int stacks = 0;
    int currentStack = 0;


    int minStackCount = 10; // check the stack only after 10 of them 

    public Text stackMessage,stackScore;
    public Animation scoreAnimation;

    bool stacking;
    bool givingPoint; // if stacking and stack is more than minStackCount

    private void Start()
    {
        stackScore.enabled = false;
    }

    private void Update()
    {

        if (givingPoint) return;
        if (stacks > minStackCount && stacking==false)
        {
            stacking = true;
            currentStack = stacks;
        }

        if (stacking)
        {
            ShowStackMessage();

            if (stacks < currentStack-5) // when the stack is less than current and this tollerance value, stop stacking and give points
            {
                stacking = false;


                StopAllCoroutines();
                StartCoroutine(givingPointRoutine());
                ShowStackMessage(false);
                stacks = 0;

            }

        }
        else
        {
            ShowStackMessage(false);
        }

      }


    IEnumerator givingPointRoutine()
    { // A simple routine that do the "give point" animation and stop everything else

        givingPoint = true;
        stackScore.enabled = true;

        scoreAnimation.Play();
        stackScore.text = "STACKED " + currentStack + "!";
        yield return new WaitForSeconds(2);
    
        stackScore.enabled = false;
        currentStack = 0;
        givingPoint = false;

    }




    void ShowStackMessage(bool val=true)
    {
        if (val)
        {
            stackMessage.enabled = true;
            stackMessage.text = "STACKING " + currentStack;
        }
        else {
            stackMessage.enabled = false;
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        stacks++;

        if (stacking)  // only if you reached the minimum stack it's considered stacking
        {
            currentStack++;
            GameEventsCollection.instance.IncreaseScore(1, Vector3.zero);
        }
     
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(stacks>0)
        stacks--;


    }
}
