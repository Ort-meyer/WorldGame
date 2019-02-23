using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ModuleType
{
    // Turrets
    TurretHeavy, TurretLightLaunch, TurretLight, TurretMedium, TurretMediumLaunch,
    // Weapons
    WeaponHighVelocityCannon, WeaponMachineGun, WeaponMaDeuce, WeaponMediumBarrageLauncher, WeaponMediumCannon, WeaponMediumLauncher,
    // Hulls
    LightHull, MediumHull, HeavyHull, TruckHull // Should change names to HullHeavy etc. Try and see if easy?
};

public class UnitModule : MonoBehaviour
{
    public ModuleType m_moduleType;
    public Dictionary<int, UnitModule> m_modules = new Dictionary<int, UnitModule>();

    public void M_Init(ModuleType moduleType)
    {
        m_moduleType = moduleType;
        ModuleHardpoint[] moduleHardpoints = GetComponentsInChildren<ModuleHardpoint>();
        foreach (ModuleHardpoint hardpoint in moduleHardpoints)
        {
            hardpoint.m_moduleTopObject = this.gameObject;
        }
    }

    public void M_AddToModuleDict(int id, UnitModule module)
    {
        m_modules[id] = module;
    }

    public void M_RemoveFromModuleDict(int id)
    {
        m_modules.Remove(id);
    }
}
