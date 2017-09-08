using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTile : MonoBehaviour {

    public GameObject cardBoard;
    //public GameObject turret;

	// Use this for initialization
	void Start () {
        cardBoard = GameObject.Find("CardBoard");
    }
	
	// Update is called once per frame
	void Update () {
        		
	}

    private void OnMouseDown()
    {
        cardBoard.SetActive(true);

        cardBoard.GetComponent<CardBoard>().SetCard(transform);
    }
}
