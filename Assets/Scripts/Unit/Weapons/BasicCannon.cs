using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCannon : BaseBallisticWeapon
{


    // Traverse (not yet implemented)
    public float m_maxTraverse;
    public float m_traverseSpeed;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        
    }


    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        // Fire
        if (m_canFire)
        {
            FireWeapon();
        }
    }



}
