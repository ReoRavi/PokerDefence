using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTile : TouchObject {

    private CardBoard cardBoard;

    private Turret turret;

    // Move
    //private Transform dragbox;
    //private Vector3 startTouchPos;
    //private Vector3 currentTouchPos;

    //private float xPos, yPos;

    //private int xDirection, yDirection;

    // Use this for initialization
    //dragbox = GameObject.Find("DragBox").GetComponent<Transform>();
    //dragbox.gameObject.SetActive(false);

    //xPos = 0;
    //yPos = 0;

    //xDirection = 0;
    //yDirection = 0;

    void Start () {
        cardBoard = GameObject.Find("CardBoard").GetComponent<CardBoard>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnMouseDown()
    {
        if (GameManager.Instance.isTurretSet)
        {
            turret = GameManager.Instance.setTurret.GetComponent<Turret>();

            Turret turretObject = Instantiate(turret, new Vector3(transform.position.x, transform.position.y, turret.transform.position.z), Quaternion.identity).GetComponent<Turret>();
            GameManager.Instance.turrets.Add(turretObject);
            turretObject.Init(GameManager.Instance.turretLevel);

            GameManager.Instance.isTurretSet = false;

            GameManager.Instance.StartStage();
        }
    }


    // Move
    //void OnMouseDown()
    //{
    //    startTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    //    Vector3 size = dragbox.GetComponent<Renderer>().bounds.size;

    //    dragbox.transform.position = new Vector3(startTouchPos.x + (size.x / 2), startTouchPos.y - (size.y / 2), dragbox.position.z);

    //}

    //private void OnMouseDrag()
    //{
    //    dragbox.gameObject.SetActive(true);

    //    currentTouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    //    dragbox.transform.localScale = new Vector3(currentTouchPos.x - startTouchPos.x, startTouchPos.y - currentTouchPos.y, dragbox.transform.position.z);

    //    Vector3 size = dragbox.GetComponent<Renderer>().bounds.size;

    //    if (currentTouchPos.x < startTouchPos.x)
    //    {
    //        xPos = currentTouchPos.x + (size.x / 2);
    //        xDirection = 1;
    //    }
    //    else
    //    {
    //        xPos = startTouchPos.x + (size.x / 2);
    //        xDirection = -1;
    //    }

    //    if (currentTouchPos.y < startTouchPos.y)
    //    {
    //        yPos = startTouchPos.y - (size.y / 2);
    //        yDirection = 1;
    //    }
    //    else
    //    {
    //        yPos = currentTouchPos.y - (size.y / 2);
    //        yDirection = -1;
    //    }

    //    dragbox.transform.position = new Vector3(xPos, yPos, dragbox.position.z);
    //    Debug.Log("Drag");
    //}

    //private void OnMouseUp()
    //{
    //    if (GameManager.Instance.selectedTurrets.Count == 0)
    //    {
    //        GameManager.Instance.SelectDragedTurret(dragbox.transform.position, dragbox.transform.localScale, xDirection, yDirection);
    //    }
    //    else
    //    {
    //        foreach (Turret turret in GameManager.Instance.selectedTurrets)
    //        {
    //            if (!cardBoard.isBoardOpen)
    //            {
    //                turret.Move(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    //            }
    //        }
    //    }

    //    dragbox.gameObject.SetActive(false);
    //}
}
