using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpriteManager : MonoBehaviour {
    public SpriteRenderer sprite;
	

	
	// Update is called once per frame
	void Update () {
        Vector3 euler = transform.eulerAngles;
        euler.z = 0;
        transform.eulerAngles = euler;
        sprite.sprite = ShopHandler.instance.shopItemToUse.sprite.sprite;
	}
}
