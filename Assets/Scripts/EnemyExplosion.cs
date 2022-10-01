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
        //print("Hit enemy");
        OnDeathAction?.Invoke(pointValue);
        //display vfx
        
        
        //destroy this object
        Destroy(this.gameObject);


        StartCoroutine(waitToExplode());



    }

    private IEnumerator waitToExplode()
    {
        yield return new WaitForSeconds(0.25f);
        hitInfo = Physics.SphereCastAll(this.transform.position, radius, Vector3.up, radius,mask);
        if (hitInfo.Length > 0)
        {
            foreach (var hit in hitInfo)
            {
                hit.collider.gameObject.GetComponentInParent<EnemyExplosion>().explode();
            }
        }
    }
    
    
    
    
}
