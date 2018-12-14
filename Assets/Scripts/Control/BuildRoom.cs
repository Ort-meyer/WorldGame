using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildRoom : MonoBehaviour
{
    private RaycastHit m_hit;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DoRaycast();
    }

    private void DoRaycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out m_hit);
    }
    private void LeftClick()
    {
        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            ModuleHardpoint hardpointSelected = m_hit.collider.GetComponentInParent<ModuleHardpoint>();
            if (hardpointSelected)
            {

            }
        }
    }
}
