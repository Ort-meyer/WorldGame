using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MetaUnits;
//using System;

[System.Serializable]

public class MetaTurret
{
    public TurretVariant m_turretVariant;
    public List<MetaTurret> m_metaTurrets = new List<MetaTurret>();
    public List<MetaWeapon> m_metaWeapons = new List<MetaWeapon>();

    private List<GameObject> m_ownTurrets = new List<GameObject>();
    private List<GameObject> m_ownWeapons = new List<GameObject>();

    private List<ModuleHardpoint> m_hardPoints = new List<ModuleHardpoint>();

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void M_BuildFromMeta(GameObject builtTurret)
    {
        UnitBuilder unitBuilder = UnityEngine.Object.FindObjectOfType<UnitBuilder>();
        // Get hardpoints
        m_hardPoints = new List<ModuleHardpoint>(builtTurret.GetComponentsInChildren<ModuleHardpoint>());

        // Build turrets attached to this turret
        foreach(MetaTurret turret in m_metaTurrets)
        {
            ModuleHardpoint hardpoint;
            bool success = FindHardpoint(ModuleHardpoint.HardPointType.Turret, out hardpoint);
            if(success)
            {
                unitBuilder.M_BuildTurret(m_metaTurrets[hardpoint.m_hardPointIndex], hardpoint.transform);
            }
            else
            {
                // No hardpoint exists for this turret. Shouldn't happen
            }
        }

        foreach (MetaWeapon weapon in m_metaWeapons)
        {
            ModuleHardpoint hardpoint;
            bool success = FindHardpoint(ModuleHardpoint.HardPointType.Turret, out hardpoint);
            if (success)
            {
                unitBuilder.M_BuildTurret(m_metaTurrets[hardpoint.m_hardPointIndex], hardpoint.transform);
            }
            else
            {
                // No hardpoint exists for this turret. Shouldn't happen
            }
        }

        // Build parts to hardpoints from relevant metadata
        foreach (ModuleHardpoint hardpoint in m_hardPoints)
        {
            if (hardpoint.m_hardPointType == ModuleHardpoint.HardPointType.Turret)
            {
                if (m_metaTurrets.Count < hardpoint.m_hardPointIndex)
                {
                    unitBuilder.M_BuildTurret(m_metaTurrets[hardpoint.m_hardPointIndex], hardpoint.transform);
                }
            }

            else if (hardpoint.m_hardPointType == ModuleHardpoint.HardPointType.Weapon)
            {
                if (m_metaWeapons.Count < hardpoint.m_hardPointIndex)
                {
                    unitBuilder.M_BuildWeapon(m_metaWeapons[hardpoint.m_hardPointIndex], hardpoint.transform);
                }
            }
        }
    }

    private bool FindHardpoint(ModuleHardpoint.HardPointType hardPointType, out ModuleHardpoint foundHardpoint)
    {
        bool success = false;
        foundHardpoint = null;
        foreach (ModuleHardpoint hardpoint in m_hardPoints)
        {
            if (hardpoint.m_hardPointType == ModuleHardpoint.HardPointType.Turret)
            {
                if (m_metaTurrets.Count < hardpoint.m_hardPointIndex)
                {
                    foundHardpoint = hardpoint;
                    success = true;
                }
            }

            else if (hardpoint.m_hardPointType == ModuleHardpoint.HardPointType.Weapon)
            {
                if (m_metaWeapons.Count < hardpoint.m_hardPointIndex)
                {
                    foundHardpoint = hardpoint;
                    success = true;
                }
            }
        }
        return success;
    }
}
