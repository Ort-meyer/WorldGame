using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : BaseTraverseWeapon
{
    public float m_maxSpread; 

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        base.Update();

        // Fire
        if (m_canFire)
        {
            FireWeapon();
        }
    }

    protected override void FireWeapon()
    {
        base.FireWeapon();
        GameObject newBullet = Instantiate(m_projectilePrefab);

        newBullet.transform.position = transform.position;
        newBullet.transform.rotation = transform.rotation;
        // Create some spread
        float spreadx = Random.Range(-m_maxSpread, m_maxSpread);
        float spready = Random.Range(-m_maxSpread, m_maxSpread);
        newBullet.transform.Rotate(spreadx, spready, 0, Space.Self);
        newBullet.GetComponent<Rigidbody>().velocity = newBullet.transform.forward.normalized * m_exitVelocity;
        Collider[] ownTankColliders = m_ownUnit.GetComponentsInChildren<Collider>();
        foreach (Collider collider in ownTankColliders)
        {
            Physics.IgnoreCollision(newBullet.GetComponent<Collider>(), collider);
        }
    }
}
