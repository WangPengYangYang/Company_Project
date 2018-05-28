using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    public Rigidbody2D rigid;
    public bool hit = false;


    // this are values to determine the randomization of the obstacle
    public bool maintainAspectRateo = true;
    public bool dontScale = false;
    public bool dontMove = false;
    public bool dontRotate = false;

    Vector2 startingScale;
    public Vector2 minMaxSize = new Vector2(0.5f, 2f);
    public Vector2 minMaxMovement = new Vector2(-2f, 2f);


    public ObstacleSpawner.ObstacleKind ObstacleType;


    // these are the values for obstacles that has multiple pieces in them
    public Transform[] specialPieces;
    Vector3[] specialLocalEuler;
    Vector3[] specialLocalPosition;
    List<Rigidbody2D> specialRigids = new List<Rigidbody2D>();
    public bool[] specialHits;



    public bool spawned;
    float maxYSpeed = -3.5f;

    public TrailRenderer arrowTrail;

    public bool hitByKepper;

    private void Start()
    {
        Vector3 vel = rigid.velocity;
        vel.y = maxYSpeed;
        rigid.velocity = vel;       // the obstacle will start with max falling speed 


        // rotator and stick has multiple objects and are set up differently
        if (ObstacleType == ObstacleSpawner.ObstacleKind.rotator
            || ObstacleType == ObstacleSpawner.ObstacleKind.stick) { CompoundSetup(); }

        startingScale = transform.localScale; // this is set because when the object re-spawns maintained the previous scale

        Spawn();

    }

    private void Update()
    {

        if (rigid != null)
        {
            Vector3 vel = rigid.velocity;
            vel.y = Mathf.Clamp(vel.y, maxYSpeed, Mathf.Infinity); // clamp the velocity Y 
            rigid.velocity = vel;
        }


        if (ObstacleType == ObstacleSpawner.ObstacleKind.stick) // clamp the velocity Y of the sticks
        {
            if (specialRigids.Count > 0)
                foreach (Rigidbody2D rigidX in specialRigids)
                {
                    Vector3 vel = rigidX.velocity;
                    vel.y = Mathf.Clamp(vel.y, maxYSpeed, Mathf.Infinity);
                    rigidX.velocity = vel;

                }
        }



        if (transform.position.y < -10 && this.gameObject.activeInHierarchy) // Hide the arrow trail before it respawns because Unity is bugged as hell
        {
            if (arrowTrail != null) arrowTrail.enabled = false;
        }

        if (transform.position.y < -30 && this.gameObject.activeInHierarchy) // The Obstacle is clearly gone down and needs to be pooled
        {
            ReAddToList();
            Spawn();
        }
    }

    public void Spawn()
    {
        if (ObstacleType == ObstacleSpawner.ObstacleKind.rotator ||
         ObstacleType == ObstacleSpawner.ObstacleKind.stick) CompoundSpecialSpawn();

        hit = false;
        hitByKepper = false;

        // randomize the obstacle
        RandomizeSize();
        RandomMove();
        RandomRotate();

        Show(false);

        spawned = true;
    }

    // here there are special commands for the rotator object
    #region CompoundSpecials
    void CompoundSetup()
    {

        specialLocalEuler = new Vector3[specialPieces.Length];
        specialLocalPosition = new Vector3[specialPieces.Length];
        specialHits = new bool[specialPieces.Length];

        for (int i = 0; i < specialPieces.Length; i++)
        {
            specialLocalEuler[i] = specialPieces[i].localEulerAngles;
            specialLocalPosition[i] = specialPieces[i].localPosition;
        }
    }

    void SpecialDetach()
    {
        for (int i = 0; i < specialRigids.Count; i++)
        {

            Destroy(specialRigids[i]);
        }

        specialRigids = new List<Rigidbody2D>();
        for (int i = 0; i < specialPieces.Length; i++)
        {
            specialPieces[i].parent = null;

            specialRigids.Add(specialPieces[i].gameObject.AddComponent<Rigidbody2D>());

        }

    }

    void CompoundSpecialSpawn()
    {
        foreach (Rigidbody2D rigid in specialRigids)
        {

            Destroy(rigid);
        }

        for (int i = 0; i < specialPieces.Length; i++)
        {

            if (ObstacleType == ObstacleSpawner.ObstacleKind.rotator)
            {
                if (i != 0)
                {
                    specialPieces[i].parent = specialPieces[0];
                }
                else
                { specialPieces[i].parent = this.transform; }
            }
            else if (ObstacleType == ObstacleSpawner.ObstacleKind.stick)

            {
                specialPieces[i].parent = this.transform;
            }

            specialHits[i] = false;
            specialPieces[i].transform.localEulerAngles = specialLocalEuler[i];
            specialPieces[i].transform.localPosition = specialLocalPosition[i];


        }
    }

    public void CompoundSpecialHit(Transform piece)
    {
        if (hit) return;


        int index = System.Array.IndexOf(specialPieces, piece);
        if (specialHits[index] == true) return;
        specialHits[index] = true;

        if (ObstacleType == ObstacleSpawner.ObstacleKind.rotator)
        {
            piece.parent = null;
            specialRigids.Add(piece.gameObject.AddComponent<Rigidbody2D>());

        }


    }
    #endregion

    public void ObstacleHit()
    {
        if (hit) return;
        hit = true;
    }

    public void Show(bool val = true)  // show the object
    {
        gameObject.SetActive(val);
        if (rigid != null) rigid.velocity = Vector2.zero;
        if (arrowTrail != null && this.isActiveAndEnabled) StartCoroutine(ShowArrowTrail());

    }
    IEnumerator ShowArrowTrail()
    {
        yield return new WaitForSeconds(0.5f); // this is done to avoid the trail show across the screen
        arrowTrail.enabled = true;
    }

    public void Drop()
    {
        Show();
        if (ObstacleType == ObstacleSpawner.ObstacleKind.stick) SpecialDetach(); // the sticks detach all the items. The prafab is made by more sticks

    }



    // Just randomizing the rotation, spawnposition and size
    #region randomizeItems
    public void RandomRotate()
    {

        transform.rotation = Quaternion.identity;
        if (dontRotate) return;
        Vector3 euler = transform.eulerAngles;
        euler.z = Random.Range(0, 360);
        transform.eulerAngles = euler;
    }
    public void RandomMove()
    {
        transform.position = ObstacleSpawnerManager.instance.transform.position;

        if (dontMove) return;
        float range = Random.Range(minMaxMovement.x, minMaxMovement.y);
        transform.position += Vector3.right * range;
    }
    public void RandomizeSize()
    {
        if (dontScale) return;

        Vector3 scale = startingScale;
        float val = Random.Range(minMaxSize.x, minMaxSize.y);

        if (maintainAspectRateo)
        {
            scale.x = scale.x * val;
            scale.y = scale.y * val;
        }
        else
        {
            scale.x = scale.x * val;
            val = Random.Range(minMaxSize.x, minMaxSize.y); //  randomize scale on separate axis
            scale.y = scale.y * val;
        }

        transform.localScale = scale;

    }

#endregion

    public void ReAddToList()
    {
        // add to proper lists to be pooled


        switch (ObstacleType)
        {
            case ObstacleSpawner.ObstacleKind.arrow:
                ObstacleSpawner.instance.arrowObstacles.Add(this);
                break;

            case ObstacleSpawner.ObstacleKind.square:
                ObstacleSpawner.instance.squareObstacles.Add(this);
                break;

            case ObstacleSpawner.ObstacleKind.circle:
                ObstacleSpawner.instance.circleObstacles.Add(this);
                break;

            case ObstacleSpawner.ObstacleKind.stick:
                ObstacleSpawner.instance.stickObstacles.Add(this);
                break;

            case ObstacleSpawner.ObstacleKind.rotator:
                ObstacleSpawner.instance.rotatorObstacles.Add(this);
                break;

        }
    }


}
