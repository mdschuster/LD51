using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplosion : MonoBehaviour
{
    public int pointValue;
    public float radius;
    public LayerMask mask;

    public GameObject explositionCircle;
    
    
    
    public Action<int> OnDeathAction;

    private RaycastHit[] hitInfo;
    private bool alreadyHit = false;

    private void Start()
    {
        OnDeathAction += ScoreManager.Instance().addToScore;
    }


    public void explode()
    {
        if (alreadyHit) return;
        alreadyHit = true;
        OnDeathAction?.Invoke(pointValue);
        ScoreManager.Instance().increaseNum(1);
        
        StartCoroutine(waitToExplode());

    }

    private IEnumerator waitToExplode()
    {
        yield return new WaitForSeconds(0.1f);
        //display vfx

        hitInfo = Physics.SphereCastAll(this.transform.position, radius, Vector3.up, radius,mask);
        if (hitInfo.Length > 0)
        {
            foreach (var hit in hitInfo)
            {
                hit.collider.gameObject.GetComponentInParent<EnemyExplosion>().explode();
            }
        }

        OnDeathAction -= ScoreManager.Instance().addToScore;
        //destroy this object (it's pooled)
        this.gameObject.SetActive(false);
        
    }

    public void resetEnemy()
    {
        explositionCircle.transform.localScale = new Vector3(radius, radius, 1f);
    }
    
    
    
    
}
