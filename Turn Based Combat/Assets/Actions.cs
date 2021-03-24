using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Actions : MonoBehaviour
{

    [Header("Action Information")]
    public string actionName;

    public int actionDamage;

    public int actionSpeed;

    public float specialBonus;

    public float actionAccuracy;

    public Text buttonOneName;
    public Text buttonOneCost;
    public int buttonOneDamage;

    public Text buttonTwoName;
    public Text buttonTwoCost;
    public int buttonTwoDamage;

    public Text buttonThreeName;
    public Text buttonThreeCost;
    public int buttonThreeDamage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Small Player Moves

    public void SmallPlayerQuickMove()
    {
        actionSpeed = 10;
        actionDamage = 5;
        buttonOneName.text = "Quick Move";
        buttonOneCost.text = "Time Added: " + actionSpeed;
        buttonOneDamage = actionDamage;
        specialBonus = 1.2f;
        actionAccuracy = 8;
    }

    public void SmallPlayerStandardMove()
    {
        actionSpeed = 25;
        actionDamage = 8;
        buttonTwoName.text = "Standard Move";
        buttonTwoCost.text = "Time Added: " + actionSpeed;
        buttonTwoDamage = actionDamage;
        specialBonus = 1.5f;
        actionAccuracy = 10;
    }

    public void SmallPlayerHeavyMove()
    {
        actionSpeed = 35;
        actionDamage = 11;
        buttonThreeName.text = "Heavy Move";
        buttonThreeCost.text = "Time Added: " + actionSpeed;
        buttonThreeDamage = actionDamage;
        specialBonus = 1.7f;
        actionAccuracy = 6.5f;
    }


    //Medium Player Moves

    public void MediumPlayerQuickMove()
    {
        actionSpeed = 15;
        actionDamage = 6;
        buttonOneName.text = "Quick Move";
        buttonOneCost.text = "Time Added: " + actionSpeed;
        buttonOneDamage = actionDamage;
        specialBonus = 1.4f;
        actionAccuracy = 7.5f;
    }

    public void MediumPlayerStandardMove()
    {
        actionSpeed = 25;
        actionDamage = 8;
        buttonTwoName.text = "Standard Move";
        buttonTwoCost.text = "Time Added: " + actionSpeed;
        buttonTwoDamage = actionDamage;
        specialBonus = 1.5f;
        actionAccuracy = 10;
    }

    public void MediumPlayerHeavyMove()
    {
        actionSpeed = 40;
        actionDamage = 17;
        buttonThreeName.text = "Heavy Move";
        buttonThreeCost.text = "Time Added: " + actionSpeed;
        buttonThreeDamage = actionDamage;
        specialBonus = 1.8f;
        actionAccuracy = 7f;
    }

    //Heavy Player Moves

    public void HeavyPlayerQuickMove()
    {
        actionSpeed = 20;
        actionDamage = 7;
        buttonOneName.text = "Quick Move";
        buttonOneCost.text = "Time Added: " + actionSpeed;
        buttonOneDamage = actionDamage;
        specialBonus = 1.4f;
        actionAccuracy = 7;
    }

    public void HeavyPlayerStandardMove()
    {
        actionSpeed = 25;
        actionDamage = 8;
        buttonTwoName.text = "Standard Move";
        buttonTwoCost.text = "Time Added: " + actionSpeed;
        buttonTwoDamage = actionDamage;
        specialBonus = 1.5f;
        actionAccuracy = 10;
    }

    public void HeavyPlayerHeavyMove()
    {
        actionSpeed = 55;
        actionDamage = 17;
        buttonThreeName.text = "Heavy Move";
        buttonThreeCost.text = "Time Added: " + actionSpeed;
        buttonThreeDamage = actionDamage;
        specialBonus = 2.0f;
        actionAccuracy = 6.5f;
    }

    //Possible Enemy Actions

    public void EnemyQuickMove()
    {
        actionSpeed = 20;
        actionDamage = 6;
        actionAccuracy = 9;
    }

    public void EnemyStandardMove()
    {
        actionSpeed = 30;
        actionDamage = 9;
        actionAccuracy = 8.5f;
    }
}
