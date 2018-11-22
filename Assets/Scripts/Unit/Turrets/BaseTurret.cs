using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTurret : MonoBehaviour
{
    protected Transform m_target = null;

    // Use this for initialization
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(m_target == null)
        {
            return;
        }
        RotateTurret();
    }

    private void RotateTurret()
    {
        //m_diffAngle = Helpers.GetDiffAngle2D(transform.forward, m_target.position - transform.position);
        //// If we're not looing at the target, turn the turret
        //if (Mathf.Abs(m_diffAngle) > 0)
        //{
        //    float rotateAngle = Mathf.Sign(m_diffAngle) * m_currentRotationSpeed * Time.deltaTime;

        //    // If we overshoot, set rotate to diff for perfect rotate
        //    if (Mathf.Abs(rotateAngle) > Mathf.Abs(m_diffAngle))
        //    {
        //        rotateAngle = m_diffAngle;
        //    }
        //    transform.Rotate(0, rotateAngle, 0, Space.World); // What happens if the tank tilts? Should be Space.World?
        //}
    }

    virtual public void M_SetTarget(Transform target)
    {
        m_target = target;
    }

    virtual public void M_ClearTarget()
    {
        m_target = null;
    }
}

