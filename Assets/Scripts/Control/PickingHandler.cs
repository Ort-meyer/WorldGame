using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickingHandler : MonoBehaviour
{

    public static ArrayList m_currentlySelectedUnits = new ArrayList();
    public Texture2D m_selectionTexture;
    public Texture2D m_selectionEdgeTexture;
    public float m_edgeThickness;

    private static Vector3 mouseDownPoint;
    private static Vector3 mouseUpPoint;

    private bool isDragging = false;

    public GameObject agent;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            isDragging = true;
            mouseDownPoint = Input.mousePosition;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            isDragging = false;
        }

    }

    private void OnGUI()
    {
        if (isDragging)
        {
            Rect rect = GetScreenRect(mouseDownPoint, Input.mousePosition);
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
}
