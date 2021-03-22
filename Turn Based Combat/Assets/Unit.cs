using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum UnitType { PLAYER, ENEMY }

public class Unit : MonoBehaviour
{
    [Header("Unit Information")]
    public string unitName;
    public int unitLevel;

    public int unitDamage;

    public int unitMaxHP;
    public int unitCurrentHP;

    public int unitSpeed;

    private BattleSystem battleSystem;

    public UnitType unitType;

    void Awake()
    {
        battleSystem = GameObject.Find("BattleSystem").GetComponent<BattleSystem>();

        battleSystem.turnOrder.Add(this);
    }

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

    public void NormalAttackSpeed()
    {
        Debug.Log("Name: " + this.unitName);

        int speedChange = battleSystem.turnOrder.ElementAt(1).unitSpeed;

        foreach (var x in battleSystem.turnOrder)
        {
            x.unitSpeed -= speedChange;
            //Debug.Log(x.ToString());
        }

        battleSystem.turnOrder.First().unitSpeed += (speedChange) + 10;


        battleSystem.turnOrder = battleSystem.turnOrder.OrderBy(w => w.unitSpeed).ToList();
    }

}
