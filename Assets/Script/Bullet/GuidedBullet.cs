using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedBullet : Bullet
{
    // Start Positon (Turret Position)
    private Vector3 startPosition;

	// Use this for initialization
	void Start () {
        startPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.transform.position - transform.position;
        float distanceThisFrame = 20F * GameManager.Instance.GetDeltaTimeByGameSpeed();

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    public override void Init(Transform target, float damage)
    {
        this.target = target;
        this.damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().Attacked(damage);
            Destroy(gameObject);
        }
    }
}
