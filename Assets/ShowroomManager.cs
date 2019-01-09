using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using MetaUnits;
public class ShowroomManager : MonoBehaviour
{
    public Dropdown m_hullDropDown;
    public Dropdown m_moduleDropdown;
    public UnitBuilder m_unitBuilder;
    public Transform m_spawnPosition;

    private GameObject m_currentVehicle;
    private ModuleHardpoint m_currentHardpoint;
    // Use this for initialization
    void Start()
    {
        List<GameObject> hullPrefabs = new List<GameObject>(m_unitBuilder.m_hullPrefabs);
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
        foreach (GameObject hullPrefab in hullPrefabs)
        {
            options.Add(new Dropdown.OptionData(hullPrefab.name));
        }
        m_hullDropDown.ClearOptions();
        m_hullDropDown.AddOptions(options);
        m_hullDropDown.onValueChanged.AddListener(delegate { HullSelected(m_hullDropDown); });

        m_moduleDropdown.ClearOptions();
        m_moduleDropdown.onValueChanged.AddListener(delegate { ModuleSelected(m_moduleDropdown); });
    }

    void HullSelected(Dropdown change)
    {
        if (m_currentVehicle != null)
        {
            Destroy(m_currentVehicle);
        }
        HullVariant variant = (HullVariant)Enum.Parse(typeof(HullVariant), change.captionText.text);
        MetaHull hull = new MetaHull();
        hull.m_hullVariant = variant;
        m_currentVehicle = m_unitBuilder.M_BuildHull(hull, m_spawnPosition);
    }

    void ModuleSelected(Dropdown change)
    {
        if (m_currentVehicle != null)
        {
            switch(m_currentHardpoint.m_hardPointType)
            {
                case ModuleHardpoint.HardPointType.Turret:
                    MetaTurret newTurretData = new MetaTurret();
                    newTurretData.m_turretVariant = (TurretVariant)Enum.Parse(typeof(TurretVariant), change.captionText.text);
                    m_unitBuilder.M_BuildTurret(newTurretData, m_currentHardpoint.transform);
                    break;
                case ModuleHardpoint.HardPointType.Weapon:

                    break;
            }
        }
    }

    public void M_HardpointSelected(ModuleHardpoint hardPoint)
    {
        m_currentHardpoint = hardPoint;
        m_moduleDropdown.ClearOptions();
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
        if (hardPoint.m_hardPointType == ModuleHardpoint.HardPointType.Turret)
        {
            foreach(TurretVariant variant in hardPoint.m_availableTurrets)
            {
                options.Add(new Dropdown.OptionData(variant.ToString()));
            }
        }
        else if (hardPoint.m_hardPointType == ModuleHardpoint.HardPointType.Turret)
        {
            foreach (WeaponVariant variant in hardPoint.m_availableWeapons)
            {
                options.Add(new Dropdown.OptionData(variant.ToString()));
            }
        }
        m_moduleDropdown.AddOptions(options);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
