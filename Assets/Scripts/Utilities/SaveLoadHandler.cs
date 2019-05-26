using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveLoadHandler : MonoBehaviour
{
    private UnitBuilder m_unitBuilder;
    const string m_unitSaveFolder = @"SavedUnits\";
    const string m_fileFormat = ".unit";

    private void Start()
    {
        
    }

    List<string> GetSavedUnitNames()
    {
        m_unitBuilder = GetComponent<UnitBuilder>();
        string fullSaveDirectory = Directory.GetCurrentDirectory() + m_unitSaveFolder;
        return new List<string>(Directory.GetFiles(fullSaveDirectory));
    }

    private void SaveToFile(string fileName, string saveString)
    {
        // See if unit name already exists, and if we should overwrite. Make check separate method?
        string fullFileName = m_unitSaveFolder + fileName + m_fileFormat;
        File.WriteAllText(fullFileName, saveString);

    }

    private string LoadFromFile(string loadFile)
    {
        string loadedFile;
        var fileStream = new FileStream(loadFile, FileMode.Open, FileAccess.Read);
        using (var streamReader = new StreamReader(fileStream))
        {
            loadedFile = streamReader.ReadToEnd();
        }
        return loadedFile;
    }

    public void M_SaveUnitToFile(string unitName, UnitModule unitModuleToSave)
    {
        SavedModule unitToSave = SaveSubModules(unitModuleToSave);
        string jsonString = JsonUtility.ToJson(unitToSave);
        SaveToFile(unitName, jsonString);
    }

    public SavedModule M_LoadUnitFromFile(string unitName)
    {
        string jsonString = LoadFromFile(m_unitSaveFolder + unitName + m_fileFormat);

         SavedModule unitToLoad = JsonUtility.FromJson<SavedModule>(jsonString);
        return unitToLoad;

        //////// This stuff should be in showroom manager, or buildroom
        //GameObject newUnit = m_unitBuilder.M_BuildUnit((ModuleType)Enum.Parse(typeof(ModuleType), unitToLoad.moduleType), m_spawnPosition);
        //Destroy(m_currentVehicle);
        //m_currentVehicle = newUnit;
        //m_currentHardpoint = null;
    }

    SavedModule SaveSubModules(UnitModule currentModule)
    {
        SavedModule moduleToSave = new SavedModule();
        moduleToSave.moduleType = currentModule.m_moduleType.ToString();
        foreach (UnitSubModule module in currentModule.m_modules.Values)
        {
            SavedModule subModule = SaveSubModules(module);
            subModule.attachedToIndex = module.m_attachedToIndex;
            moduleToSave.modules.Add(subModule);
        }
        return moduleToSave;
    }
}
