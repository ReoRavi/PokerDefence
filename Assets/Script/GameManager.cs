using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum eGameState { Play, Wait, End }

public class GameManager : MonoBehaviour {

#region Enemy
    // Enemy Prefabs
    private GameObject[] enemyPrefabs;

    // Enemy Respawn Position
    private Transform respawnPosition;

    // Current Stage Enemy Object List
    private List<GameObject> enemyList;
    // Current EnemyCount;
    private int enemyCurrentCount;
    // Enemy Max Count
    private int enemyMaxCount;
    // Enemy All Created
    private bool enemyAllCreated;
    #endregion
    // Current Stage Count
    private int stageCount;

    // Stage Max Time
    private float stageTime;
    [SerializeField]
    // Stage Time
    private float time;
    // Game State
    private eGameState state;

    // Money
    private int moneyCount;
    // Life
    private int lifeCount;

    // CardBoard
    private CardBoard cardBoard;

    // Upgrade Count
    public int upgradeCount;
    // Upgrade Cost
    private int upgradeCost;

    #region SystemUI
    // Timer
    private Text timerText;
    // Money
    private Text moneyText;
    // life 
    private Text lifeText;
    // Upgrade
    private Text upgradeText;
    #endregion

    [SerializeField]
    public Turret currentTurret;

    public GameObject selectTurretImage;

    // Use this for initialization
    void Start () {
        enemyPrefabs = Resources.LoadAll<GameObject>("Prefab/Enemy");

        respawnPosition = GameObject.Find("DownPathZone").transform;

        enemyList = new List<GameObject>();
        enemyCurrentCount = 0;
        enemyMaxCount = 5;
        enemyAllCreated = false;

        stageCount = 0;

        stageTime = 170;
        time = stageTime;

        state = eGameState.Wait;

        timerText = GameObject.Find("Timer").GetComponent<Text>();

        moneyCount = 10;
        moneyText = GameObject.Find("Money").GetComponent<Text>();
        moneyText.text = "Money : " + moneyCount.ToString();

        lifeCount = 20;
        lifeText = GameObject.Find("Life").GetComponent<Text>();
        lifeText.text = "Life : " + lifeCount.ToString();

        upgradeCount = 0;
        upgradeCost = 1;
        upgradeText = GameObject.Find("Upgrade").transform.Find("Text").GetComponent<Text>();
        upgradeText.text = upgradeCost.ToString();

        SetTimer();

        cardBoard = GameObject.Find("CardBoard").GetComponent<CardBoard>();
        cardBoard.SetCard();



        selectTurretImage = GameObject.Find("TurretSelect");

        state = eGameState.Wait;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == eGameState.Play)
        {
            if ((time <= 0 || enemyList.Count == 0) && enemyAllCreated)
            {
                EndStage();
            }
            else
                SetTimer();
        }
        else if (state == eGameState.Wait)
        {

        }
    }

    public void SetTurretImagePosition(float x, float y)
    {
        selectTurretImage.transform.position = new Vector3(x, y, -3);
    }

    private void SetTimer()
    {
        time -= Time.deltaTime;

        float min = time / 60;
        string minString = "";

        if (min <= 1)
            minString = "0";
        else
            minString = "1";

        float sec = time % 60;
        string secString = "";

        if (sec <= 10)
        {
            secString += "0";
        }

        secString += sec.ToString("N0");

        timerText.text = minString + ":" + secString;
    }

    private void EndStage()
    {
        if (lifeCount <= 0)
        {
            state = eGameState.End;
            return;
        }

        lifeCount -= enemyCurrentCount;
        lifeText.text = "Life : " + lifeCount.ToString();

        moneyCount += 10;
        moneyText.text = "Money : " + moneyCount.ToString();

        foreach (GameObject enemy in enemyList)
        {
            Destroy(enemy.gameObject);
        }

        enemyList.Clear();
        enemyCurrentCount = 0;

        cardBoard.SetCard();

        state = eGameState.Wait;
    }

    public void RemoveEnemy(GameObject enemy)
    {
        enemyList.Remove(enemy);
    }

    public void StartStage()
    {
        enemyAllCreated = false;
        enemyCurrentCount = 0;
        time = stageTime;
        stageCount++;
        state = eGameState.Play;
        StartCoroutine(CreateEmeny());
    }

    private IEnumerator CreateEmeny()
    {
        GameObject enemy = enemyPrefabs[(stageCount - 1) % enemyPrefabs.Length];

        while (true)
        {
            if (time <= stageTime - 1)
            {
                Enemy enemyObject = Instantiate(enemy, new Vector3(respawnPosition.position.x, respawnPosition.position.y, enemy.transform.position.z), Quaternion.identity).GetComponent<Enemy>();

                enemyList.Add(enemyObject.gameObject);
                enemyObject.Init(50 * stageCount);
                enemyCurrentCount++;

                if (enemyCurrentCount >= enemyMaxCount)
                {
                    enemyAllCreated = true;

                    break;
                }

                yield return new WaitForSeconds(0.3F);
            }

            yield return null;
        }
    }

    public void TurretUpgrade()
    {
        if (moneyCount >= upgradeCost)
        {
            moneyCount -= upgradeCost;
            moneyText.text = "Money : " + moneyCount.ToString();

            upgradeCost++;
            upgradeCount++;

            upgradeText.text = upgradeCost.ToString();
        }
    }

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;
                if (!_instance)
                {
                    GameObject container = new GameObject();
                    container.name = "GameManager";
                    _instance = container.AddComponent(typeof(GameManager)) as GameManager;
                }
            }

            return _instance;
        }
    }
}
