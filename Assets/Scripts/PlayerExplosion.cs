using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerExplosion : MonoBehaviour
{

    public Action OnDeathAction;

    public float radius;
    public LayerMask mask;


    private RaycastHit[] hitInfo;
    

    void OnFire()
    {
        //explode (including spherecast)
        hitInfo = Physics.SphereCastAll(this.transform.position, radius, Vector3.up, radius,mask);
        if (hitInfo.Length > 0)
        {
            foreach (var hit in hitInfo)
            {
                hit.collider.gameObject.GetComponentInParent<EnemyExplosion>().explode();
            }
        }
        //inform the action that we've exploded
        OnDeathAction?.Invoke();
        
        //spawn explosion effects
        
        //kill player
        Destroy(this.gameObject);
    }
}
