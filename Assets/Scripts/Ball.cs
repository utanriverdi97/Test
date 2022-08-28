using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;
    public bool isOut;
    public bool isIn;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Respawn"))
        { 
            GetComponent<SphereCollider>().material.dynamicFriction = 999;
        }
    }

    private void FixedUpdate()
    {
        //rb.velocity=Vector3.down*5;
    }
}
