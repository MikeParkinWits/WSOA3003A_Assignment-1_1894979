﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
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

    public string playerType;

    private BattleSystem battleSystem;

    public UnitType unitType;

    private Actions actions;

    public int uniqueNum;

    public int attackPower;

    public int defensePower;

    void Awake()
    {
        battleSystem = GameObject.Find("BattleSystem").GetComponent<BattleSystem>();

        battleSystem.turnOrder.Add(this);

        uniqueNum++;
        //unitSpeed *= UnityEngine.Random.Range(1, 10); //FOR TESTING, TAKE OUT WHEN FINAL
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

        if (unitCurrentHP >= unitMaxHP)
        {
            unitCurrentHP = unitMaxHP;
        }

        int speedChange = battleSystem.turnOrder.ElementAt(1).unitSpeed;

        foreach (var x in battleSystem.turnOrder)
        {
            x.unitSpeed -= speedChange;
            //Debug.Log(x.ToString());

            Debug.Log("YES" + x.unitSpeed);
        }

        battleSystem.turnOrder.First().unitSpeed += (speedChange) + 20;


        battleSystem.turnOrder = battleSystem.turnOrder.OrderBy(w => w.unitSpeed).ToList();
    }

    public void NormalAttackSpeed()
    {
        Debug.Log("Name: " + this.unitName);

        int speedChange = battleSystem.turnOrder.ElementAt(1).unitSpeed;

        foreach (var x in battleSystem.turnOrder)
        {
            x.unitSpeed -= speedChange;
            //Debug.Log(x.ToString());

            Debug.Log("YES" + x.unitSpeed);
        }

        battleSystem.turnOrder.First().unitSpeed += (speedChange) + battleSystem.currentAttackSpeed;


        battleSystem.turnOrder = battleSystem.turnOrder.OrderBy(w => w.unitSpeed).ToList();
    }

    public void EnemyAttackSpeed()
    {
        Debug.Log("Name: " + this.unitName);

        int speedChange = battleSystem.turnOrder.ElementAt(1).unitSpeed;

        foreach (var x in battleSystem.turnOrder)
        {
            x.unitSpeed -= speedChange;
            //Debug.Log(x.ToString());

            Debug.Log("YES" + x.unitSpeed);
        }

        battleSystem.turnOrder.First().unitSpeed += (speedChange) + battleSystem.currentAttackSpeed;


        battleSystem.turnOrder = battleSystem.turnOrder.OrderBy(w => w.unitSpeed).ToList();
    }

}
