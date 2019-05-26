﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DefaultUnit
{
    HeavyTank,
    LongRangeTank,
    MaDeuceCar,
    MediumTank,
    MGCar,
    RocketTruck,
    Stupid,
}

public class UnitSpawner : MonoBehaviour
{
    private SaveLoadHandler m_saveLoadHandler;
    private UnitBuilder m_unitBuilder;
    public DefaultUnit m_thisUnit;
    // Use this for initialization
    void Start()
    {
        GameObject gameUtils = GameObject.Find("GameUtils");
        m_saveLoadHandler = gameUtils.GetComponent<SaveLoadHandler>();
        m_unitBuilder = gameUtils.GetComponent<UnitBuilder>();
        string unitToLoad = m_thisUnit.ToString();
        SavedModule savedUnit = m_saveLoadHandler.M_LoadUnitFromFile(unitToLoad);
        m_unitBuilder.M_BuildUnit(savedUnit, transform); 
    }

    // Update is called once per frame
    void Update()
    {

    }
}
