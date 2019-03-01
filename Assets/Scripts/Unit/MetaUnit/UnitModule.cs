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

// Class to save modules    
[System.Serializable]
public class Module
{
    public string moduleType;
    public List<Module> modules = new List<Module>();
    public int attachedToIndex;
    public Module() { }
}


public class UnitModule : MonoBehaviour
{
    public ModuleType m_moduleType;
    public Dictionary<int, UnitModule> m_modules = new Dictionary<int, UnitModule>();

    public void M_Init(ModuleType moduleType)
    {
        m_moduleType = moduleType;
        int index = 0;
        ModuleHardpoint[] moduleHardpoints = GetComponentsInChildren<ModuleHardpoint>();
        foreach (ModuleHardpoint hardpoint in moduleHardpoints)
        {
            hardpoint.m_moduleTopObject = this.gameObject;
            // Set index. Hope it's deterministic... (otherwise derive it off of it's localPosition?)
            hardpoint.m_hardpointIndex = index;
            index++;
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
