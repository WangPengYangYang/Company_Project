using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : ObstacleSpawnerManager
{
    // this script is part of ObstacleSpawnerManager. 
   
    bool jumpTimer = false;  // this value is used to jump the timer used for re-spawning the obstacles
    // for example when more obstacles are spawned together

    public enum ObstacleKind
    {
        square, circle, rotator, arrow, stick, longs
    }
    public ObstacleKind ObstacleToUse;

    public override void SpawnRoutine()
    {
        StartCoroutine(Spawncycle());
    }
    IEnumerator Spawncycle()
    {
        yield return new WaitForSeconds(1);

        // movePlanetDown
        yield return DoThePlanetAnimation();
         


        float timeForSpawnItem = 0.2f; // each x time a item will be spawned
        float currentTimer = 0;
        int obstaclesToSpawn = 0; 

        while (true)
        {
            obstaclesToSpawn--;

            if (obstaclesToSpawn <= 0)  // there are no obstacle to spawn so you get a random obstacle form the pool
            {

              yield return GetRandomObstacle();
                currentTimer = timeForSpawnItem; // set the timer to spawn this kind of obstacle
            }

            if (jumpTimer == false)
            {
                yield return new WaitForSeconds(currentTimer);
            }
           




            switch (ObstacleToUse)
            {
                case ObstacleKind.arrow:
                    yield return DropItem(arrowObstacles);
                    break;

                case ObstacleKind.square:
                    yield return DropItem(squareObstacles);
                 if(obstaclesToSpawn<=0)obstaclesToSpawn = Random.Range(2, 8); // add more obstacle of the same kind to the spawn
                    break;

                case ObstacleKind.circle:
                    yield return DropItem(circleObstacles);
                    if (obstaclesToSpawn <= 0) obstaclesToSpawn = Random.Range(2, 8); // add more obstacle of the same kind to the spawn
                    break;

                case ObstacleKind.longs:
                    yield return DropItem(longObstacles);
                    if (obstaclesToSpawn <= 0) obstaclesToSpawn = Random.Range(2, 8); // add more obstacle of the same kind to the spawn
                    break;

                case ObstacleKind.rotator:
                    yield return DropItem(rotatorObstacles, 0.5f, 0.5f); // add a extra timer to wait for the next obstacle because the obstacle is big
                    break;

                case ObstacleKind.stick:
                    yield return DropItem(stickObstacles,0.3f, 0.55f);// add a extra timer to wait for the next obstacle because the obstacle is big
                    break;

        
            }
        }
    }



    IEnumerator DropItem(List<Obstacle> ListToUse,float waitTimer=0,float afterTimer=0)
    {
        if (ListToUse.Count > 0)  // if in the list of the obstacles (squares,circles etc) a obstacle is present, spawn it
        {
            if(waitTimer>0)yield return new WaitForSeconds(waitTimer); // extra timers for big obstacles

            ListToUse[0].Drop();
            ListToUse.RemoveAt(0);  // drop and remove from the list
            jumpTimer = false;

            if(afterTimer>0)yield return new WaitForSeconds(afterTimer);  // extra timers for big obstacles

        }
        else
        {
            jumpTimer = true; // find an other obstacle to spawn ignoring the timer

        }
        yield return null;

    }

    IEnumerator GetRandomObstacle() // find the obstacle to use 
    {
        ObstacleToUse = new ObstacleKind();

        int random = Random.Range(0, 5 + 1);

        switch (random)
        {
            case 0:
                ObstacleToUse = ObstacleKind.arrow;
                break;
            case 1:
                ObstacleToUse = ObstacleKind.square;
                break;
            case 2:
                ObstacleToUse = ObstacleKind.circle;
                break;
            case 3:
                ObstacleToUse = ObstacleKind.rotator;
                break;
            case 4:
                ObstacleToUse = ObstacleKind.stick; 
                break;
            case 5:
                ObstacleToUse = ObstacleKind.longs;
                break;

        }
        yield return null;

    }

   IEnumerator DoThePlanetAnimation()
    {

        // move the planet down in a constant speed. And then start spawning

        float lerper = 0;
        float lerperTime = 1;
        float movPower = 2; 
        while (lerper <= 1)
        {
            planet.transform.position += -Vector3.up * Time.deltaTime * movPower;
            lerper += Time.deltaTime / lerperTime;
            yield return new WaitForEndOfFrame(); 
        }
    }

}
