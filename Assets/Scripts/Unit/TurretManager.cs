using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{

    public float m_maxRotationSpeed;

    private float m_currentRotationSpeed;
    private Transform m_target;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Move with constant rotation speed for now. Should probably be smooth in the future
        m_currentRotationSpeed = m_maxRotationSpeed;

        // Ugly way but it should work for now
        if(m_target == null)
        {
            return;
        }

        float diffAngle = GetDiffAngle();
        // If we're not looing at the target, turn the turret
        if(Mathf.Abs(diffAngle) > 0)
        {
            float rotateAngle = Mathf.Sign(diffAngle) * m_currentRotationSpeed * Time.deltaTime;

            // If we overshoot, set rotate to diff for perfect rotate
            if (Mathf.Abs(rotateAngle) > Mathf.Abs(diffAngle))
            {
                rotateAngle = diffAngle;    
            }
            transform.Rotate(0, rotateAngle, 0, Space.World); // What happens if the tank tilts? Should be Space.World?
        }
    }

    private float GetDiffAngle()
    {
        // Where the turret is currently facing
        Vector2 currentDirection = new Vector2(transform.forward.x, transform.forward.z).normalized;
        // Where the turret wants to face (towards the target)
        Vector3 targetTurretVector = m_target.position - transform.position;
        Vector2 targetDirection = new Vector2(targetTurretVector.x, targetTurretVector.z).normalized;

        float diffAngle = Vector2.Angle(currentDirection, targetDirection);

        // For some reason, this angle is absolute. Do some algebra magic to get negative angle
        Vector3 cross = Vector3.Cross(transform.forward, targetTurretVector);
        if(cross.y < 0)
        {
            diffAngle *= -1;
        }
        return diffAngle;
    }

    // The turret will continue to aim at this target
    public void M_SetTarget(Transform target)
    {
        m_target = target;
        GetComponentInChildren<BasicCannon>().M_SetTarget(target);
    }

    public void M_ClearTarget()
    {
        m_target = null;
    }
}
