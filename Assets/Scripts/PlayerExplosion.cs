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

    public MeshRenderer pulseRenderer;


    private Collider[] hitInfo;

    private void Start()
    {
        explosionCircle.transform.localScale = new Vector3(radius, radius, 0f);
    }

    public void OnPulse()
    {
        if (GameManager.Instance().getControls()) return;
        //explode (including spherecast)
        hitInfo = Physics.OverlapSphere(this.transform.position, radius,mask);
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
        StartCoroutine(activatePulseFX());

        //kill player
        //Destroy(this.gameObject);
    }

    public IEnumerator playExplosionAudio()
    {
        yield return new WaitForSeconds(0.2f);
        Instantiate(ExplosionAudio, this.transform.position, Quaternion.identity);
    }

    public IEnumerator activatePulseFX()
    {
        
        pulseRenderer.material.SetFloat("_Intensity",25);
        //pulseRenderer.material.SetFloat("_AlphaIntensity",1);
        float targetAlpha = 1;
        float alpha = 0;
        float time = 0;
        //ramp alpha up
        while(alpha != targetAlpha)
        {
            alpha = Mathf.Lerp(alpha, targetAlpha, time/0.25f);
            time += Time.deltaTime;
            pulseRenderer.material.SetFloat("_AlphaIntensity",alpha);

            yield return null;
        }

        yield return new WaitForSeconds(0.25f);
        
        //ramp alpha down
        targetAlpha = 0;
        alpha = 1;
        time = 0;
        while(alpha != targetAlpha)
        {
            alpha = Mathf.Lerp(alpha, targetAlpha, time/0.5f);
            time += Time.deltaTime;
            pulseRenderer.material.SetFloat("_AlphaIntensity",alpha);

            yield return null;
        }

        pulseRenderer.material.SetFloat("_Intensity",0);
        pulseRenderer.material.SetFloat("_AlphaIntensity",0);

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
