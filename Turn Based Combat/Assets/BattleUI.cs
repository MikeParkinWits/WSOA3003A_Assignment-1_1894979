using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    [Header("UI References")]
    public Text nameText;
    public Text levelText;
    public Text hpText;

    public void SetUI(Unit unit)
    {
        nameText.text = unit.unitName;
        levelText.text = "Lvl " + unit.unitLevel;
        hpText.text = "Health:" + unit.unitCurrentHP;

        Debug.Log(unit.unitCurrentHP);
    }

    public void SetHP(int healthPoints)
    {
        hpText.text = "Health:" + healthPoints;
    }

}
