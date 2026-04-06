using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RicochetGameManager : MonoBehaviour
{
    public static RicochetGameManager S;
    [Header("UI")]
    public Text shotsText;
    public Text levelText;
    public Text resultText;


    [Header("Game Setting")]
    public int maxShots = 3;

    [Header("Dynamic")]
    public int shotsTaken = 0;
    public bool levelEnded = false;


    void Start()
    {
        UpdateShotsUI();
        resultText.gameObject.SetActive(false);
    }
    void Awake()
    {
        S = this;
    }

    public void ShotFired()
    {
        if (levelEnded) return;
        shotsTaken++;
        UpdateShotsUI();
    }

    void UpdateShotsUI()
    {
        int shotsLeft = maxShots - shotsTaken;
        shotsText.text = "Shots Left: " + shotsLeft;
    }

    void Update()
    {
        if(levelEnded) return;

        if(Enemy.enemyCount<= 0)
        {
            levelEnded = true;
            ShowResult("You Win!");
        }
        else if(shotsTaken >= maxShots && Projectile.projectileCount <= 0 && Enemy.enemyCount > 0)
        {
            levelEnded = true;
            ShowResult("You Lose!");
        }
    }

    void ShowResult(string msg)
    {
        resultText.gameObject.SetActive(true);
        resultText.text = msg;
    }
}
