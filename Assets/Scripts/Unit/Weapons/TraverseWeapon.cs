using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraverseWeapon : MonoBehaviour
{

    public float m_maxElevation;
    public float m_minElevation;
    public float m_elevationSpeed;
    private float m_currentElevation = 0;
    private float m_targetElevation = 0;

    public float m_maxTraverse;
    public float m_traverseSpeed;
    private float m_currentTraverse = 0;
    private float m_targetTraverse = 0;

    // Use this for initialization
    protected void Start()
    {

    }

    // Update is called once per frame
    protected void Update()
    {
        // Elevation
        float elevationAngleThisFrame = GetRotationAngle(m_targetElevation, m_currentElevation, m_elevationSpeed);
        if (m_currentElevation + elevationAngleThisFrame > m_maxElevation)
        {
            m_currentElevation = m_maxElevation;
        }
        if (m_currentElevation + elevationAngleThisFrame < m_minElevation)
        {
            m_currentElevation = m_minElevation;
        }
        m_currentElevation += elevationAngleThisFrame;

        float traverseAngleThisFrame = GetRotationAngle(m_targetTraverse, m_currentTraverse, m_traverseSpeed);
        m_currentTraverse += traverseAngleThisFrame;
        if (m_currentTraverse + traverseAngleThisFrame > m_maxTraverse)
        {
            m_currentTraverse = m_maxTraverse;
        }
        if (m_currentTraverse + traverseAngleThisFrame < -m_maxTraverse)
        {
            m_currentTraverse = -m_maxTraverse;
        }


        transform.localRotation = Quaternion.Euler(new Vector3(m_currentElevation, m_currentTraverse));
    }

    public float GetRotationAngle(float targetAngle, float currentAngle, float angleSpeed)
    {
        float rotateAngle = 0;
        float diffAngle = targetAngle - currentAngle;
        if (Mathf.Abs(diffAngle) > 0)
        {
            rotateAngle = Mathf.Sign(diffAngle) * angleSpeed * Time.deltaTime;

            // If we overshoot, set rotate to diff for perfect rotate
            if (Mathf.Abs(rotateAngle) > Mathf.Abs(diffAngle))
            {
                rotateAngle = diffAngle;
            }
        }
        return rotateAngle;
    }


    public void M_SetTargetAngles(float traverse, float elevation)
    {
        m_targetTraverse = traverse;
        m_targetElevation = elevation;
    }


}
