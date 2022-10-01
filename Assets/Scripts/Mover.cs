using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Mover : MonoBehaviour
{

    public float speed;

    private Vector2 dir;
    private Vector2 moveAmount;

    private void FixedUpdate()
    {
        Vector3 pos = this.transform.position;
        pos += new Vector3(dir.x,dir.y,0f) * speed*Time.deltaTime;
        this.transform.position = pos;    }

    public void setDirection(Vector2 dir)
    {
        this.dir = dir;
        this.dir.Normalize();
        //transform.rotation = Quaternion.LookRotation(dir);
    }

    public void setSpeed(float speed)
    {
        this.speed = speed;
    }
}
