using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    public enum ProjectileType { smallDumb, lightDumb, mediumDumb, mediumSelfPropelled, lightSelfPropelled};

    public ProjectileType m_projectileType;
    protected GameObject m_firingWeapon;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void M_ProjectileFired(GameObject firingWeapon)
    {
        m_firingWeapon = firingWeapon;
    }
}
