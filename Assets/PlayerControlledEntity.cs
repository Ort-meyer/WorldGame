using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlledEntity : MonoBehaviour
{
    public bool m_onScreen;
    public GameObject m_selectionHighlightPrefab;
    private GameObject m_projector;
    public float m_selectedHoverDistance;
    private bool m_selected = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Select()
    {
        // If we weren't arleady selected, become selected
        if (!m_selected)
        {
            m_projector = Instantiate(m_selectionHighlightPrefab, transform.position + new Vector3(0, m_selectedHoverDistance, 0), Quaternion.Euler(90, 0, 0), transform);
            m_selected = true;
        }

    }

    public void DeSelect()
    {
        m_selected = false;
        Destroy(m_projector);
    }
}
