using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class InputMover : MonoBehaviour
{
    [Header("Movement Properties")] 
    public float speed;

    private Vector3 moveAmount;
    private Rigidbody rb;

    private PlayerInput input;

    private void OnMove(InputValue value)
    {
        moveAmount = value.Get<Vector2>();
    }

    // Start is called before the first frame update
    void Start()
    {
        moveAmount = Vector3.zero;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rb.velocity = moveAmount*speed;
    }
}
