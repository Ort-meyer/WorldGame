using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTurret : UnitModule
{
    List<UnitModule> m_submodules;

    public UnitTurret(ModuleType moduleType) : base(moduleType)
    {
        m_submodules = new List<UnitModule>();
    }

    public virtual new void M_Build(Transform attachTransform)
    {
        base.M_Build(attachTransform);
        foreach(UnitModule module in m_submodules)
        {
            module.M_Build(FindAttachTransform());
        }
    }

    private Transform FindAttachTransform()
    {
        return m_entity.transform; // Temporary to get it to compile
    }
}
