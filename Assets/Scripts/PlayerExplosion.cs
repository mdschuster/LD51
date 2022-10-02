using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Mathematics;

public class PlayerExplosion : MonoBehaviour
{

    public Action OnDeathAction;

    public float radius;
    public LayerMask mask;
    public GameObject explosionCircle;

    public GameObject ExplosionAudio;
    public GameObject PulseAudio;


    private Collider[] hitInfo;

    private void Start()
    {
        explosionCircle.transform.localScale = new Vector3(radius, radius, 0f);
    }

    public void OnFire()
    {
        if (GameManager.Instance().getControls()) return;
        //explode (including spherecast)
        hitInfo = Physics.OverlapSphere(this.transform.position, radius,mask);
        print(hitInfo.Length);
        if (hitInfo.Length > 0)
        {
            StartCoroutine(playExplosionAudio());
            foreach (var hit in hitInfo)
            {
                hit.gameObject.GetComponent<EnemyExplosion>().explode();
            }
        }
        //inform the action that we've exploded
        OnDeathAction?.Invoke();
        
        //spawn explosion effects
        Instantiate(PulseAudio, this.transform.position, Quaternion.identity);

        //kill player
        //Destroy(this.gameObject);
    }

    public IEnumerator playExplosionAudio()
    {
        yield return new WaitForSeconds(0.15f);
        Instantiate(ExplosionAudio, this.transform.position, Quaternion.identity);
    }
    
    // private void OnDrawGizmos()
    // {
    //     Gizmos.DrawWireSphere(transform.position, radius);
    //
    //     RaycastHit hit;
    //     if (Physics.SphereCast(transform.position, radius, transform.forward * radius, out hit, radius, mask))
    //     {
    //         Gizmos.color = Color.green;
    //         Vector3 sphereCastMidpoint = transform.position + (transform.forward * hit.distance);
    //         Gizmos.DrawWireSphere(sphereCastMidpoint, radius);
    //         Gizmos.DrawSphere(hit.point, 0.1f);
    //         Debug.DrawLine(transform.position, sphereCastMidpoint, Color.green);
    //     }
    //     else
    //     {
    //         Gizmos.color = Color.red;
    //         Vector3 sphereCastMidpoint = transform.position + (transform.forward * (radius-radius));
    //         Gizmos.DrawWireSphere(sphereCastMidpoint, radius);
    //         Debug.DrawLine(transform.position, sphereCastMidpoint, Color.red);
    //     }
    // }
}
