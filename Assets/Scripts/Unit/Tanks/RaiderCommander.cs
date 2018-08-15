using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiderCommander : MonoBehaviour
{

    public Transform m_target;

    // Use this for initialization
    void Start()
    {
        GetComponentInChildren<BaseTurret>().M_SetTarget(m_target);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
