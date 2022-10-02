using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Cinemachine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerPrefab;
    private GameObject player;

    public CinemachineVirtualCamera vcam;

    public int respawnTime;

    public TMP_Text pulseValueText;
    public float pulseTime;
    public GameObject pulseBeep;
    private float pulseTimer;

    public TMP_Text bombsLeftText;
    public int totalBombs;
    private int bombsLeft;

    public TMP_Text finalScore;
    public Canvas gameOverCanvas;

    private bool lockControls;
    private bool isGameOver;

    private bool played = false;
    
    private static GameManager instance = null;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this);
        }
    }

    public static GameManager Instance() {
        return instance;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        gameOverCanvas.gameObject.SetActive(false);
        isGameOver = false;
        lockControls = false;
        pulseTimer = pulseTime;
        bombsLeft = totalBombs;
        bombsLeftText.text = ""+totalBombs;
        resetPlayer();
    }
    
    void Update()
    {
        if (isGameOver) return;
        pulseTimer -= Time.deltaTime;
        //update pulse UI
        pulseValueText.text=String.Format("{0:0.0}", pulseTimer);

        if (pulseTimer <= 3 && !played)
        {
            played = true;
            StartCoroutine(playBeepOnCountdown());
        }
        
        if (pulseTimer <= 0)
        {
            bombsLeft--;
            bombsLeftText.text = ""+bombsLeft;

            //pulse
            player.GetComponent<PlayerExplosion>().OnPulse();
            if (bombsLeft <= 0)
            {
                StartCoroutine(gameOver());
                return;
            }
            pulseTimer = pulseTime;
            played = false;

        }
    }

    public IEnumerator playBeepOnCountdown()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(pulseBeep, this.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }
    }
    

    public void OnPlayerDeath()
    {
        //reset player stuff
        //StartCoroutine(respawn(respawnTime));

    }


    public void resetPlayer()
    {
        player = Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);
        player.GetComponent<PlayerExplosion>().OnDeathAction += OnPlayerDeath;
        vcam.Follow = player.transform;
    }

    public IEnumerator respawn(int seconds)
    {
        for (var i = seconds; i >0; i--)
        {
            print("Respawing in " + i);
            yield return new WaitForSeconds(1);
        }
        resetPlayer();
    }

    public GameObject getPlayer()
    {
        if (player == null)
        {
            return this.gameObject;
        }
        return player;
    }

    private IEnumerator gameOver()
    {
        isGameOver = true;
        lockControls = true;
        yield return new WaitForSeconds(2f);

        //show game over panel
        gameOverCanvas.gameObject.SetActive(true);
        finalScore.text = "" + ScoreManager.Instance().getScore();

        //lock player controls


    }

    public bool getControls()
    {
        return lockControls;
    }

    public void onMenuClick()
    {
        SceneManager.LoadScene("Menu");
    }
    
}
