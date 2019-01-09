using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitBase
{
    Light, Medium, Heavy, Truck
};

public class Unit
{
    UnitBase m_base;

    List<UnitModule> m_modules;

    public Unit(UnitBase unitBase)
    {
        m_base = unitBase;
        m_modules = new List<UnitModule>();
    }
}
