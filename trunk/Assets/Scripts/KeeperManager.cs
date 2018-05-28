using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeeperManager : MonoBehaviour {

    // the keeper ring is just a circle with a collision that moves when you move the finger around

  public  Rigidbody2D rigid;
    float movSpeed = 400;

	void FixedUpdate () {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 1;

            Vector2 mousePosV2 = Camera.main.ScreenToWorldPoint(mousePos);

            float dist =Mathf.Abs(mousePosV2.x- transform.position.x) * 20; // distance from the current "keeper" and the mouse position. This value is important to not let teleport the keeper if you tap around

            rigid.MovePosition(Vector2.Lerp(transform.position, mousePosV2, movSpeed * Time.deltaTime / dist));
           
        }
        else
        {
            rigid.velocity = Vector3.zero; // if you are not touching the screen, the keeper is solid and doesnt get affected by other rigids
        }
	} 
}
