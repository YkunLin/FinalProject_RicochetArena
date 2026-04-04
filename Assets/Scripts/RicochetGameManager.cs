using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicochetGameManager : MonoBehaviour
{
    public static RicochetGameManager S;

    [Header("Game Setting")]
    public int maxShots = 3;

    [Header("Dynamic")]
    public int shotsTaken = 0;
    public bool levelEnded = false;

    void Awake()
    {
        S = this;
    }

    public void ShotFired()
    {
        if (levelEnded) return;
        shotsTaken++;
        Debug.Log("Shots Left: " + (maxShots - shotsTaken));
    }

    void Update()
    {
        if(levelEnded) return;

        if(Enemy.enemyCount<= 0)
        {
            levelEnded = true;
            Debug.Log("You Win!");
        }
        else if(shotsTaken >= maxShots)
        {
            levelEnded = true;
            Debug.Log("You Lose!");
        }
    }
}
