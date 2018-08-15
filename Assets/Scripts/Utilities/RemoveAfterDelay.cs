using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveAfterDelay : MonoBehaviour
{

    public float m_timeToDelete;
    // Use this for initialization
    void Start()
    {
        Invoke("DeleteSelf", m_timeToDelete);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void DeleteSelf()
    {
        GameObject.Destroy(this.gameObject);
    }
}
