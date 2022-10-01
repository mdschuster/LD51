using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyExplosion : MonoBehaviour
{
    public int pointValue;
    public float radius;
    public LayerMask mask;

    public GameObject explositionCircle;
    public GameObject scoreGraphic;

    public Action<int> OnDeathAction;

    private Collider[] hitInfo;
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
        //waitToExplode();

    }

    private IEnumerator waitToExplode()
    {
        yield return new WaitForSeconds(0.1f);
        GameObject go=Instantiate(scoreGraphic, this.transform.position, Quaternion.identity);
        go.GetComponentInChildren<TMP_Text>().text = ""+pointValue*ScoreManager.Instance().getMultiplier();
        //display vfx

        hitInfo = Physics.OverlapSphere(this.transform.position, radius,mask);
        if (hitInfo.Length > 0)
        {
            foreach (var hit in hitInfo)
            {
                hit.gameObject.GetComponent<EnemyExplosion>().explode();
            }
        }

        OnDeathAction -= ScoreManager.Instance().addToScore;
        //destroy this object (it's pooled)
        this.gameObject.SetActive(false);
        
    }

    public void resetEnemy()
    {
        explositionCircle.transform.localScale = new Vector3(radius, radius, 1f);
        alreadyHit = false;
    }




}
