using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitBase
{
    LightHull, MediumHull, HeavyHull, TruckHull
};

public class Unit : MonoBehaviour
{
    public UnitBase m_base;

    //List<UnitModule> m_modules = new List<UnitModule>();

    public void Init(UnitBase unitBase)
    {
        m_base = unitBase;
    }
}
