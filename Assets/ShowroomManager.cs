using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class ShowroomManager : MonoBehaviour
{
    public Dropdown m_hullDropDown;
    public UnitBuilder m_unitBuilder;
    public Transform m_spawnPosition;

    private GameObject m_currentVehicle;
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
    }

    void HullSelected(Dropdown change)
    {
        if(m_currentVehicle !=null)
        {
            Destroy(m_currentVehicle);
        }
        MetaUnits.HullVariant variant = (MetaUnits.HullVariant)Enum.Parse(typeof(MetaUnits.HullVariant), change.captionText.text);
        MetaHull hull = new MetaHull();
        hull.m_hullVariant = variant;
        m_currentVehicle = m_unitBuilder.M_BuildHull(hull, m_spawnPosition);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
