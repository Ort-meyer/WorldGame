using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickingHandler : MonoBehaviour
{

    public static ArrayList m_currentlySelectedUnits = new ArrayList();
    public Texture2D m_selectionTexture;
    public Texture2D m_selectionEdgeTexture;
    public float m_edgeThickness;

    private static Vector3 m_mouseDownPoint;

    private bool m_isDragging = false;

    private RaycastHit m_hit;


    public Dictionary<int, GameObject> m_selectedUnits;


    public GameObject agent;
    // Use this for initialization
    void Start()
    {
        m_selectedUnits = new Dictionary<int, GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        // Do raycast every frame, and let other methods use the results as they want
        DoRaycast();
        RightClick();
        LeftClick();
    }

    // Simply does a raycast and stores the hit data in private member
    private void DoRaycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out m_hit);
    }

    private void RightClick()
    {
        // Right mouse button - move order (really bad idea to have it here)
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (m_hit.transform != null)
            {
                // See if we clicked an enemy
                if (m_hit.transform.gameObject.GetComponent<EnemyEntity>())
                {
                    OrderEngage(new GameObject[] { m_hit.transform.gameObject });
                }
                else if (m_hit.transform != null) // Is this the right way to check if we hit anything?
                {
                    MoveUnits();
                }
            }
        }
    }

    private void OrderEngage(GameObject[] targets)
    {
        foreach (KeyValuePair<int, GameObject> pair in m_selectedUnits)
        {
            if (pair.Value == null)
                continue;
            GameObject obj = pair.Value;
            BasicTank thisUnit = obj.GetComponent<BasicTank>();

            // Engage closes of potential targets
            float min = 10000;
            GameObject target = null;
            foreach(GameObject potentialTarget in targets)
            {
                float distToTarget = (pair.Value.transform.position - potentialTarget.transform.position).magnitude;
                if (distToTarget < min)
                {
                    target = potentialTarget;
                    min = distToTarget;
                }
            }
            if(target)
            {
                thisUnit.M_SetFireTarget(target);
            }
        }
    }

    private void MoveUnits()
    {
        foreach (KeyValuePair<int, GameObject> pair in m_selectedUnits)
        {
            if (pair.Value == null)
                continue;
            GameObject obj = pair.Value;
            // Pretty hard coded for now. Have to be able to order multiple units
            BasicTank thisUnit = obj.GetComponent<BasicTank>();
            thisUnit.M_SetDestination(m_hit.point);
            obj.GetComponent<BasicTank>().M_SetDestination(m_hit.point);
        }
    }

    private void LeftClick()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            m_isDragging = true;
            m_mouseDownPoint = Input.mousePosition;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            m_isDragging = false;
            // Check if selection was just a click (kinda ugly but should be stable)
            // Click (both where we started dragging and where we release are basically the same points)
            if ((Input.mousePosition - m_mouseDownPoint).magnitude < 4) // Value is virtually pixels in screenspace
            {
                // If we're holding shift, we want to select move stuff
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    // Deselect everything if we just click
                    foreach (KeyValuePair<int, GameObject> kvp in m_selectedUnits)
                    {
                        kvp.Value.GetComponent<PlayerControlledEntity>().DeSelect();
                    }
                    m_selectedUnits.Clear();
                }
                // If we hit something, and if that is a player unit, select it 
                if (m_hit.transform != null)
                {
                    // See if we select something new
                    if (m_hit.transform.GetComponent<PlayerControlledEntity>())
                    {
                        SetSelected(m_hit.transform.gameObject, true);
                    }
                }
            }
            // Drag (selection box)
            else
            {
                // Group engage
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    Object[] objs = FindObjectsOfType(typeof(EnemyEntity));
                    List<GameObject> targets = new List<GameObject>();
                    for (int i = 0; i < objs.Length; i++)
                    {
                        GameObject obj = (objs[i] as EnemyEntity).gameObject;
                        if (IsWithinSelectionBounds(obj))
                        {
                            targets.Add(obj);
                        }
                    }
                    OrderEngage(targets.ToArray());
                }
                // Group select
                else
                {
                    Object[] objs = FindObjectsOfType(typeof(PlayerControlledEntity));
                    for (int i = 0; i < objs.Length; i++)
                    {
                        GameObject obj = (objs[i] as PlayerControlledEntity).gameObject;
                        if (IsWithinSelectionBounds(obj))
                        {
                            SetSelected(obj, true);
                        }
                        // If we're holding shift, we want to select move stuff
                        else if (!Input.GetKey(KeyCode.LeftShift))
                        {
                            SetSelected(obj, false);
                        }
                    }
                }
            }
        }
    }

    private void SetSelected(GameObject obj, bool selected)
    {
        int objId = obj.GetInstanceID();
        if (selected)
        {
            obj.GetComponent<PlayerControlledEntity>().Select();
            if (!m_selectedUnits.ContainsKey(objId))
            {
                m_selectedUnits.Add(objId, obj);
            }
        }
        else
        {
            obj.GetComponent<PlayerControlledEntity>().DeSelect();
            if (m_selectedUnits.ContainsKey(objId))
            {
                m_selectedUnits.Remove(objId);
            }
        }

    }

    private void OnGUI()
    {
        if (m_isDragging)
        {
            Rect rect = GetScreenRect(m_mouseDownPoint, Input.mousePosition);
            DrawSelectionBox(rect);
        }
    }

    private Rect GetScreenRect(Vector3 screenPosition1, Vector3 screenPosition2)
    {
        // Move origin from bottom left to top left
        screenPosition1.y = Screen.height - screenPosition1.y;
        screenPosition2.y = Screen.height - screenPosition2.y;
        // Calculate corners
        var topLeft = Vector3.Min(screenPosition1, screenPosition2);
        var bottomRight = Vector3.Max(screenPosition1, screenPosition2);
        // Create Rect
        return Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
    }

    private void DrawSelectionBox(Rect rect)
    {
        // Draw the inner box
        GUI.DrawTexture(rect, m_selectionTexture);
        // Draw the edges
        GUI.DrawTexture(new Rect(rect.xMin, rect.yMin, rect.width, m_edgeThickness), m_selectionEdgeTexture);
        // Left
        GUI.DrawTexture(new Rect(rect.xMin, rect.yMin, m_edgeThickness, rect.height), m_selectionEdgeTexture);
        // Right
        GUI.DrawTexture(new Rect(rect.xMax - m_edgeThickness, rect.yMin, m_edgeThickness, rect.height), m_selectionEdgeTexture);
        // Bottom
        GUI.DrawTexture(new Rect(rect.xMin, rect.yMax - m_edgeThickness, rect.width, m_edgeThickness), m_selectionEdgeTexture);
    }

    private Bounds GetViewportBounds(Vector3 screenPosition1, Vector3 screenPosition2)
    {
        var v1 = Camera.main.ScreenToViewportPoint(screenPosition1);
        var v2 = Camera.main.ScreenToViewportPoint(screenPosition2);
        var min = Vector3.Min(v1, v2);
        var max = Vector3.Max(v1, v2);
        min.z = Camera.main.nearClipPlane;
        max.z = Camera.main.farClipPlane;

        var bounds = new Bounds();
        bounds.SetMinMax(min, max);
        return bounds;
    }

    public bool IsWithinSelectionBounds(GameObject gameObject)
    {
        var viewportBounds = GetViewportBounds(m_mouseDownPoint, Input.mousePosition);
        return viewportBounds.Contains(Camera.main.WorldToViewportPoint(gameObject.transform.position));
    }
}
