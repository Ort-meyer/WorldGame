using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveLoadHandler
{
    const string m_unitSaveFolder = @"SavedUnits\";
    const string m_fileFormat = ".unit";

    List<string> GetSavedUnitNames()
    {
        string fullSaveDirectory = Directory.GetCurrentDirectory() + m_unitSaveFolder;
        return new List<string>(Directory.GetFiles(fullSaveDirectory));
    }

    public void M_SaveToFile(string fileName, string saveString)
    {
        // See if unit name already exists, and if we should overwrite. Make check separate method?
        string fullFileName = m_unitSaveFolder + fileName + m_fileFormat;
        File.WriteAllText(fullFileName, saveString);

    }

    public string M_LoadFromFile(string loadFile)
    {
        string loadedFile;
        var fileStream = new FileStream(loadFile, FileMode.Open, FileAccess.Read);
        using (var streamReader = new StreamReader(fileStream))
        {
            loadedFile = streamReader.ReadToEnd();
        }
        return loadFile;
    }

    public void M_SaveUnitToFile(string unitName, UnitModule unitModuleToSave)
    {
        SavedModule unitToSave = CoolRecursiveMethod(unitModuleToSave);
        string jsonString = JsonUtility.ToJson(unitToSave);
        M_SaveToFile(unitName, jsonString);
    }

    public void M_LoadUnitFromFile(string unitName)
    {
        string jsonString = M_LoadFromFile(unitName);

        SavedModule unitToLoad = JsonUtility.FromJson<SavedModule>(jsonString);
        GameObject newUnit = m_unitBuilder.M_BuildUnit((ModuleType)Enum.Parse(typeof(ModuleType), unitToLoad.moduleType), m_spawnPosition);
        Destroy(m_currentVehicle);
        m_currentVehicle = newUnit;
        m_currentHardpoint = null;
        foreach (SavedModule newModule in unitToLoad.modules)
        {
            CoolRecursiveMethodLoad(newModule, newUnit);
        }
    }

    ModuleHardpoint FindHardpointByIndex(GameObject module, int index)
    {
        ModuleHardpoint correctHardpoint = null;
        foreach (ModuleHardpoint hardpoint in module.GetComponentsInChildren<ModuleHardpoint>())
        {
            if (hardpoint.m_hardpointIndex == index)
            {
                correctHardpoint = hardpoint;
            }
        }
        return correctHardpoint;
    }

    SavedModule CoolRecursiveMethod(UnitModule currentModule)
    {
        SavedModule moduleToSave = new SavedModule();
        moduleToSave.moduleType = currentModule.m_moduleType.ToString();
        foreach (UnitModule module in currentModule.m_modules.Values)
        {
            moduleToSave.modules.Add(CoolRecursiveMethod(module));
        }
        return moduleToSave;
    }

    GameObject CoolRecursiveMethodLoad(SavedModule moduleToBuild, GameObject parent)
    {
        GameObject newModuleObject = m_unitBuilder.M_BuildModule(Helpers.StringToModuleType(moduleToBuild.moduleType), FindHardpointByIndex(parent, moduleToBuild.attachedToIndex));
        foreach (SavedModule newModule in moduleToBuild.modules)
        {
            CoolRecursiveMethodLoad(newModule, newModuleObject);
        }
        return newModuleObject;
    }
}
