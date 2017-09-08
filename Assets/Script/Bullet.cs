using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public GameObject target;

    private Vector3 startPosition;

	// Use this for initialization
	void Start () {
        target = GameObject.Find("DumpEnemy");
        startPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 dir = target.transform.position - transform.position;
        float distanceThisFrame = 20F * Time.deltaTime;

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Enemy")
        {
            Debug.Log("d");
            Destroy(this.gameObject);
        }
    }
}
