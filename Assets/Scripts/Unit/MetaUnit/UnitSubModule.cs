using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class UnitSubModule : UnitModule
{
    UnitModule m_attachedTo;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        m_attachedTo.M_RemoveFromModuleDict(this.GetInstanceID());
    }

    public void M_Init(ModuleType moduleType, ModuleHardpoint parentModule)
    {
        base.M_Init(moduleType);
        M_AttachTo(parentModule);
    }

    public void M_Build(Transform attachTransform)
    {
    }

    private void M_AttachTo(ModuleHardpoint attachTo)
    {
        m_attachedTo = attachTo.m_moduleTopObject.GetComponent<UnitModule>();
        ModuleHardpoint[] hardpoints = GetComponentsInChildren<ModuleHardpoint>();
        foreach (ModuleHardpoint hardpoint in hardpoints)
        {
            if (hardpoint.attachesTo)
            {
                transform.parent = attachTo.m_moduleTopObject.transform;
                transform.position = attachTo.transform.position;
                transform.localPosition -= hardpoint.transform.localPosition;
                transform.rotation = attachTo.transform.rotation;
                hardpoint.attachesTo = attachTo; // Why? Remove?
                attachTo.m_moduleTopObject.GetComponent<UnitModule>().M_AddToModuleDict(this.GetInstanceID(), this);
            }
        }
    }
}
