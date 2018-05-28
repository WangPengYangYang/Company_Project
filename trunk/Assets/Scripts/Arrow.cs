using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
    public Rigidbody2D rigid;
    public ObstacleHitManager obstacleHit;
    public Obstacle obstacle;
    public SpriteRenderer sprite;
    // Use this for initialization

    public Color color1, color2;

    void Start () { // in the pooling system it clears his rigidbody.
        Destroy(rigid);
    }
    private void OnEnable()
    {
        StartCoroutine(ColorAnimation());

    }
    IEnumerator ColorAnimation() // just flash the colors
    {
        sprite.enabled = true;
        while (true)
        {
            sprite.color = color1;
            yield return new WaitForSeconds(0.07f);
            sprite.color = color2;
            yield return new WaitForSeconds(0.07f);
        }

    }

    private void Update()
    {
        if (CharacterManager.instance == null)  // destroy this arrow if the player is not existent
        {
            Hit();
            Destroy(this.gameObject);
            return;
        }

        // look at the player
        Vector3 viewVector = CharacterManager.instance.transform.position - transform.position;
        if (viewVector!=Vector3.zero)transform.rotation = Quaternion.LookRotation(viewVector);
      

        float movementSpeed = 2.2f;
        transform.position += transform.forward * movementSpeed * Time.deltaTime;

        transform.Rotate(0, 90, -90); // correcting the arrow position

        float dist = Vector3.Distance(transform.position, CharacterHitManager.instance.transform.position);

        if (dist <= 0.5f) // when the distance is less than X, the player is considered Hit ( and it will die )
        {
            transform.parent = null;
            CharacterHitManager.instance.Hit();
            Hit();
        }

    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (obstacleHit.hitByKeeper) return;
        if (CharacterHitManager.instance.hasBeenHit) return;

        if (collision.gameObject.tag == "Keeper" && collision.name == "KeeperRing") // when the keeper hits the arrow but not when other obstacles do that
        {
            Hit();
           
        }
    }

    private void Hit()
    {
        StartCoroutine(ArrowHit());
    }

    bool disappearing = false;

    IEnumerator ArrowHit()
    {
        if (!disappearing)
        {

            disappearing = true;


            obstacleHit.NormalHit();
            obstacleHit.CheckScorePoint();
            obstacleHit.SpawnSpecialArrowParticle();
            obstacleHit.hitByKeeper = true;
            obstacle.arrowTrail.enabled = false;
            sprite.enabled = false;

            yield return new WaitForSeconds(0.5f);
            obstacle.Spawn();
            obstacle.ReAddToList();
            disappearing = false;
        }
        yield return null;
 

    }

}