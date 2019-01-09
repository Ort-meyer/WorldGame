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

public class UnitModule
{

    protected GameObject m_entity;
    protected ModuleType m_moduleType;
    
    public UnitModule(ModuleType moduleType)
    {
        m_moduleType = moduleType;
    }

    public void M_Build(Transform attachTransform)
    {
    }

}
