using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseImpact : MonoBehaviour
{
    public float m_damage;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject hitObject = collision.gameObject;
        BaseUnit hitUnit = hitObject.GetComponent<BaseUnit>();

        if (hitUnit)
        {
            hitUnit.M_InflictDamage(m_damage);
        }
        Destroy(this.gameObject);
    }
}
