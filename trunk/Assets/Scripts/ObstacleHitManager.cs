using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHitManager : MonoBehaviour {

    // this will manage if a obstacle has been hit, and what to do next 

    public Obstacle obstacle;
    float minMagnitudeParticle = 9f;
    float maxMagnitudeMultiplier = 2.2f; // if the magnitude is higher than this it spawn an other hit particle ( the enlarging circle )
    public bool hitByKeeper = false;

    private void OnEnable()
    {
        hitByKeeper = false;
    }
   

    private void OnCollisionEnter2D(Collision2D collision)

    {
        if (obstacle.spawned == false) return;

        if (collision.gameObject.tag == "Keeper")
        {
            try
            {
                CheckScorePoint();
                SpawnHitParticles(collision);

                hitByKeeper = true;

                if (obstacle.ObstacleType != ObstacleSpawner.ObstacleKind.rotator)
                { // rotator cannot activate this boolean because it gives point for every piece you touch
                    obstacle.hitByKepper = true;
                }
            }
            catch { }
        }

        if (obstacle.ObstacleType == ObstacleSpawner.ObstacleKind.rotator
            || obstacle.ObstacleType == ObstacleSpawner.ObstacleKind.stick)
        {
            CompoundHit(); // the compounds are special properties when they are hit ( for example they break )
        }
        else
        {
            NormalHit();
        }
    }


    void SpawnHitParticles(Collision2D collision)

    {
        float hitMagnitude = collision.relativeVelocity.magnitude;
        Vector2 hitPoint = collision.contacts[0].point;

        if (hitMagnitude < minMagnitudeParticle) return; // not enough hit power

        // enough hit power for particle ( NORMAL )

        Vector3 direction =  collision.transform.position- (Vector3)collision.contacts[0].point;
        GameObject newParticle = (GameObject)Instantiate(GraphicsManager.instance.hitParticle);
        newParticle.transform.position = hitPoint;
        newParticle.transform.rotation = Quaternion.LookRotation(direction);

        Destroy(newParticle, 3);


        // enough hit power for particle ( CIRCLE )

        if (hitMagnitude >=( minMagnitudeParticle * maxMagnitudeMultiplier)) {
            GameObject hitRing = (GameObject)Instantiate(GraphicsManager.instance.specialHitRing);
            hitRing.transform.position = hitPoint;
            Destroy(hitRing, 3);
        }


    }


    public void SpawnSpecialArrowParticle()  // the arrow has a different particle 
    {
        GameObject newPart = (GameObject)Instantiate(GraphicsManager.instance.specialArrowParticle);
        newPart.transform.position = transform.position;
        Destroy(newPart, 3);
    }

    public void CheckScorePoint() {
        // you score only once if you hit a obstacle. You can't do multiple points each time you hit it

        if (hitByKeeper || obstacle.hitByKepper) return;

        GameEventsCollection.instance.IncreaseScore(1,transform.position);

        }

    void CompoundHit()
    {
        obstacle.CompoundSpecialHit(this.transform);
    }

   public void NormalHit()
    {
        obstacle.ObstacleHit();
    }


}
