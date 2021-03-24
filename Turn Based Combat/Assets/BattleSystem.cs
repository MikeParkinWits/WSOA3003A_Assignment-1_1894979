using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum BattleState { START, UPNEXT, PLAYERTURN, ENEMYTURN, WON, LOST  }

public class BattleSystem : MonoBehaviour
{
    [Header("UI References")]
    public Text dialogueText;
    public GameObject actionsUI;

    [Header("UI Turn Order")]
    public Text[] turnsUI = new Text[4];

    [Header("Current State")]
    public BattleState state;

    [Header("Instance References")]
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    public BattleUI playerUI;
    public BattleUI enemyUI;

    [Header("Unit UI References")]
    public GameObject[] characterUnitsUI = new GameObject[3];
    public GameObject[] enemyUnitsUI = new GameObject[3];

    [Header("Unit References")]
    public Unit[] playerUnit;
    public Unit[] enemyUnit;
    private int currentPlayerNum = 0;
    private int currentEnemyNum = 0;

    [Header("Code Variables")]
    public float textDelay = 2f;
    public List<Unit> turnOrder = new List<Unit>();

    [Header("Objects to Instantiate")]
    public GameObject[] playerCharacters;
    public GameObject[] enemyCharacters;
    public Transform[] unitSpawnPoints = new Transform[6];
    public Transform[] enemySpawnPoints = new Transform[6];
    public int unitSpawnOffset = 0;
    public int enemySpawnOffset = 0;

    public Actions actions;

    public int currentAttackDamage;
    public int currentAttackSpeed;
    public float currentSpecialBonus;
    public float currentAccuracy;
    public float specialAccuracyBuffer = 0f;
    public float specialMoveMultiplier = 1f;

    public GameObject enemySelectPanel;
    public int enemyAttackSelection;

    public GameObject enemyOneSprite;
    public GameObject enemyOneUISprite;

    public GameObject enemyTwoUISprite;

    public GameObject unitOneUISprite;
    public GameObject unitTwoUISprite;

    public GameObject actionButtonsUI;

    public bool specialInputActive = false;
    public bool specialInputAchieved = false;
    public float specialInputTimer;
    public GameObject specialTimeIndicator;

    public int damageCalculated;
    public float damageCalculatedFloat;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("sdfkjbsdfkj" + playerCharacters.Length);
        state = BattleState.START;

        StartCoroutine(BattleSetup());

        turnOrder = turnOrder.OrderBy(w => w.unitSpeed).ToList();

        int startSpeedBuffer = turnOrder.First().unitSpeed;

        if (turnOrder.First().unitSpeed > 0)
        {
            foreach (var x in turnOrder)
            {
                x.unitSpeed -= startSpeedBuffer;
                //Debug.Log(x.ToString() + " " + x.unitSpeed);
            }
        }

        if (playerCharacters.Length >= 1)
        {
            if (playerUnit[0].unitSpeed == 0)
            {
                currentPlayerNum = 0;
            }

            if (playerCharacters.Length >= 2)
            {
                if (playerUnit[1].unitSpeed == 0)
                {
                    currentPlayerNum = 1;
                }

                if (playerCharacters.Length == 3)
                {
                    
                    if (playerUnit[2] != null && playerUnit[2].unitSpeed == 0)
                    {
                        currentPlayerNum = 2;
                    }
                }
            }
        }



        int count = 0;

        foreach (var x in turnOrder)
        {
            if (turnOrder[count] != null && count < 4)
            {
                turnsUI[count].text = x.unitName.ToString() + " " + x.unitSpeed;
            }

            Debug.Log(x.ToString() + " " + x.unitSpeed);
            count++;
        }




    }

    void UnitBattleSpawnLocation()
    {
        if (playerCharacters.Length == 1)
        {
            unitSpawnOffset = 0;
        }
        else if (playerCharacters.Length == 2)
        {
            unitSpawnOffset = 1;
        }
        else if (playerCharacters.Length == 3)
        {
            unitSpawnOffset = 3;
        }
    }

    void EnemyBattleSpawnLocation()
    {
        if (playerCharacters.Length == 1)
        {
            enemySpawnOffset = 0;
        }
        else if (playerCharacters.Length == 2)
        {
            enemySpawnOffset = 1;
        }
        else if (playerCharacters.Length == 3)
        {
            enemySpawnOffset = 3;
        }
    }

    void Update()
    {

        playerUI.UIIndicator();

        if (specialInputActive)
        {
            if (specialInputTimer >= 0)
            {
                specialInputTimer -= Time.deltaTime;

                specialTimeIndicator.SetActive(true);

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    specialInputAchieved = true;
                }

            }
            else
            {
                specialInputActive = false;
                specialInputTimer = currentSpecialBonus;

                specialTimeIndicator.SetActive(false);
            }
        }

        Debug.Log("RANDO: " + UnityEngine.Random.Range(0, 2));

        int count = 0;

        foreach (var x in turnOrder)
        {
            if (turnOrder[count] != null && count < 4)
            {
                turnsUI[count].text = x.unitName.ToString() + " " + x.unitSpeed;

                if (turnOrder.Count == 3)
                {
                    turnsUI[3].text = " ";
                }

                if (turnOrder.Count == 2)
                {
                    turnsUI[3].text = " ";
                    turnsUI[2].text = " ";
                }

            }

            //Debug.Log(x.ToString() + " " + x.unitSpeed);
            count++;
        }

        //playerUI.UIIndicator();

        //Debug.Log("Current Player: " + currentPlayerNum);

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            /*
            turnOrder.First().unitSpeed += 10;

            Debug.Log("New Order");

            foreach (var x in turnOrder)
            {
                Debug.Log(x.ToString() + " " + x.unitSpeed);
            }
            */


            //Debug.Log("Player Unit 1: " + playerUnit[0].unitSpeed);

        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            /*
            turnOrder.First().unitSpeed += 10;

            Debug.Log("New Order");

            foreach (var x in turnOrder)
            {
                Debug.Log(x.ToString() + " " + x.unitSpeed);
            }
            */

            Debug.Log("Player Unit 2: " + playerUnit[1].unitSpeed);

        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            /*
            turnOrder.First().unitSpeed += 10;

            Debug.Log("New Order");

            foreach (var x in turnOrder)
            {
                Debug.Log(x.ToString() + " " + x.unitSpeed);
            }
            */

            playerUnit[0].unitSpeed -= 10;

            Debug.Log("Player Unit 1: " + playerUnit[0].unitSpeed);

        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            /*
            turnOrder.First().unitSpeed += 10;

            Debug.Log("New Order");

            foreach (var x in turnOrder)
            {
                Debug.Log(x.ToString() + " " + x.unitSpeed);
            }
            */

            playerUnit[1].unitSpeed -= 10;

            Debug.Log("Player Unit 2: " + playerUnit[1].unitSpeed);

        }
    }

    private IEnumerator BattleSetup()
    {

        specialTimeIndicator.SetActive(false);
        specialInputActive = false;
        specialInputTimer = 2f;

        actionsUI.SetActive(false);
        enemySelectPanel.SetActive(false);

        enemyOneSprite.SetActive(true);
        enemyOneUISprite.SetActive(true);

        enemyTwoUISprite.SetActive(true);

        unitOneUISprite.SetActive(true);
        unitTwoUISprite.SetActive(true);

        actionButtonsUI.SetActive(true);

        UnitBattleSpawnLocation();

        playerUI.SetUnitUI();

        GameObject playerGameObject;

        int playerUnitIndex= 0;

        playerUnit = new Unit[playerCharacters.Length];
        enemyUnit = new Unit[enemyCharacters.Length];

        foreach (var x in playerCharacters)
        {
            playerGameObject = Instantiate(x, unitSpawnPoints[unitSpawnOffset + playerUnitIndex]);
            //Debug.Log("Count: " + playerGameObject.GetComponent<Unit>());
            playerUnit[playerUnitIndex] = playerGameObject.GetComponent<Unit>();
            playerUnitIndex++;
            //Debug.Log("Count: " + count);
        }

        //GameObject playerGameObject = Instantiate(playerPrefab, playerBattleStation);

        EnemyBattleSpawnLocation();

        enemyUI.SetEnemyUI();

        GameObject enemyGameObject;

        int enemyUnitIndex = 0;

        foreach (var x in enemyCharacters)
        {
            enemyGameObject = Instantiate(x, enemySpawnPoints[enemySpawnOffset + enemyUnitIndex]);
            enemyUnit[enemyUnitIndex] = enemyGameObject.GetComponent<Unit>();
            enemyUnitIndex++;
        }

        //GameObject enemyGameObject = Instantiate(enemyPrefab, enemyBattleStation);

        dialogueText.text = "A wild " + enemyUnit[0].unitName + " approaches...";

        if (playerCharacters.Length >= 1)
        {
            playerUI.SetUI(playerUnit[0], 0);

            if (playerCharacters.Length >= 2)
            {
                playerUI.SetUI(playerUnit[1], 1);

                if (playerCharacters.Length == 3)
                {
                    playerUI.SetUI(playerUnit[2], 2);
                }
            }
        }

        if (enemyCharacters.Length >= 1)
        {
            enemyUI.SetUI(enemyUnit[0], 0);

            if (enemyCharacters.Length >= 2)
            {
                enemyUI.SetUI(enemyUnit[1], 1);

                if (enemyCharacters.Length == 3)
                {
                    enemyUI.SetUI(enemyUnit[2], 2);
                }
            }
        }

        yield return new WaitForSeconds(textDelay);

        playerUI.UIIndicator();

        state = BattleState.UPNEXT;
        UpNext();
    }

    void UpNext()
    {
        if(playerCharacters.Length == 1)
        {
            if (playerUnit[currentPlayerNum].IsDead())
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
        else if (playerCharacters.Length == 2)
        {
            if (playerUnit[0].IsDead() && playerUnit[1].IsDead())
            {
                state = BattleState.LOST;
                EndBattle();
            }
            else if (!playerUnit[currentPlayerNum].IsDead())
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
            else
            {
                playerUI.UIIndicator();

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
        else if (playerCharacters.Length == 3)
        {
            if (playerUnit[0].IsDead() && playerUnit[1].IsDead() && playerUnit[2].IsDead())
            {
                state = BattleState.LOST;
                EndBattle();
            }
            else if (!playerUnit[currentPlayerNum].IsDead())
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


        //Checking if Won


        if (enemyCharacters.Length == 1)
        {
            if (enemyUnit[0].IsDead())
            {
                state = BattleState.WON;
                EndBattle();
            }
        }
        else if (enemyCharacters.Length == 2)
        {
            if (enemyUnit[0].IsDead() && enemyUnit[1].IsDead())
            {
                state = BattleState.WON;
                EndBattle();
            }
        }
        else if (enemyCharacters.Length == 3)
        {
            if (enemyUnit[0].IsDead() && enemyUnit[1].IsDead() && enemyUnit[2].IsDead())
            {
                state = BattleState.WON;
                EndBattle();
            }
        }

    }

    void PlayerTurn()
    {
        actionButtonsUI.SetActive(true);

        dialogueText.text = "Choose an action: ";

        if (playerUnit[0].unitSpeed == 0)
        {
            currentPlayerNum = 0;
        }
        else if (playerUnit[1].unitSpeed == 0)
        {
            currentPlayerNum = 1;
        }
        else if (playerUnit[2].unitSpeed == 0)
        {
            currentPlayerNum = 2;
        }

        actionsUI.SetActive(false);
        enemySelectPanel.SetActive(false);
    }

    IEnumerator PlayerAttack()
    {
        actionsUI.SetActive(false);
        enemySelectPanel.SetActive(false);

        actionButtonsUI.SetActive(false);

        playerUnit[currentPlayerNum].NormalAttackSpeed();

        Debug.Log("New Order");

        int count = 0;

        foreach (var x in turnOrder)
        {

            if (turnOrder[count] != null && count < 4)
            {
                turnsUI[count].text = x.unitName.ToString() + " " + x.unitSpeed;
            }

            Debug.Log(x.ToString() + " " + x.unitSpeed);
        }

        specialInputTimer = currentSpecialBonus;

        yield return new WaitForSeconds(currentSpecialBonus * UnityEngine.Random.Range(1f, 2f));

        specialInputActive = true;

        yield return new WaitForSeconds(currentSpecialBonus + 0.01f);

        if (specialInputAchieved)
        {
            Debug.Log("Special YAY");
            specialAccuracyBuffer = 0.9f;
            specialMoveMultiplier = 1.5f;
        }
        else
        {
            Debug.Log("Special NAY");
            specialAccuracyBuffer = 1.1f;
            specialMoveMultiplier = 1f;
        }

        specialInputAchieved = false;

        damageCalculated = (currentAttackDamage + playerUnit[currentPlayerNum].attackPower + UnityEngine.Random.Range(0,10)) - enemyUnit[0].defensePower;
        damageCalculatedFloat = damageCalculated * specialMoveMultiplier;
        damageCalculated = (int) damageCalculatedFloat;

        Debug.Log("DAMAGE AMOUNT: " + damageCalculated);

        if (UnityEngine.Random.Range(0f, 10f/specialAccuracyBuffer) <= currentAccuracy)
        {
            enemyUnit[0].TakeDamage(damageCalculated);
            dialogueText.text = "The attack is successful!";
        }
        else
        {
            dialogueText.text = "The attack was not successful!";
        }



        enemyUI.SetHP(enemyUnit[0].unitCurrentHP, 0);

        yield return new WaitForSeconds(textDelay);

        playerUI.UIIndicator();

        state = BattleState.UPNEXT;
        UpNext();
    }

    IEnumerator EnemyTurn()
    {

        if (enemyUnit[0].unitSpeed == 0)
        {
            currentEnemyNum = 0;
        }
        else if (enemyUnit[1].unitSpeed == 0)
        {
            currentEnemyNum = 1;
        }
        else if (enemyUnit[2].unitSpeed == 0)
        {
            currentEnemyNum = 2;
        }

        int randomNum = UnityEngine.Random.Range(0, 2);

        if (randomNum == 1)
        {
            actions.EnemySmallMove();

            currentAttackDamage = actions.actionDamage;
            currentAttackSpeed = actions.actionSpeed;
            currentAccuracy = actions.actionAccuracy;
        }
        else if (randomNum == 2)
        {
            actions.EnemyMediumMove();

            currentAttackDamage = actions.actionDamage;
            currentAttackSpeed = actions.actionSpeed;
            currentAccuracy = actions.actionAccuracy;
        }

        enemyUnit[currentEnemyNum].NormalAttackSpeed();

        Debug.Log("New Order");

        int count = 0;

        foreach (var x in turnOrder)
        {
            if (turnOrder[count] != null && count < 4)
            {
                turnsUI[count].text = x.unitName.ToString() + " " + x.unitSpeed;
            }

            Debug.Log(x.ToString() + " " + x.unitSpeed);
        }

        dialogueText.text = enemyUnit[currentEnemyNum].unitName + " attacks!";

        yield return new WaitForSeconds(textDelay);

        playerUI.UIIndicator();

        int num = 0;

        if (playerUnit.Length >= 1)
        {
            num = 0;

            if (playerUnit.Length >= 2)
            {
                if (!playerUnit[0].IsDead())
                {
                    num = UnityEngine.Random.Range(0, playerUnit.Length);
                }
                else
                {
                    num = 1;
                }

                if (playerUnit.Length == 3)
                {
                    if (!playerUnit[0].IsDead() && !playerUnit[1].IsDead() && !playerUnit[3].IsDead())
                    {
                        int[] numbers = new int[3];
                        numbers[0] = 0;
                        numbers[1] = 1;
                        numbers[2] = 2;
                        int randomIndex = UnityEngine.Random.Range(0, numbers.Length);

                        num = numbers[randomIndex];
                    }
                    else if (playerUnit[0].IsDead() && !playerUnit[1].IsDead() && !playerUnit[3].IsDead())
                    {
                        int[] numbers = new int[2];
                        numbers[0] = 1;
                        numbers[1] = 2;
                        int randomIndex = UnityEngine.Random.Range(0, numbers.Length);

                        num = numbers[randomIndex];
                    }
                    else if (!playerUnit[0].IsDead() && playerUnit[1].IsDead() && !playerUnit[3].IsDead())
                    {
                        int[] numbers = new int[2];
                        numbers[0] = 0;
                        numbers[1] = 2;
                        int randomIndex = UnityEngine.Random.Range(0, numbers.Length);

                        num = numbers[randomIndex];
                    }
                    else if (!playerUnit[0].IsDead() && !playerUnit[1].IsDead() && playerUnit[3].IsDead())
                    {
                        int[] numbers = new int[2];
                        numbers[0] = 0;
                        numbers[1] = 1;
                        int randomIndex = UnityEngine.Random.Range(0, numbers.Length);

                        num = numbers[randomIndex];
                    }
                    else if (playerUnit[0].IsDead() && playerUnit[1].IsDead() && !playerUnit[3].IsDead())
                    {
                        int[] numbers = new int[1];
                        numbers[0] = 0;
                        int randomIndex = UnityEngine.Random.Range(0, numbers.Length);

                        num = numbers[randomIndex];
                    }
                    else if (!playerUnit[0].IsDead() && playerUnit[1].IsDead() && !playerUnit[3].IsDead())
                    {
                        int[] numbers = new int[1];
                        numbers[0] = 1;
                        int randomIndex = UnityEngine.Random.Range(0, numbers.Length);

                        num = numbers[randomIndex];
                    }
                    else if (!playerUnit[0].IsDead() && !playerUnit[1].IsDead() && playerUnit[3].IsDead())
                    {
                        int[] numbers = new int[1];
                        numbers[0] = 2;
                        int randomIndex = UnityEngine.Random.Range(0, numbers.Length);

                        num = numbers[randomIndex];
                    }
                }
            }
        }






        if (UnityEngine.Random.Range(0f, 1f) <= 0.05f)
        {
            Debug.Log("Special YAY");
            specialAccuracyBuffer = 0.9f;
            specialMoveMultiplier = 1.5f;
            Debug.Log("1235");

        }
        else
        {
            Debug.Log("Special NAY");
            specialAccuracyBuffer = 1f;
            specialMoveMultiplier = 1f;
            Debug.Log("1235");
        }

        specialInputAchieved = false;

        damageCalculated = (currentAttackDamage + enemyUnit[currentEnemyNum].attackPower + UnityEngine.Random.Range(0, 10)) - playerUnit[num].defensePower;
        damageCalculatedFloat = damageCalculated * specialMoveMultiplier;
        damageCalculated = (int)damageCalculatedFloat;

        Debug.Log("DAMAGE AMOUNT ENEMY: " + damageCalculated);

        if (UnityEngine.Random.Range(0f, 10f / specialAccuracyBuffer) <= currentAccuracy)
        {
            playerUnit[num].TakeDamage(damageCalculated);
            dialogueText.text = "The attack is successful!";
        }
        else
        {
            dialogueText.text = "The attack was not successful!";
        }








        playerUI.SetHP(playerUnit[num].unitCurrentHP, num);

        yield return new WaitForSeconds(textDelay);

        if (PlayerDeathChecker(num))
        {
            dialogueText.text = "Player knocked out";

            turnOrder.Remove(playerUnit[num]);

            yield return new WaitForSeconds(textDelay);
        }

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

        playerUnit[currentPlayerNum].Heal(10);

        playerUI.SetHP(playerUnit[currentPlayerNum].unitCurrentHP, currentPlayerNum);
        dialogueText.text = "You feel renewed strength!";

        yield return new WaitForSeconds(textDelay);

        state = BattleState.UPNEXT;
        UpNext();
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        else
        {

            if (playerUnit[currentPlayerNum].playerType == "Small")
            {
                actions.SmallPlayerSmallMove();
                actions.SmallPlayerMediumMove();
                actions.SmallPlayerHeavyMove();
            }

            if (playerUnit[currentPlayerNum].playerType == "Medium")
            {
                actions.MediumPlayerSmallMove();
                actions.MediumPlayerMediumMove();
                actions.MediumPlayerHeavyMove();
            }

            if (playerUnit[currentPlayerNum].playerType == "Heavy")
            {
                actions.HeavyPlayerSmallMove();
                actions.HeavyPlayerMediumMove();
                actions.HeavyPlayerHeavyMove();
            }

            actionsUI.SetActive(true);
            //StartCoroutine(PlayerAttack());
        }
    }

    public void onActionSelection()
    {



        if (playerUnit[currentPlayerNum].playerType == "Small")
        {
            if (EventSystem.current.currentSelectedGameObject.name == "Move 1")
            {
                actions.SmallPlayerSmallMove();
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "Move 2")
            {
                actions.SmallPlayerMediumMove();
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "Move 3")
            {
                actions.SmallPlayerHeavyMove();
            }
        }

        if (playerUnit[currentPlayerNum].playerType == "Medium")
        {
            if (EventSystem.current.currentSelectedGameObject.name == "Move 1")
            {
                actions.MediumPlayerSmallMove();
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "Move 2")
            {
                actions.MediumPlayerMediumMove();
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "Move 3")
            {
                actions.MediumPlayerHeavyMove();
            }
        }

        if (playerUnit[currentPlayerNum].playerType == "Heavy")
        {
            if (EventSystem.current.currentSelectedGameObject.name == "Move 1")
            {
                actions.HeavyPlayerSmallMove();
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "Move 2")
            {
                actions.HeavyPlayerMediumMove();
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "Move 3")
            {
                actions.HeavyPlayerHeavyMove();
            }
        }

        currentAttackDamage = actions.actionDamage;
        currentAttackSpeed = actions.actionSpeed;
        currentSpecialBonus = actions.specialBonus;
        currentAccuracy = actions.actionAccuracy;

        //1234

        if (enemyCharacters.Length == 1)
        {
            currentAttackDamage = actions.actionDamage;
            currentAttackSpeed = actions.actionSpeed;
            currentSpecialBonus = actions.specialBonus;
            currentAccuracy = actions.actionAccuracy;

            StartCoroutine(PlayerAttack());
        }
        else if (enemyCharacters.Length == 2 && (!enemyUnit[1].IsDead() && !enemyUnit[0].IsDead()))
        {
            currentAttackDamage = actions.actionDamage;
            currentAttackSpeed = actions.actionSpeed;
            currentSpecialBonus = actions.specialBonus;
            currentAccuracy = actions.actionAccuracy;

            enemySelectPanel.SetActive(true);
        }
        else if (enemyCharacters.Length == 2 && (!enemyUnit[1].IsDead() || !enemyUnit[0].IsDead()))
        {
            if (enemyUnit[1].IsDead())
            {
                currentAttackDamage = actions.actionDamage;
                currentAttackSpeed = actions.actionSpeed;
                currentSpecialBonus = actions.specialBonus;
                currentAccuracy = actions.actionAccuracy;

                StartCoroutine(PlayerAttackEnemyVariations(0));
            }
            else if (enemyUnit[0].IsDead())
            {
                currentAttackDamage = actions.actionDamage;
                currentAttackSpeed = actions.actionSpeed;
                currentSpecialBonus = actions.specialBonus;
                currentAccuracy = actions.actionAccuracy;

                StartCoroutine(PlayerAttackEnemyVariations(1));
            }
        }

        //Debug.Log("DAMAGE: " + currentAttackDamage + " SPEED" + currentAttackSpeed);

    }



    IEnumerator PlayerAttackEnemyVariations(int enemyAlive)
    {
        actionsUI.SetActive(false);
        enemySelectPanel.SetActive(false);
        actionButtonsUI.SetActive(false);

        playerUnit[currentPlayerNum].NormalAttackSpeed();

        Debug.Log("New Order");

        int count = 0;

        foreach (var x in turnOrder)
        {

            if (turnOrder[count] != null && count < 4)
            {
                turnsUI[count].text = x.unitName.ToString() + " " + x.unitSpeed;
            }

            Debug.Log(x.ToString() + " " + x.unitSpeed);
        }

        specialInputTimer = currentSpecialBonus;

        yield return new WaitForSeconds(currentSpecialBonus * UnityEngine.Random.Range(1f, 2f));

        specialInputActive = true;

        yield return new WaitForSeconds(currentSpecialBonus + 0.01f);

        if (specialInputAchieved)
        {
            Debug.Log("Special YAY");
            specialAccuracyBuffer = 0.9f;
            specialMoveMultiplier = 1.5f;
            Debug.Log("1235");

        }
        else
        {
            Debug.Log("Special NAY");
            specialAccuracyBuffer = 1.1f;
            specialMoveMultiplier = 1f;
            Debug.Log("1235");
        }

        specialInputAchieved = false;

        damageCalculated = (currentAttackDamage + playerUnit[currentPlayerNum].attackPower + UnityEngine.Random.Range(0, 10)) - enemyUnit[enemyAlive].defensePower;
        damageCalculatedFloat = damageCalculated * specialMoveMultiplier;
        damageCalculated = (int)damageCalculatedFloat;

        Debug.Log("DAMAGE AMOUNT: " + damageCalculated);

        if (UnityEngine.Random.Range(0f, 10f / specialAccuracyBuffer) <= currentAccuracy)
        {
            enemyUnit[enemyAlive].TakeDamage(damageCalculated);
            dialogueText.text = "The attack is successful!";
        }
        else
        {
            dialogueText.text = "The attack was not successful!";
        }

        enemyUI.SetHP(enemyUnit[enemyAlive].unitCurrentHP, enemyAlive);

        yield return new WaitForSeconds(textDelay);

        playerUI.UIIndicator();

        state = BattleState.UPNEXT;
        UpNext();
    }



    public void OnEnemySelect(int selection)
    {
        enemyAttackSelection = selection;

        StartCoroutine(PlayerAttackEnemySelect());

        actionButtonsUI.SetActive(false);
    }

    private bool DeathChecker(int killed)
    {

        Debug.Log("KILLED: " + killed);

        if (enemyUnit[killed].IsDead())
        {

            if (enemyUnit.Length == 2)
            {
                enemySpawnPoints[killed + 1].gameObject.SetActive(false);
            }
            else if (enemyUnit.Length == 1)
            {
                enemySpawnPoints[killed].gameObject.SetActive(false);
            }
            else if (enemyUnit.Length == 3)
            {
                enemySpawnPoints[killed + 2].gameObject.SetActive(false);
            }

            if (killed == 0)
            {
                enemyOneSprite.SetActive(false);
                enemyOneUISprite.SetActive(false);
            }

            if (killed == 1)
            {
                enemyOneSprite.SetActive(false);
                enemyTwoUISprite.SetActive(false);
            }

            return true;
        }
        else
        {
            return false;
        }

    }

    private bool PlayerDeathChecker(int killed)
    {

        Debug.Log("KILLED: " + killed);

        if (playerUnit[killed].IsDead())
        {

            if (playerUnit.Length == 2)
            {
                unitSpawnPoints[killed + 1].gameObject.SetActive(false);
            }
            else if (playerUnit.Length == 1)
            {
                unitSpawnPoints[killed].gameObject.SetActive(false);
            }
            else if (playerUnit.Length == 3)
            {
                unitSpawnPoints[killed + 2].gameObject.SetActive(false);
            }

            if (killed == 0)
            {
                unitOneUISprite.SetActive(false);
            }

            if (killed == 1)
            {
                unitTwoUISprite.SetActive(false);
            }
            return true;
        }
        else
        {
            return false;
        }

    }

    IEnumerator PlayerAttackEnemySelect()
    {

        actionsUI.SetActive(false);
        enemySelectPanel.SetActive(false);

        playerUnit[currentPlayerNum].NormalAttackSpeed();

        Debug.Log("New Order");

        int count = 0;

        foreach (var x in turnOrder)
        {

            if (turnOrder[count] != null && count < 4)
            {
                turnsUI[count].text = x.unitName.ToString() + " " + x.unitSpeed;
            }

            Debug.Log(x.ToString() + " " + x.unitSpeed);
        }

        Debug.Log("DAMAGE: " + currentAttackDamage);

                specialInputTimer = currentSpecialBonus;

        yield return new WaitForSeconds(currentSpecialBonus * UnityEngine.Random.Range(1f, 2f));

        specialInputActive = true;

        yield return new WaitForSeconds(currentSpecialBonus + 0.01f);



        if (specialInputAchieved)
        {
            Debug.Log("Special YAY");
            specialAccuracyBuffer = 0.9f;
            specialMoveMultiplier = 1.5f;
            Debug.Log("1235");

        }
        else
        {
            Debug.Log("Special NAY");
            specialAccuracyBuffer = 1.1f;
            specialMoveMultiplier = 1f;
            Debug.Log("1235");
        }

        specialInputAchieved = false;

        damageCalculated = (currentAttackDamage + playerUnit[currentPlayerNum].attackPower + UnityEngine.Random.Range(0, 10)) - enemyUnit[enemyAttackSelection].defensePower;
        damageCalculatedFloat = damageCalculated * specialMoveMultiplier;
        damageCalculated = (int)damageCalculatedFloat;

        Debug.Log("DAMAGE AMOUNT: " + damageCalculated);

        if (UnityEngine.Random.Range(0f, 10f / specialAccuracyBuffer) <= currentAccuracy)
        {
            enemyUnit[enemyAttackSelection].TakeDamage(damageCalculated);
            dialogueText.text = "The attack is successful!";
        }
        else
        {
            dialogueText.text = "The attack was not successful!";
        }

        enemyUI.SetHP(enemyUnit[enemyAttackSelection].unitCurrentHP, enemyAttackSelection);

        yield return new WaitForSeconds(textDelay);

        playerUI.UIIndicator();

        if (DeathChecker(enemyAttackSelection))
        {
            dialogueText.text = "Enemy knocked out";

            turnOrder.Remove(enemyUnit[enemyAttackSelection]);

            yield return new WaitForSeconds(textDelay);
        }

        state = BattleState.UPNEXT;
        UpNext();
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        else
        {
            actionButtonsUI.SetActive(false);
            StartCoroutine(PlayerHeal());
        }
    }

    public void OnForfeitButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        else
        {
            StartCoroutine(PlayerForfeit());
        }
    }

    public void OnBackButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        else
        {
            actionsUI.SetActive(false);
        }
    }

    IEnumerator PlayerForfeit()
    {

        dialogueText.text = "You have given up all hope and forfeit...";

        yield return new WaitForSeconds(textDelay);

        state = BattleState.LOST;
        EndBattle();
    }
}
