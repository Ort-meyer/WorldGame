using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleHardpoint : MonoBehaviour
{
    public int m_hardPointIndex;
    public enum HardPointType { AttachesTo, Turret, Weapon }
    public HardPointType m_hardPointType;

    public List<MetaUnits.TurretVariant> m_availableTurrets = new List<MetaUnits.TurretVariant>();
    public List<MetaUnits.WeaponVariant> m_availableWeapons = new List<MetaUnits.WeaponVariant>();

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
