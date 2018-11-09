using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUGScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 position = transform.position + transform.right * 10 * 0.5f;
        Vector3 force = transform.forward;

        Helpers.DrawDebugLine(position, position + force);
        GetComponent<Rigidbody>().AddForceAtPosition(force, position);
    }
}
