//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using MetaUnits;

//[System.Serializable]
//public class MetaHull
//{
//    public HullVariant m_hullVariant;
//    public List<MetaTurret> m_metaTurrets = new List<MetaTurret>();
//    public List<MetaWeapon> m_metaWeapons = new List<MetaWeapon>();

//    private List<GameObject> m_ownTurrets = new List<GameObject>();
//    private List<GameObject> m_ownWeapons = new List<GameObject>();

//    private List<ModuleHardpoint> m_hardPoints = new List<ModuleHardpoint>();

//    // Use this for initialization
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }

//    public void M_BuildFromMeta(GameObject builtHull)
//    {
//        UnitBuilder unitBuilder = Object.FindObjectOfType<UnitBuilder>();
//        // Get hardpoints
//        m_hardPoints = new List<ModuleHardpoint>(builtHull.GetComponentsInChildren<ModuleHardpoint>());

//        // Build parts to hardpoints from relevant metadata
//        foreach (ModuleHardpoint hardpoint in m_hardPoints)
//        {
//            if (hardpoint.m_hardPointType == ModuleHardpoint.HardPointType.Turret)
//            {
//                if (m_metaTurrets.Count < hardpoint.m_hardPointIndex)
//                {
//                    unitBuilder.M_BuildTurret(m_metaTurrets[hardpoint.m_hardPointIndex], hardpoint.transform);
//                }
//            }

//            else if (hardpoint.m_hardPointType == ModuleHardpoint.HardPointType.Weapon)
//            {
//                if (m_metaWeapons.Count < hardpoint.m_hardPointIndex)
//                {
//                    unitBuilder.M_BuildWeapon(m_metaWeapons[hardpoint.m_hardPointIndex], hardpoint.transform);
//                }
//            }
//        }
//    }
//}
