﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardRound : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        // This is debuggy since my bullet "stands up". TODOta fix when better model exists
        transform.Rotate(90, 0, 0, Space.Self);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(GetComponent<Rigidbody>().velocity);
        //transform.forward = GetComponent<Rigidbody>().velocity.normalized;
        //transform.up = Vector3.Cross(transform.right, transform.forward).normalized;
        // This is debuggy since my bullet "stands up". TODOta fix when better model exists
        transform.Rotate(90, 0, 0, Space.Self);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}
