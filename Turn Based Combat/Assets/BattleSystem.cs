using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public List<Unit> turnOrder = new List<Unit>();

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;

        StartCoroutine(BattleSetup());

        int startSpeedBuffer = turnOrder.First().unitSpeed;

        if (turnOrder.First().unitSpeed != 0)
        {
            foreach (var x in turnOrder)
            {
                x.unitSpeed -= startSpeedBuffer;
                //Debug.Log(startSpeedBuffer);
            }
        }

        turnOrder = turnOrder.OrderBy(w => w.unitSpeed).ToList();

        foreach (var x in turnOrder)
        {
            Debug.Log(x.ToString() + " " + x.unitSpeed);
        }


    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            turnOrder.First().unitSpeed += 10;

            Debug.Log("New Order");

            foreach (var x in turnOrder)
            {
                Debug.Log(x.ToString() + " " + x.unitSpeed);
            }
        }
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

        state = BattleState.UPNEXT;
        UpNext();
    }

    void UpNext()
    {
        if (playerUnit.IsDead())
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            if (turnOrder.First().unitType == UnitType.PLAYER)
            {
                state = BattleState.PLAYERTURN;
                PlayerTurn();
            }
            else if (turnOrder.First().unitType == UnitType.ENEMY)
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }
    }

    void PlayerTurn()
    {
        dialogueText.text = "Choose an action: ";
    }

    IEnumerator PlayerAttack()
    {

        playerUnit.NormalAttackSpeed();

        Debug.Log("New Order");

        foreach (var x in turnOrder)
        {
            Debug.Log(x.ToString() + " " + x.unitSpeed);
        }

        enemyUnit.TakeDamage(playerUnit.unitDamage);

        enemyUI.SetHP(enemyUnit.unitCurrentHP);
        dialogueText.text = "The attack is successful!";

        yield return new WaitForSeconds(textDelay);

        state = BattleState.UPNEXT;
        UpNext();
    }

    IEnumerator EnemyTurn()
    {

        enemyUnit.NormalAttackSpeed();

        Debug.Log("New Order");

        foreach (var x in turnOrder)
        {
            Debug.Log(x.ToString() + " " + x.unitSpeed);
        }

        dialogueText.text = enemyUnit.unitName + " attacks!";

        yield return new WaitForSeconds(textDelay);

        playerUnit.TakeDamage(enemyUnit.unitDamage);

        playerUI.SetHP(playerUnit.unitCurrentHP);

        yield return new WaitForSeconds(textDelay);

        state = BattleState.UPNEXT;
        UpNext();
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
