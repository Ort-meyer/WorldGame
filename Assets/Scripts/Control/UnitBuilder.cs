using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MetaUnits;

namespace MetaUnits
{
    //public enum HullVariant { LightHull, MediumHull, HeavyHull, TruckHull };
    //public enum WeaponVariant { HighVelocityCannon, MachineGun, MaDeuce, MediumBarrageLauncher, MediumCannon, MediumLauncher };
    //public enum TurretVariant { HeavyTurret, LightLaunchTurret, LightTurret, MediumTurret, MediumLaunchTurret };
    //public enum ProjectileVariant { LightRocket, LightSolidShot, MediumAtgm, SmallSolidShot, SolidShot };
}

[System.Serializable]
public class UnitBaseMap
{
    public UnitBase unitBase;
    public GameObject prefab;
}

[System.Serializable]
public class ModuleTypeMap
{
    public ModuleType moduleType;
    public GameObject prefab;
}


public class UnitBuilder : MonoBehaviour // Singleton<UnitBuilder> TODO make singleton when we don't rely on the inspector anymore
{

    public Transform DEBUGspawnPosition;
    //public MetaHull DEBUGmetaHull;
    //public MetaHull DEBUGmetaHull1;
    //public MetaHull DEBUGmetaHull2;
    



    public UnitBaseMap[] m_unitBasePrefabs = new UnitBaseMap[System.Enum.GetNames(typeof(UnitBase)).Length];
    public ModuleTypeMap[] m_modulePrefabs = new ModuleTypeMap[System.Enum.GetNames(typeof(ModuleType)).Length];
    //public GameObject[] m_weaponPrefabs = new GameObject[System.Enum.GetNames(typeof(WeaponVariant)).Length];
    //public GameObject[] m_turretPrefabs = new GameObject[System.Enum.GetNames(typeof(TurretVariant)).Length];
    //public GameObject[] m_projectilePrefabs = new GameObject[System.Enum.GetNames(typeof(ProjectileVariant)).Length];

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyUp(KeyCode.H))
        //{
        //    M_BuildHull(DEBUGmetaHull, DEBUGspawnPosition);
        //}
        //if (Input.GetKeyUp(KeyCode.G))
        //{
        //    M_BuildHull(DEBUGmetaHull2, DEBUGspawnPosition);
        //}
        //if (Input.GetKeyUp(KeyCode.F))
        //{
        //    M_BuildHull(DEBUGmetaHull1, DEBUGspawnPosition);
        //}
    }

    public GameObject M_BuildUnit(UnitBase unitBase, Transform spawnPosition)
    {
        GameObject newUnit = Instantiate(M_FindUnitBasePrefab(unitBase));
        newUnit.transform.position = spawnPosition.position;
        newUnit.transform.rotation = spawnPosition.rotation;

        return newUnit;
    }

    public GameObject M_BuildModule(ModuleType moduleType, GameObject parentModule, Transform hardPoint)
    {
        GameObject newModule = Instantiate(M_FindModulePrefab(moduleType), parentModule.transform);
        newModule.GetComponent<UnitModule>().M_Init(moduleType, parentModule, hardPoint);

        return newModule;
    }

    private GameObject M_FindUnitBasePrefab(UnitBase unitBase)
    {
        foreach(UnitBaseMap pair in m_unitBasePrefabs)
        {
            if(pair.unitBase == unitBase)
            {
                return pair.prefab;
            }
        }
        Debug.LogError("UnitBasePrefab mapping not found");
        return null; // Should never happen
    }

    private GameObject M_FindModulePrefab(ModuleType moduleType)
    {
        foreach (ModuleTypeMap pair in m_modulePrefabs)
        {
            if (pair.moduleType == moduleType)
            {
                return pair.prefab;
            }
        }
        Debug.LogError("ModuleType mapping not found");
        return null; // Should never happen
    }
}
