using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTile : TouchObject {

    private CardBoard cardBoard;

    // Use this for initialization
    void Start () {
        cardBoard = GameObject.Find("CardBoard").GetComponent<CardBoard>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnMouseDown()
    {
        Debug.Log("Ground");
        Turret turret = GameManager.Instance.currentTurret;

        if (turret != null && !cardBoard.isBoardOpen)
        {
            turret.Move(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }
}
