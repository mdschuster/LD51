using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class MoveInDirection : MonoBehaviour
{

    public Vector3 direction;
    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        direction.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = this.transform.position;
        pos += direction * speed * Time.deltaTime;
        this.transform.position = pos;
    }
}
