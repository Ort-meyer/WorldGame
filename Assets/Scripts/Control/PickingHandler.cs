using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickingHandler : MonoBehaviour
{
    public GameObject agent;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HandlePick();
    }

    private void HandlePick()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 positionHit = hit.point;
                agent.GetComponent<BasicTank>().M_SetDestination(positionHit);
            }
        }
    }
}
