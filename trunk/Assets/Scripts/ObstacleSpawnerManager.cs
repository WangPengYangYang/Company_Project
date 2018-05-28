using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawnerManager : MonoBehaviour
{

    public static ObstacleSpawnerManager instance;

    // the lists of obstacle for the pool

    public List<Obstacle> squareObstacles = new List<Obstacle>(); 
    public List<Obstacle> circleObstacles = new List<Obstacle>();
    public List<Obstacle> rotatorObstacles = new List<Obstacle>();
    public List<Obstacle> arrowObstacles = new List<Obstacle>();
    public List<Obstacle> stickObstacles = new List<Obstacle>();
    public List<Obstacle> longObstacles = new List<Obstacle>();

    // how many type of obstacles do you want for each type

    public int squares = 10;
    public int circles = 10;
    public int rotators = 5;
    public int arrows = 5;
    public int sticks = 20;
    public int longs = 20;

    public Transform planet;

    // the prefabs of the obstacles 

    public GameObject squareBase, circleBase, rotatorBase, arrowBase, stickBase,longBase;

    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {

        SpawnAll();
        SpawnRoutine();
    }

    void SpawnAll()  // you spawn all the items of the pool all at once so you can use them later
    {
        SpawnItems(squares, squareBase, squareObstacles);
        SpawnItems(circles, circleBase, circleObstacles);
        SpawnItems(rotators, rotatorBase, rotatorObstacles);
        SpawnItems(arrows, arrowBase, arrowObstacles);
        SpawnItems(sticks, stickBase, stickObstacles);
        SpawnItems(longs, longBase, longObstacles);

    }

    void SpawnItems(int itemValue, GameObject item, List<Obstacle> listToUse)
    {
        for (int i = 0; i < itemValue; i++)  // spawn the items and take their Obstacle property by adding them in the list
        {
            GameObject newObj = (GameObject)Instantiate(item);
            listToUse.Add(newObj.GetComponent<Obstacle>());
            newObj.transform.parent = this.transform;
        }
    }


    public virtual void SpawnRoutine()
    {
       
        // this void is needed to attach ObstacleSpawner script. It will be override by obstacleSpawner.cs
    }



}
