using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHitManager : MonoBehaviour {

    public static CharacterHitManager instance;
   public bool hasBeenHit;


    // these are player effects
    public Transform playerSprite;
    public Transform hitSprite;
    public GameObject playerParticle;
    public SpriteRenderer keeperRing;
    public SpriteRenderer flash;

    public CharacterSpriteManager playerSpriteManager;
 
    private void Awake()
    {
        instance = this;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasBeenHit == false)
        {
            collision.gameObject.transform.parent = null;

            Rigidbody2D rigid = collision.gameObject.GetComponent<Rigidbody2D>(); // the object that hits the character must have a rigidbody attached
            if (rigid==null)collision.gameObject.AddComponent<Rigidbody2D>();
            
         
            Hit();
        }
        else { return; }
    }

    public void Hit()
    {
        if (hasBeenHit) return;

        if(VibrationButton.vibrationEnabled)
        Handheld.Vibrate();  // enable and disable all the player effects and scripts, then give a torque speed and fall down

        flash.gameObject.SetActive(true);
        StartCoroutine(fadeFlash());
        keeperRing.enabled = false;
        playerSpriteManager.enabled = false;

        hasBeenHit = true;
        playerSprite.parent = null;
       Rigidbody2D rigid = playerSprite.gameObject.AddComponent<Rigidbody2D>();
        Destroy(playerParticle.gameObject);

     
        rigid.AddForce(Vector3.right * Random.Range(-3f,3f), ForceMode2D.Impulse);
        rigid.AddTorque(30, ForceMode2D.Impulse);
        ObstacleSpawner.instance.gameObject.SetActive(false);

        GameEventsCollection.instance.Death();
    }

    IEnumerator fadeFlash() // this is the flash that happens when the character gets hit
    {
        Color target = flash.color;
        target.a = 0;
        Color current = flash.color;
        float lerper = 0;
        float lerperTime = 0.5f;
        hitSprite.gameObject.SetActive(true);
        while (lerper <=1)
        {
            lerper += Time.deltaTime/lerperTime;
            yield return new WaitForEndOfFrame();
            flash.color = Color.Lerp(current, target, lerper);
        }
         hitSprite.gameObject.SetActive(false);

        flash.gameObject.SetActive(false);

    }


}
