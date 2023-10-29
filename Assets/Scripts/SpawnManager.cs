using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] Obstacles;
    public GameObject ScoreCounter;
    private Vector3 spawnPos = new Vector3(25, 0, 0);

    public float startDelay = 2;
    public float repeatRate = 2;

    public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        //Find PlayerController script
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        //Spawn obstacles repeatedly
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    public void SpawnObstacle()
    {
        if (playerController.gameOver == false)
        {
            //Random obstacle from list
            int rnd = Random.Range(0,Obstacles.Length);
            //Create instances
            Instantiate(Obstacles[rnd], spawnPos, Obstacles[rnd].transform.rotation);
            Instantiate(ScoreCounter, spawnPos + new Vector3(1,1,0), ScoreCounter.transform.rotation);
        }

    }
}