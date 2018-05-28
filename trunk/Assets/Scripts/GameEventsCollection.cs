using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventsCollection : MonoBehaviour {

    public static GameEventsCollection instance;
    public GameObject[] objectsToActivateAtStart;
    public GameObject[] objectsToDeactivateAtEnd;



    void Awake()
    {
        instance = this;
    }


 
    public bool GameStarted;
    public static bool firstStart = true;

    public void StartGame()
    {
        
        if (firstStart == true)  // this will avoid the gravity to decrease it's value each time you start the game. For some reason unity treats the gravity as static value
        {
            firstStart = false;
            Physics2D.gravity = Physics2D.gravity / 2;
        }
        GameStarted = true;

        foreach (GameObject objectAct in objectsToActivateAtStart)
        {
            objectAct.SetActive(true);
        }

}

    public void IncreasePoint(int val)
    {
      ScoreHandler.instance.increaseSpecialPoints(val);
    }

    public void IncreaseScore(int val,Vector3 positionToSpawn)
    {

        if (ObliusGameManager.instance.gameState == ObliusGameManager.GameState.game)
        {
            ScoreHandler.instance.increaseScore(val);

            if (positionToSpawn != Vector3.zero) // give the +1 effect a position where to spawn
                GraphicsManager.instance.SpawnPlusOneEffect(positionToSpawn);
        }    }

    bool dead;
    public void Death()
    {
        if (dead) return;
        
        dead = true;
        foreach (GameObject objectAct in objectsToDeactivateAtEnd)
        {
            objectAct.SetActive(false);
        }

        ObliusGameManager.instance.GameOver(1);

      
    }



}
