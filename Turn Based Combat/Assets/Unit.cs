using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("Unit Information")]
    public string unitName;
    public int unitLevel;

    public int unitDamage;

    public int unitMaxHP;
    public int unitCurrentHP;

    public void TakeDamage(int damage)
    {
        unitCurrentHP -= damage;
    }

    public bool IsDead()
    {
        if (unitCurrentHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Heal(int healAmount)
    {
        unitCurrentHP += healAmount;

        if (unitCurrentHP> unitMaxHP)
        {
            unitCurrentHP = unitMaxHP;
        }
    }

}
