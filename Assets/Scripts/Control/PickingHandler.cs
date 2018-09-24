using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickingHandler : MonoBehaviour
{

    public static ArrayList m_currentlySelectedUnits = new ArrayList();
    public GUIStyle m_mouseDragSkin;

    private static Vector3 mouseDownPoint;
    private static Vector3 mouseUpPoint;
    private static Vector3 currentMousePoint;

    private bool isDragging = false;

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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            currentMousePoint = hit.point;
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                isDragging = true;
                mouseDownPoint = hit.point;
                {
                    agent.GetComponent<BasicTank>().M_SetDestination(currentMousePoint);
                }
            }

            if(Input.GetKeyUp(KeyCode.Mouse0))
            {
                isDragging = false;
                mouseUpPoint = Input.mousePosition;
            }
        } // end world hit
        else // Didn't hit world, count as release (this should probably be handled somehow
        {
            isDragging = false;
        }
    }

    private void OnGUI()
    {
        if (isDragging)
        {
            float boxWidth = Camera.main.WorldToScreenPoint(mouseDownPoint).x - Camera.main.WorldToScreenPoint(currentMousePoint).x;
            float boxHeight = Camera.main.WorldToScreenPoint(mouseDownPoint).y - Camera.main.WorldToScreenPoint(currentMousePoint).y;

            float boxLeft = Input.mousePosition.x;
            float boxTop = (Screen.height - Input.mousePosition.y) - boxHeight;

            // Box width, height, top left corner
            Vector2 boxPosition = new Vector2(boxLeft, boxTop);
            Vector2 size = new Vector2(boxWidth, boxHeight);
            GUI.Box(new Rect(boxPosition, size), "", m_mouseDragSkin);
        }
    }
}
