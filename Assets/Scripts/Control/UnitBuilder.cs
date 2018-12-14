using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MetaUnits;

namespace MetaUnits
{
    public enum HullVariant { LightHull, MediumHull, HeavyHull, TruckHull };
    public enum WeaponVariant { HighVelocityCannon, MachineGun, MaDeuce, MediumBarrageLauncher, MediumCannon, MediumLauncher };
    public enum TurretVariant { HeavyTurret, LightLaunchTurret, LightTurret, MediumTurret, MediumLaunchTurret };
    public enum ProjectileVariant { LightRocket, LightSolidShot, MediumAtgm, SmallSolidShot, SolidShot };
}
public class UnitBuilder : MonoBehaviour // Singleton<UnitBuilder> TODO make singleton when we don't rely on the inspector anymore
{

    public Transform DEBUGspawnPosition;
    public MetaHull DEBUGmetaHull;
    public MetaHull DEBUGmetaHull1;
    public MetaHull DEBUGmetaHull2;

    public GameObject[] m_hullPrefabs = new GameObject[System.Enum.GetNames(typeof(HullVariant)).Length];
    public GameObject[] m_weaponPrefabs = new GameObject[System.Enum.GetNames(typeof(WeaponVariant)).Length];
    public GameObject[] m_turretPrefabs = new GameObject[System.Enum.GetNames(typeof(TurretVariant)).Length];
    public GameObject[] m_projectilePrefabs = new GameObject[System.Enum.GetNames(typeof(ProjectileVariant)).Length];

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.H))
        {
            M_BuildHull(DEBUGmetaHull, DEBUGspawnPosition);
        }
        if (Input.GetKeyUp(KeyCode.G))
        {
            M_BuildHull(DEBUGmetaHull2, DEBUGspawnPosition);
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            M_BuildHull(DEBUGmetaHull1, DEBUGspawnPosition);
        }
    }

    public GameObject M_BuildHull(MetaHull hullData, Transform spawnTransform)
    {
        GameObject newHull = Instantiate(m_hullPrefabs[(int)hullData.m_hullVariant]);
        newHull.transform.position = spawnTransform.position;
        newHull.transform.rotation = spawnTransform.rotation;
        

        hullData.M_BuildFromMeta(newHull);

        return newHull;
    }

    public GameObject M_BuildTurret(MetaTurret turretData, Transform hardPoint)
    {
        GameObject newTurret = Instantiate(m_turretPrefabs[(int)turretData.m_turretVariant], hardPoint);
        SetModuleTransform(ref newTurret, hardPoint);

        turretData.M_BuildFromMeta(newTurret);

        return newTurret;
    }

    public GameObject M_BuildWeapon(MetaWeapon weaponData, Transform hardPoint)
    {
        GameObject newWeapon = Instantiate(m_weaponPrefabs[(int)weaponData.m_weaponVariant], hardPoint);
        SetModuleTransform(ref newWeapon, hardPoint);

        return newWeapon;
    }

    public void SetModuleTransform(ref GameObject turret, Transform attachesTo)
    {
        ModuleHardpoint[] hardpoints = turret.GetComponentsInChildren<ModuleHardpoint>();
        foreach(ModuleHardpoint hardpoint in hardpoints)
        {
            if(hardpoint.m_hardPointType == ModuleHardpoint.HardPointType.AttachesTo)
            {
                turret.transform.position = attachesTo.position - hardpoint.transform.localPosition;
                turret.transform.rotation = attachesTo.rotation;
                return;
            }
        }
    }
}
