using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum eGameState { Play, End }

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
    #endregion

    // Current Stage Count
    private int stageCount;

    [SerializeField]
    // Stage Time
    private float time;
    // Game State
    private eGameState state;

    // Life
    private int lifeCount;

    #region SystemUI
    // Timer
    private Text timerText;
    // life 
    private Text lifeText;
    #endregion

    // Use this for initialization
    void Start () {
        enemyPrefabs = Resources.LoadAll<GameObject>("Prefab/Enemy");

        respawnPosition = GameObject.Find("DownPathZone").transform;

        enemyList = new List<GameObject>();
        enemyCurrentCount = 0;
        enemyMaxCount = 20;

        stageCount = 0;

        time = 69;
        state = eGameState.Play;

        timerText = GameObject.Find("Timer").GetComponent<Text>();

        lifeCount = 20;
        lifeText = GameObject.Find("Life").GetComponent<Text>();
        lifeText.text = "Life : " + lifeCount.ToString();

        GetComponent<CreateMap>().CreateMapTiles();

        StartCoroutine(CreateEmeny());
    }

    // Update is called once per frame
    void Update()
    {
        if (state == eGameState.Play)
        {
            if (time <= 0)
            {
                EndStage();
            }
            else
                SetTimer();
        }
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
        lifeCount -= enemyCurrentCount;
        lifeText.text = "Life : " + lifeCount.ToString();

        foreach (GameObject enemy in enemyList)
        {
            Destroy(enemy.gameObject);
        }

        enemyList.Clear();

        if (lifeCount > 0)
        {
            time = 70;
            StartCoroutine(CreateEmeny());
        }
        else
        {
            state = eGameState.End;
        }
    }

    private IEnumerator CreateEmeny()
    {
        while (true)
        {
            if (time <= 60)
            {
                enemyList.Add(Instantiate(enemyPrefabs[stageCount], respawnPosition.position, Quaternion.identity));
                enemyCurrentCount++;

                if (enemyCurrentCount >= enemyMaxCount)
                    break;

                yield return new WaitForSeconds(0.3F);
            }

            yield return null;
        }
    }
}
