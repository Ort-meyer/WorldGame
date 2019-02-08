using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSubModule : UnitModule
{
    public GameObject m_parentEntity;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void M_Init(ModuleType moduleType, GameObject parentEntity, Transform hardpoint)
    {
        m_moduleType = moduleType;
        m_parentEntity = parentEntity;
        M_AttachTo(hardpoint);
    }

    public void M_Build(Transform attachTransform)
    {
    }

    private void M_AttachTo(Transform attachTo)
    {
        ModuleHardpoint[] hardpoints = GetComponentsInChildren<ModuleHardpoint>();
        foreach (ModuleHardpoint hardpoint in hardpoints)
        {
            if (hardpoint.attachesTo)
            {
                transform.position = attachTo.position + transform.position - hardpoint.transform.position;
                transform.rotation = attachTo.rotation;
                hardpoint.attachesTo = attachTo;
            }
        }
    }
}
