using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WFG.Utilities;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : Singleton<ScoreManager>
{
    public TMP_Text scoreText;
    private int score=0;

    new void Awake()
    {
        base.Awake();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        resetScore();
    }

    private void updateScore()
    {
        scoreText.text = "Score: " + score;
    }

    private void resetScore()
    {
        score = 0;
        updateScore();

    }


    public void addToScore(int value)
    {
        score += value;
        updateScore();
    }
}
