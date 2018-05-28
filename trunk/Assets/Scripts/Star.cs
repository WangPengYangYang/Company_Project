using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour {
    bool moving=true;
    bool taken = false;
   public  float speed = 2f;
    public float rotSpeed = 5f;
  public  Vector3 returnPosition;


    // Spawn a star, move it in a random position of the screen and then rotates. 
    // when the star gets taken, fly to the "return position" that's top right of the screen

    private void Start()
    {
        RandomizePosition();
    }

    void RandomizePosition()
    {
        Vector3 pos = transform.position;
        pos.y = 8;
        pos.x = Random.Range(-2.4f, 2.4f);
        transform.position = pos;
    }

    // Update is called once per frame
    void Update () {
        if (moving)
        {
            transform.position += Vector3.down * Time.deltaTime * speed;
            transform.Rotate(0, 0, 360 * rotSpeed*Time.deltaTime);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {if (taken) return;

        if (collision.gameObject.tag == "Keeper" && collision.gameObject.name =="KeeperRing")
        {
            taken = true;
            moving = false;
            StartCoroutine(CollectRoutine());
        }
    } 

    IEnumerator CollectRoutine()
    {
        float lerper = 0;
        float lerperTime = 0.3f;
        Vector3 pos = transform.position;
        while (lerper <= 1)
        {
            lerper += Time.deltaTime / lerperTime;
            yield return new WaitForEndOfFrame();
            transform.position = Vector3.Lerp(pos, returnPosition, lerper);
        }
        GameEventsCollection.instance.IncreasePoint(1);
        Destroy(this.gameObject);

    }


}
