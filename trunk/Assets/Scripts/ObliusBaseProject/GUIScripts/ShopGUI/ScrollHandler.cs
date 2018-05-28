using UnityEngine;
using System.Collections;

public class ScrollHandler : MonoBehaviour
{

    float previousMousePosition = 0;
    float newMousePosition = 0;

    public float positionVariation;
    public bool isScrolling;

    public float scrollPower;

    public static ScrollHandler instance;


    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        decreaseScrollPower();
        if (Input.GetMouseButton(0))
        {
            newMousePosition = Input.mousePosition.x;
            if (!isScrolling)
            {
                previousMousePosition = newMousePosition;
            }
            else {


                positionVariation = newMousePosition - previousMousePosition;
                previousMousePosition = newMousePosition;
                scrollPower = positionVariation;


            }


            isScrolling = true;
        }
        else
        {
            isScrolling = false;
        }
    }

    void decreaseScrollPower()
    {
        if (scrollPower > 0)
        {
            scrollPower -= 3f;
            if (Mathf.Round(scrollPower) < 5)
            {
                scrollPower = 0;
            }
        }
        if (scrollPower < 0)
        {
            scrollPower += 3f;
            if (Mathf.Round(scrollPower) > -5)
            {
                scrollPower = 0;
            }
        }


    }
}
