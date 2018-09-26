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
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            m_isDragging = true;
            m_mouseDownPoint = Input.mousePosition;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            m_isDragging = false;
            Object[] objs = FindObjectsOfType(typeof(PlayerControlledEntity));
            for (int i = 0; i < objs.Length; i++)
            {
                GameObject obj = (objs[i] as PlayerControlledEntity).gameObject;
                if (IsWithinSelectionBounds(obj))
                {
                    obj.GetComponent<PlayerControlledEntity>().Select();
                    m_selectedUnits[obj.GetInstanceID()] = obj;
                }
                else
                {
                    m_selectedUnits[obj.GetInstanceID()] = null;
                    obj.GetComponent<PlayerControlledEntity>().DeSelect();
                }
            }
        }

        // Right mouse button - move order (really bad idea to have it here)
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                foreach (KeyValuePair<int, GameObject> pair in m_selectedUnits)
                {
                    if (pair.Value == null)
                        continue;
                    GameObject obj = pair.Value;
                    BasicTank derp = obj.GetComponent<BasicTank>();
                    derp.M_SetDestination(hit.point);
                    obj.GetComponent<BasicTank>().M_SetDestination(hit.point);
                }
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
