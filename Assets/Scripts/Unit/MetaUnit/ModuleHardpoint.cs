using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleHardpoint : MonoBehaviour
{
    //public int m_hardPointIndex;
    //public enum HardPointType { AttachesTo, Turret, Weapon }
    public bool attachesTo;
    public List<ModuleType> m_availableModules = new List<ModuleType>();
    public GameObject m_module;
    public GameObject m_connectedTo;
    // The top level transform of this module
    public GameObject m_moduleTopObject;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
