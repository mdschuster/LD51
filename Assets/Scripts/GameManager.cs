using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WFG.Utilities;

public class GameManager : Singleton<GameManager>
{
    public GameObject PlayerPrefab;
    
    new void Awake()
    {
        base.Awake();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
