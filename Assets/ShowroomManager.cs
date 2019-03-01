﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using MetaUnits;
using System.IO;

public class ShowroomManager : MonoBehaviour
{
    public Dropdown m_hullDropDown;
    public Dropdown m_moduleDropdown;
    public UnitBuilder m_unitBuilder;
    public Transform m_spawnPosition;

    public Button m_saveButton;
    public InputField m_inputField;

    public Button m_loadButton;

    private GameObject m_currentVehicle;
    private ModuleHardpoint m_currentHardpoint;
    // Use this for initialization
    void Start()
    {
        List<ModuleTypeMap> modulePrefabs = new List<ModuleTypeMap>(m_unitBuilder.m_modulePrefabs);
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
        // Add all hulls to menu (this seems possibly silly)
        foreach (ModuleTypeMap modulePair in modulePrefabs)
        {
            string moduleName = modulePair.prefab.name;
            if (moduleName.ToLower().Contains("hull"))
            {
                options.Add(new Dropdown.OptionData(moduleName));
            }
        }
        m_hullDropDown.ClearOptions();
        m_hullDropDown.AddOptions(options);
        m_hullDropDown.onValueChanged.AddListener(delegate { HullSelected(m_hullDropDown); });

        m_moduleDropdown.ClearOptions();
        m_moduleDropdown.onValueChanged.AddListener(delegate { ModuleSelected(m_moduleDropdown); });

        m_saveButton.onClick.AddListener(delegate { SaveToFile(); });
        m_loadButton.onClick.AddListener(delegate { LoadFromFile(); });
}

    void HullSelected(Dropdown change)
    {
        if (m_currentVehicle != null)
        {
            Destroy(m_currentVehicle);
        }

        ModuleType hullType = (ModuleType)Enum.Parse(typeof(ModuleType), change.captionText.text);
        m_currentVehicle = m_unitBuilder.M_BuildUnit(hullType, m_spawnPosition);
    }

    void ModuleSelected(Dropdown change)
    {
        if (m_currentVehicle != null)
        {
            // Remove old, if it exists
            if (m_currentHardpoint.m_connectedTo)
            {
                Destroy(m_currentHardpoint.m_connectedTo);
            }

            // Create new module
            ModuleType type = (ModuleType)Enum.Parse(typeof(ModuleType), change.captionText.text);
            m_currentHardpoint.m_connectedTo = m_unitBuilder.M_BuildModule(type, m_currentHardpoint);
        }
    }

    public void M_HardpointSelected(ModuleHardpoint hardPoint)
    {
        m_currentHardpoint = hardPoint;
        m_moduleDropdown.ClearOptions();
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
        foreach (ModuleType moduleType in hardPoint.m_availableModules)
        {
            options.Add(new Dropdown.OptionData(moduleType.ToString()));
        }
        m_moduleDropdown.AddOptions(options);
    }
    

    void SaveToFile()
    {
        Module unitToSave = CoolRecursiveMethod(m_currentVehicle.GetComponent<UnitModule>());
        string jsonString = JsonUtility.ToJson(unitToSave);
        File.WriteAllText("Test.unit", jsonString);
    }

    void LoadFromFile()
    {
        string jsonString;
        var fileStream = new FileStream("Test.unit", FileMode.Open, FileAccess.Read);
        using (var streamReader = new StreamReader(fileStream))
        {
            jsonString = streamReader.ReadToEnd();
        }

        Module unitToLoad = JsonUtility.FromJson<Module>(jsonString);
        GameObject newUnit = m_unitBuilder.M_BuildUnit((ModuleType)Enum.Parse(typeof(ModuleType), unitToLoad.moduleType), m_spawnPosition);
        Destroy(m_currentVehicle);
        m_currentVehicle = newUnit;
        m_currentHardpoint = null;
        foreach(Module newModule in unitToLoad.modules)
        {
            CoolRecursiveMethodLoad(newModule, newUnit);
        }
    }

    ModuleHardpoint FindHardpointByIndex(GameObject module, int index)
    {
        ModuleHardpoint correctHardpoint = null;
        foreach(ModuleHardpoint hardpoint in module.GetComponentsInChildren<ModuleHardpoint>())
        {
            if (hardpoint.m_hardpointIndex == index)
            {
                correctHardpoint = hardpoint;
            }
        }
        return correctHardpoint;
    }

    Module CoolRecursiveMethod(UnitModule currentModule)
    {
        Module moduleToSave = new Module();
        moduleToSave.moduleType = currentModule.m_moduleType.ToString();
        foreach(UnitModule module in currentModule.m_modules.Values)
        {
            moduleToSave.modules.Add(CoolRecursiveMethod(module));
        }
        return moduleToSave;
    }

    GameObject CoolRecursiveMethodLoad(Module moduleToBuild, GameObject parent)
    {
        GameObject newModuleObject = m_unitBuilder.M_BuildModule(Helpers.StringToModuleType(moduleToBuild.moduleType), FindHardpointByIndex(parent, moduleToBuild.attachedToIndex));
        foreach (Module newModule in moduleToBuild.modules)
        {
            CoolRecursiveMethodLoad(newModule, newModuleObject);
        }
        return newModuleObject;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
