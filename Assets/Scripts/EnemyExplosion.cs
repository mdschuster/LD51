using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplosion : MonoBehaviour
{
    public int pointValue;
    public float radius;
    public LayerMask mask;
    
    
    
    public Action<int> OnDeathAction;

    private RaycastHit[] hitInfo;
    private bool alreadyHit = false;


    public void explode()
    {
        if (alreadyHit) return;
        alreadyHit = true;
        OnDeathAction?.Invoke(pointValue);
        
        StartCoroutine(waitToExplode());

    }

    private IEnumerator waitToExplode()
    {
        yield return new WaitForSeconds(0.1f);
        //display vfx

        hitInfo = Physics.SphereCastAll(this.transform.position, radius, Vector3.up, radius,mask);
        print(hitInfo.Length);
        if (hitInfo.Length > 0)
        {
            foreach (var hit in hitInfo)
            {
                hit.collider.gameObject.GetComponentInParent<EnemyExplosion>().explode();
            }
        }

        //destroy this object (it's pooled)
        this.gameObject.SetActive(false);
    }
    
    
    
    
}
