using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using WFG.Utilities;

public class GameManager : Singleton<GameManager>
{
    public GameObject PlayerPrefab;
    private GameObject player;

    public CinemachineVirtualCamera vcam;

    public int respawnTime;
    
    new void Awake()
    {
        base.Awake();
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        resetPlayer();
    }
    

    public void OnPlayerDeath()
    {
        //reset player stuff
        StartCoroutine(respawn(respawnTime));

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
    
}
