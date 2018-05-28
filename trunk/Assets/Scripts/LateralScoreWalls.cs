using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LateralScoreWalls : MonoBehaviour {

    // if a obstacle goes behind these limits, they will fire a lateral particle and gives 1 point

    public GameObject particleToUse;
    public GameObject plusOneEffect;
    public float plusOneXSpawn;
	// Use this for initialization
	void Start () {
		
	}
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer==(LayerMask.NameToLayer("Obstacle")))
        {
            FireParticle(collision.transform);
        }
    }

    void FireParticle(Transform obst) {

        Vector3 particlePosition = new Vector3();
        particlePosition.x = transform.position.x;
        particlePosition.y = obst.position.y;
        particlePosition.z = 1;

        Vector3 directional = particlePosition;
        directional.x = 0;

        Vector3 direction = directional - particlePosition;
        GameObject newpart = (GameObject)Instantiate(particleToUse, particlePosition, Quaternion.identity);
        newpart.transform.rotation = Quaternion.LookRotation(direction);

        particlePosition.x = plusOneXSpawn;

        GameEventsCollection.instance.IncreaseScore(1, particlePosition);

        Destroy(newpart, 3);

    }
}
