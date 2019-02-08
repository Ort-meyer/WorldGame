using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTurret : UnitSubModule
{
    List<UnitModule> m_submodules = new List<UnitModule>();

    public virtual new void M_Build(Transform attachTransform)
    {
        base.M_Build(attachTransform);
    }
}
