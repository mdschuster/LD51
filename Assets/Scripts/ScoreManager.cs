using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WFG.Utilities;
using TMPro;
using UnityEngine.PlayerLoop;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreManager : Singleton<ScoreManager>
{
    [Header("UI Connections")]
    public TMP_Text scoreValue;
    public TMP_Text multiValue;
    public Slider multiplierTimerSlider;



    [Header("Multiplier Settings")]
    public float multiplierDecay;
    public int numToIncreaseMultiplier;
    public float timeToIncreaseMultiplier;
    private float multiplierIncreaseTime;
    private float multiplierDecayTime;
    private int multiplierIncreaseNum;
    
        
    [Header("Big Multiplier Properties")]
    public TMP_Text bigMultiplier;
    public GameObject bigMultiplierObject;
    public float bigMultiplierTime;
    public float targetMultiSize;

    
    
    private int score=0;
    private int multiplier = 1;

    new void Awake()
    {
        base.Awake();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        multiplierDecayTime = multiplierDecay;
        multiplierIncreaseNum = 0;
        resetScore();
    }

    private void Update()
    {
        if (multiplierIncreaseNum >= numToIncreaseMultiplier)
        {
            multiplierDecayTime = multiplierDecay;
            multiplierIncreaseNum = 0;
            increaseMultiplier();
        }
        
        //constantly decay multiplier
        // if (multiplier > 1)
        // {
        //     multiplierTimerSlider.value = multiplierDecayTime / multiplierDecay;
        // }

        // if (multiplierDecayTime <= 0)
        // {
        //     multiplierDecayTime = multiplierDecay;
        //     multiplierIncreaseNum = 0;
        //     decreaseMuliplier();
        // }
        // multiplierDecayTime -= Time.deltaTime;

    }

    public void increaseNum(int value)
    {
        multiplierIncreaseNum += value;
    }

    private void updateScore()
    {
        scoreValue.text = ""+score;
    }
    
    private void updateMultiplier()
    {
        multiValue.text = "x"+multiplier;
    }
    

    private void resetScore()
    {
        score = 0;
        multiplier = 1;
        //multiplierTimerSlider.gameObject.SetActive(false);
        bigMultiplierObject.SetActive(false);
        updateScore();
        updateMultiplier();

    }


    public void addToScore(int value)
    {
        score += value*multiplier;
        updateScore();
    }

    public void increaseMultiplier()
    {
        multiplier++;
        //multiplierTimerSlider.gameObject.SetActive(true);
        updateMultiplier();
        StartCoroutine(expandMultiEffect());
    }

    public void decreaseMuliplier()
    {
        multiplier--;
        if (multiplier <= 1)
        {
            //multiplierTimerSlider.gameObject.SetActive(false);
            multiplier = 1;
        }
        updateMultiplier();
    }

    public int getMultiplier()
    {
        return multiplier;
    }
    
    public IEnumerator expandMultiEffect()
    {
        bigMultiplierObject.SetActive(true);
        float newSize = 0;
        bigMultiplier.fontSize = newSize;
        float time = 0;
        bigMultiplier.text = "x" + multiplier;        
        while(newSize != targetMultiSize)
        {
            newSize = Mathf.Lerp(newSize, targetMultiSize, time/bigMultiplierTime);
            time += Time.deltaTime;
            bigMultiplier.fontSize = newSize;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        bigMultiplierObject.SetActive(false);
        
    }

    public int getScore()
    {
        return score;
    }
}
