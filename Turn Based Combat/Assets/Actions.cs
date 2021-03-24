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

    public void SmallPlayerSmallMove()
    {
        actionSpeed = 10;
        actionDamage = 5;
        buttonOneName.text = "Small Punch";
        buttonOneCost.text = "Cost: " + actionSpeed;
        buttonOneDamage = actionDamage;
        specialBonus = 1.2f;
        actionAccuracy = 10;
    }

    public void SmallPlayerMediumMove()
    {
        actionSpeed = 25;
        actionDamage = 8;
        buttonTwoName.text = "Medium Punch";
        buttonTwoCost.text = "Cost: " + actionSpeed;
        buttonTwoDamage = actionDamage;
        specialBonus = 1.5f;
        actionAccuracy = 8;
    }

    public void SmallPlayerHeavyMove()
    {
        actionSpeed = 50;
        actionDamage = 17;
        buttonThreeName.text = "Heavy Punch";
        buttonThreeCost.text = "Cost: " + actionSpeed;
        buttonThreeDamage = actionDamage;
        specialBonus = 1.7f;
        actionAccuracy = 9;
    }


    //Medium Player Moves

    public void MediumPlayerSmallMove()
    {
        actionSpeed = 20;
        actionDamage = 5;
        buttonOneName.text = "Small Punch";
        buttonOneCost.text = "Cost: " + actionSpeed;
        buttonOneDamage = actionDamage;
        specialBonus = 1.4f;
        actionAccuracy = 10;
    }

    public void MediumPlayerMediumMove()
    {
        actionSpeed = 30;
        actionDamage = 10;
        buttonTwoName.text = "Medium Punch";
        buttonTwoCost.text = "Cost: " + actionSpeed;
        buttonTwoDamage = actionDamage;
        specialBonus = 1.6f;
        actionAccuracy = 8.5f;
    }

    public void MediumPlayerHeavyMove()
    {
        actionSpeed = 55;
        actionDamage = 17;
        buttonThreeName.text = "Heavy Punch";
        buttonThreeCost.text = "Cost: " + actionSpeed;
        buttonThreeDamage = actionDamage;
        specialBonus = 1.9f;
        actionAccuracy = 9f;
    }

    //Heavy Player Moves

    public void HeavyPlayerSmallMove()
    {
        actionSpeed = 25;
        actionDamage = 5;
        buttonOneName.text = "Small Punch";
        buttonOneCost.text = "Cost: " + actionSpeed;
        buttonOneDamage = actionDamage;
        specialBonus = 1.6f;
        actionAccuracy = 10;
    }

    public void HeavyPlayerMediumMove()
    {
        actionSpeed = 30;
        actionDamage = 10;
        buttonTwoName.text = "Medium Punch";
        buttonTwoCost.text = "Cost: " + actionSpeed;
        buttonTwoDamage = actionDamage;
        specialBonus = 1.9f;
        actionAccuracy = 7;
    }

    public void HeavyPlayerHeavyMove()
    {
        actionSpeed = 55;
        actionDamage = 22;
        buttonThreeName.text = "Heavy Punch";
        buttonThreeCost.text = "Cost: " + actionSpeed;
        buttonThreeDamage = actionDamage;
        specialBonus = 2.1f;
        actionAccuracy = 8;
    }

    //Possible Enemy Actions

    public void EnemySmallMove()
    {
        actionSpeed = 25;
        actionDamage = 5;
        actionAccuracy = 8;
    }

    public void EnemyMediumMove()
    {
        actionSpeed = 35;
        actionDamage = 11;
        actionAccuracy = 7;
    }
}
