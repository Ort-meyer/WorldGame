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
    public GameObject m_moduleTopTransform;

    // Use this for initialization
    void Start()
    {
        // Set module top transform.We don't know if this hardpoint belongs to a unit or a module (this is bad, should be improved)
        Unit unit = GetComponentInParent<Unit>();
        if (unit)
        {
            m_moduleTopTransform = unit.gameObject;
        }
        else
        {
            UnitModule module = GetComponentInParent<UnitModule>();
            if (module)
            {
                m_moduleTopTransform = module.gameObject;
            }
            else // Should never happen
            {
                Debug.LogError("ModuleHardpoint: module is neither unit nor unitmodule");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
