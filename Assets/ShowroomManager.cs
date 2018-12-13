using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class ShowroomManager : MonoBehaviour
{
    public Dropdown m_dropDown;
    public UnitBuilder m_unitBuilder;
    public Transform m_spawnPosition;
    // Use this for initialization
    void Start()
    {
        List<GameObject> hullPrefabs = new List<GameObject>(m_unitBuilder.m_hullPrefabs);
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
        foreach (GameObject hullPrefab in hullPrefabs)
        {
            options.Add(new Dropdown.OptionData(hullPrefab.name));
        }
        m_dropDown.ClearOptions();
        m_dropDown.AddOptions(options);
        m_dropDown.onValueChanged.AddListener(delegate { OptionSelected(m_dropDown); });
    }

    void OptionSelected(Dropdown change)
    {
        MetaUnits.HullVariant variant = (MetaUnits.HullVariant)Enum.Parse(typeof(MetaUnits.HullVariant), change.captionText.text);
        MetaHull hull = new MetaHull();
        hull.m_hullVariant = variant;
        m_unitBuilder.M_BuildHull(hull, m_spawnPosition);
    }



    // Update is called once per frame
    void Update()
    {

    }
}
