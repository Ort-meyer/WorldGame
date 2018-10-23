using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTargeting : MonoBehaviour
{

    protected List<GameObject> m_targets = new List<GameObject>();
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Sets the target that this unit should fire on
    /// </summary>
    /// <param name="target">Target gameobject</param>
    public virtual void M_SetTargets(List<GameObject> targets)
    {
        m_targets = targets;
    }

    /// <summary>
    /// Clears all targets from this unit
    /// </summary>
    public virtual void M_ClearTargets()
    {
        m_targets.Clear();
    }
}
