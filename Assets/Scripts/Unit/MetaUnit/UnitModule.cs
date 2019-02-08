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
    public List<UnitModule> m_modules = new List<UnitModule>();
}
