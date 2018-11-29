using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MetaUnits;

namespace MetaUnits
{
    public enum HullVariant { SmallHull, MediumHull, HeavyHull, TruckHull };
    public enum WeaponVariant { HighVelocityCannon, MachineGun, MaDeuce, MediumBarrageLauncher, MediumCannon, MediumLauncher };
    public enum TurretVariant { HeavyTurret, LightLaunchTurret, LightTurret, MediumTurret };
    public enum ProjectileVariant { LightRocket, LightSolidShot, MediumAtgm, SmallSolidShot, SolidShot };
}
public class UnitBuilder : MonoBehaviour // Singleton<UnitBuilder> TODO make singleton when we don't rely on the inspector anymore
{

    public Transform DEBUGspawnPosition;
    public MetaHull DEBUGmetaHull;

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
        newTurret.transform.position = hardPoint.position;
        newTurret.transform.rotation = hardPoint.transform.rotation;

        turretData.M_BuildFromMeta(newTurret);

        return newTurret;
    }

    public GameObject M_BuildWeapon(MetaWeapon weaponData, Transform hardPoint)
    {
        GameObject newWeapon = Instantiate(m_weaponPrefabs[(int)weaponData.m_weaponVariant], hardPoint);
        newWeapon.transform.position = hardPoint.position;
        newWeapon.transform.rotation = hardPoint.transform.rotation;

        return newWeapon;
    }
}
