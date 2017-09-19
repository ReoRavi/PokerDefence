using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAttack : MonoBehaviour {

    // Turret
    private Turret turret;
    // In Range Objects
    private List<Transform> rangeObject;
    // Currnet Target
    private Transform target;

	// Use this for initialization
	void Start () {
        rangeObject = new List<Transform>();
        turret = transform.parent.gameObject.GetComponent<Turret>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionGameObj = collision.gameObject;

        if (collisionGameObj.tag == "Enemy")
        {
            rangeObject.Add(collisionGameObj.transform);

            if (target == null)
            {
                target = collision.gameObject.transform;
                turret.target = collision.gameObject.transform;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject collisionGameObj = collision.gameObject;

        rangeObject.Remove(collisionGameObj.transform);

        if (target == collisionGameObj.transform)
        {
            if (rangeObject.Count != 0)
            {
                target = rangeObject[0];
                turret.target = rangeObject[0];
            }
            else
            {
                target = null;
                turret.target = null;
            }
        }
    }
}
