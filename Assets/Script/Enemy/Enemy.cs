using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public Vector2 direction;

	// Use this for initialization
	void Start () {
        direction = Vector2.down;
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(direction * 4 * Time.deltaTime);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PathZone")
        {
            Vector3 pos = collision.transform.position;

            transform.position = new Vector3(pos.x, pos.y, transform.position.z);
            direction = collision.GetComponent<PathObject>().path;
        }
    }
}
