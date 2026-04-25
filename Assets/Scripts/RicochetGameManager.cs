using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RicochetGameManager : MonoBehaviour
{
    public static RicochetGameManager S;

    [Header("UI")]
    public Text shotsText;
    public Text levelText;
    public Text resultText;
    public Button loseRetryButton;
    public Button restartButton;
    public GameObject instructionPanel;



    [Header("Game Setting")]
    public int maxShots = 3;

    [Header("Dynamic")]
    public int shotsTaken = 0;
    public bool levelEnded = false;
    public float delayBeforeLoad = 2f;


    void Start()
    {
        UpdateShotsUI();
        UpdateLevelUI();
        
        resultText.gameObject.SetActive(false);
        loseRetryButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
    }
    void Awake()
    {
        S = this;
        Enemy.enemyCount = 0;
        Projectile.projectileCount = 0;
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

    void UpdateLevelUI()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex + 1;
        levelText.text = "Level " + currentLevel;
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

        if(msg == "You Lose!")
        {
            loseRetryButton.gameObject.SetActive(true);
        }
        else if(msg == "You Win!")
        {
            Invoke("LoadNextLevel", delayBeforeLoad);
        }
    }

    void LoadNextLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex +1;

        //last level
        if(nextIndex >= SceneManager.sceneCountInBuildSettings)
        {
            resultText.gameObject.SetActive(true);
            resultText.text = "You Beat the Game!";
            restartButton.gameObject.SetActive(true);
            return;
        }
        SceneManager.LoadScene(nextIndex);
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void CloseInstruction()
    {
        if (instructionPanel != null)
        {
            instructionPanel.SetActive(false);
        }
    }
}
