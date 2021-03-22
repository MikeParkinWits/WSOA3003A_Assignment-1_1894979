using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, UPNEXT, PLAYERTURN, ENEMYTURN, WON, LOST  }

public class BattleSystem : MonoBehaviour
{
    [Header("UI References")]
    public Text dialogueText;

    [Header("Current State")]
    public BattleState state;

    [Header("Instance References")]
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    public BattleUI playerUI;
    public BattleUI enemyUI;

    [Header("Unit References")]
    private Unit playerUnit;
    private Unit enemyUnit;

    [Header("Code Variables")]
    public float textDelay = 2f;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;

        StartCoroutine(BattleSetup());
    }

    private IEnumerator BattleSetup()
    {
        GameObject playerGameObject = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGameObject.GetComponent<Unit>();

        GameObject enemyGameObject = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGameObject.GetComponent<Unit>();

        dialogueText.text = "A wild " + enemyUnit.unitName + " approaches...";

        playerUI.SetUI(playerUnit);
        enemyUI.SetUI(enemyUnit);

        yield return new WaitForSeconds(textDelay);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        dialogueText.text = "Choose an action: ";
    }

    IEnumerator PlayerAttack()
    {

        enemyUnit.TakeDamage(playerUnit.unitDamage);

        enemyUI.SetHP(enemyUnit.unitCurrentHP);
        dialogueText.text = "The attack is successful!";

        yield return new WaitForSeconds(textDelay);

        if (enemyUnit.IsDead())
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.unitName + " attacks!";

        yield return new WaitForSeconds(textDelay);

        playerUnit.TakeDamage(enemyUnit.unitDamage);

        playerUI.SetHP(playerUnit.unitCurrentHP);

        yield return new WaitForSeconds(textDelay);

        if (playerUnit.IsDead())
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    private void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "YOU HAVE WON!";
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "YOU WERE DEFEATED...";
        }
    }

    IEnumerator PlayerHeal()
    {
        playerUnit.Heal(5);

        playerUI.SetHP(playerUnit.unitCurrentHP);
        dialogueText.text = "You feel renewed strength!";

        yield return new WaitForSeconds(textDelay);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        else
        {
            StartCoroutine(PlayerAttack());
        }
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        else
        {
            StartCoroutine(PlayerHeal());
        }
    }
}
