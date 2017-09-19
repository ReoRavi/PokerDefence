using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    [SerializeField]
    // Turret Drag Pos
    // 0 : Left, Up
    // 1 : Right, Down
    private GameObject[] enemyTiles;
    // Unit Move
    public bool unitMove;

    // Use this for initialization
    void Start()
    {
        unitMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (mousePos.x > enemyTiles[0].transform.position.x + 0.3F &&
                mousePos.x < enemyTiles[1].transform.position.x - 0.3F &&
                mousePos.y < enemyTiles[0].transform.position.y - 0.3F &&
                mousePos.y > enemyTiles[1].transform.position.y + 0.3F)
            {
                unitMove = true;
            }
            else
            {
                unitMove = false;
            }
        }
    }

    private static TouchManager _instance;
    public static TouchManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(TouchManager)) as TouchManager;
                if (!_instance)
                {
                    GameObject container = new GameObject();
                    container.name = "GameManager";
                    _instance = container.AddComponent(typeof(TouchManager)) as TouchManager;
                }
            }

            return _instance;
        }
    }
}

