using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    [Header("UI References")]
    public Text[] nameText = new Text[3];
    public Text[] levelText = new Text[3];
    public Text[] hpText = new Text[3];

    public BattleSystem battleSystem;

    public GameObject currentTurnIndicator;

    void Start()
    {
        currentTurnIndicator.SetActive(false);
    }

    public void SetUI(Unit unit, int index)
    {
        nameText[index].text = unit.unitName;
        levelText[index].text = "Lvl " + unit.unitLevel;
        hpText[index].text = "Health:" + unit.unitCurrentHP;

    }

    public void SetHP(int healthPoints, int index)
    {
        hpText[index].text = "Health:" + healthPoints;
    }

    public void SetUnitUI()
    {
        if (battleSystem.playerCharacters.Length == 1)
        {
            battleSystem.characterUnitsUI[0].SetActive(true);
            battleSystem.characterUnitsUI[1].SetActive(false);
            battleSystem.characterUnitsUI[2].SetActive(false);
        }
        else if (battleSystem.playerCharacters.Length == 2)
        {
            battleSystem.characterUnitsUI[0].SetActive(true);
            battleSystem.characterUnitsUI[1].SetActive(true);
            battleSystem.characterUnitsUI[2].SetActive(false);
        }
        else if (battleSystem.playerCharacters.Length == 3)
        {
            battleSystem.characterUnitsUI[0].SetActive(true);
            battleSystem.characterUnitsUI[1].SetActive(true);
            battleSystem.characterUnitsUI[2].SetActive(true);
        }
    }

    public void SetEnemyUI()
    {
        if (battleSystem.enemyCharacters.Length == 1)
        {
            battleSystem.enemyUnitsUI[0].SetActive(true);
            battleSystem.enemyUnitsUI[1].SetActive(false);
            battleSystem.enemyUnitsUI[2].SetActive(false);
        }
        else if (battleSystem.enemyCharacters.Length == 2)
        {
            battleSystem.enemyUnitsUI[0].SetActive(true);
            battleSystem.enemyUnitsUI[1].SetActive(true);
            battleSystem.enemyUnitsUI[2].SetActive(false);
        }
        else if (battleSystem.enemyCharacters.Length == 3)
        {
            battleSystem.enemyUnitsUI[0].SetActive(true);
            battleSystem.enemyUnitsUI[1].SetActive(true);
            battleSystem.enemyUnitsUI[2].SetActive(true);
        }
    }

    public void UIIndicator()
    {

        currentTurnIndicator.SetActive(true);

        if (battleSystem.playerUnit.Length >= 1)
        {
            if (battleSystem.turnOrder.First() == battleSystem.playerUnit[0])
            {
                currentTurnIndicator.transform.position = battleSystem.playerUnit[0].transform.position;
            }
        }

        if (battleSystem.playerUnit.Length >= 2)
        {
            if (battleSystem.turnOrder.First() == battleSystem.playerUnit[1])
            {
                currentTurnIndicator.transform.position = battleSystem.playerUnit[1].transform.position;
            }
        }

        if (battleSystem.playerUnit.Length >= 3)
        {
            if (battleSystem.playerUnit[2] != null && battleSystem.turnOrder.First() == battleSystem.playerUnit[2])
            {
                currentTurnIndicator.transform.position = battleSystem.playerUnit[2].transform.position;
            }
        }

        if (battleSystem.enemyUnit.Length >= 1)
        {
            if (battleSystem.turnOrder.First() == battleSystem.enemyUnit[0])
            {
                currentTurnIndicator.transform.position = battleSystem.enemyUnit[0].transform.position;
            }
        }

        if (battleSystem.enemyUnit.Length >= 2)
        {

            if (battleSystem.turnOrder.First() == battleSystem.enemyUnit[1])
            {
                currentTurnIndicator.transform.position = battleSystem.enemyUnit[1].transform.position;
            }
        }

        if (battleSystem.enemyUnit.Length >= 3)
        {
            if (battleSystem.turnOrder.First() == battleSystem.enemyUnit[2])
            {
                currentTurnIndicator.transform.position = battleSystem.enemyUnit[2].transform.position;
            }
        }
    }

}
