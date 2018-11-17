using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KineticImpact : BaseImpact
{

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
            float speed = GetComponent<Rigidbody>().velocity.magnitude;
            float damage = CalculateDamage(speed);
            
            hitUnit.M_InflictDamage(damage);
        }

        Destroy(this.gameObject);
    }
     
    private float CalculateDamage(float movementSpeed)
    {
        // Basic momentum calculation for now
        float mass = GetComponent<Rigidbody>().mass;
        return movementSpeed * mass;
    }
}
