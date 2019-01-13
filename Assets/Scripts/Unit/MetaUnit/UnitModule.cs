using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ModuleType
{
    // Turrets
    TurretHeavy, TurretLightLaunch, TurretLight, TurretMedium, TurretMediumLaunch,
    // Weapons
    WeaponHighVelocityCannon, WeaponMachineGun, WeaponMaDeuce, WeaponMediumBarrageLauncher, WeaponMediumCannon, WeaponMediumLauncher
};

public class UnitModule : MonoBehaviour
{
    public ModuleType m_moduleType;
    public GameObject m_parentEntity;
    
    public void M_Init(ModuleType moduleType, GameObject parentEntity, Transform hardpoint)
    {
        m_moduleType = moduleType;
        m_parentEntity = parentEntity;
        M_AttachTo(hardpoint);
    }

    public void M_Build(Transform attachTransform)
    {
    }

    private void M_AttachTo(Transform attachTo)
    {
        ModuleHardpoint[] hardpoints = GetComponentsInChildren<ModuleHardpoint>();
        foreach (ModuleHardpoint hardpoint in hardpoints)
        {
            if(hardpoint.attachesTo)
            {
                transform.position = attachTo.position - hardpoint.transform.localPosition;
                transform.rotation = attachTo.rotation;
                hardpoint.attachesTo = attachTo;
            }
        }
    }

}
