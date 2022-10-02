using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstanceRotate : MonoBehaviour
{

    public Vector3 rotSpeed;
    
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(rotSpeed.x,rotSpeed.y,rotSpeed.z));
    }
}
