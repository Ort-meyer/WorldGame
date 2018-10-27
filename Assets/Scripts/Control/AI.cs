using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{

    private Player m_player;
    // Use this for initialization
    void Start()
    {
        m_player = GetComponent<Player>();
        Invoke("OrderAllUnitsToAttack", 2);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Debuggy method to order all units to attack
    void OrderAllUnitsToAttack()
    {
        List<GameObject> allMyUnits = new List<GameObject>();
        List<GameObject> allEnemyUnits = new List<GameObject>();
        Object[] objs = FindObjectsOfType(typeof(BaseUnit));
        for (int i = 0; i < objs.Length; i++)
        {
            GameObject obj = (objs[i] as BaseUnit).gameObject;
            BaseUnit unit = obj.GetComponent<BaseUnit>();
            if (unit.m_alignment == m_player.m_alignment)
            {
                allMyUnits.Add(obj);
            }
            else
            {
                allEnemyUnits.Add(obj);
            }
        }

        m_player.M_SelectUnits(allMyUnits);
        m_player.M_EngageWithSelectedUnits(allEnemyUnits);
    }
}
