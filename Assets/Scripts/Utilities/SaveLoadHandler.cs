using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveLoadHandler : MonoBehaviour
{
    const string m_unitSaveFolder = @"SavedUnits\";
    const string m_fileFormat = ".unit";

    List<string> GetSavedUnitNames()
    {
        string fullSaveDirectory = Directory.GetCurrentDirectory() + m_unitSaveFolder;
        return new List<string>(Directory.GetFiles(fullSaveDirectory));
    }

    public void SaveToFile(string fileName, string saveString)
    {
        // See if unit name already exists, and if we should overwrite. Make check separate method?
        string fullFileName = m_unitSaveFolder + fileName + m_fileFormat;
        File.WriteAllText(fileName, saveString);

    }

    public string LoadFromFile(string loadFile)
    {
        string loadedFile;
        var fileStream = new FileStream(loadFile, FileMode.Open, FileAccess.Read);
        using (var streamReader = new StreamReader(fileStream))
        {
            loadedFile = streamReader.ReadToEnd();
        }
        return loadFile;
    }
}
