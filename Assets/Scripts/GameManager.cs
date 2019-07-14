using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    #region Singleton

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        if (instance != this) Destroy(this);
    }

    #endregion

    public List<Projectile> projectilePool;

    public int Score;

    public int scoreSum;

    public float timerMultiplier;

    public int multiplier;

    public int timesReset;

    public Text scoreText;

    public Text multiplierText;


    void Start()
    {
        multiplier = 1;
        Score = 0;
    }

    void Update()
    {
        scoreFunction();
    }

    public Projectile GetProjectileNotActive()
    {
        for (int i = 0; i < projectilePool.Count; i++)
        {

            if (projectilePool[i].activated == false)
            {
                projectilePool[i].activated = true;
                return projectilePool[i];
            }
        }

        return null;
    }

    public void scoreFunction()
    {
        if (timerMultiplier > 0) timerMultiplier -= Time.deltaTime;

        else if (timerMultiplier <= 0)
        {
            timerMultiplier = 0;
            timesReset = 0;
            multiplier = 1;
        }

        scoreText.text = Score.ToString();

        multiplierText.text = "x" + multiplier.ToString();
    }

    public void ResetMultiplier(int scoreEnemy)
    {
        timerMultiplier = 10f;
        timesReset++;

        scoreSum = scoreEnemy * multiplier;

        Score += scoreSum;

        if (timesReset % 10 == 0) multiplier++;

    }
}
