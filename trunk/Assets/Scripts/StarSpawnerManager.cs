using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawnerManager : MonoBehaviour {
    public GameObject obstacleSpawner; 
    public GameObject starObject; // the special coin is a star object

	void Start () {

        StartCoroutine(SpawnRoutine());
	}
	
	IEnumerator SpawnRoutine()
    {
       while (true)
        {
            while (obstacleSpawner.activeInHierarchy == false)
            {
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(Random.Range(5, 13)); // each X seconds spawn a star item
          GameObject newStar = (GameObject)  Instantiate(starObject, starObject.transform.position, Quaternion.identity);
            newStar.transform.parent = ObstacleSpawner.instance.transform;
        }
    }
}
