using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptBullet : Bullet
{
    private float time;
    private float maxTime;

    // Use this for initialization
    void Start()
    {
        time = 0;
        maxTime = 0.1F;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        time += Time.deltaTime;
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        if (time > maxTime)
        {
            target.GetComponent<Enemy>().Attacked(damage);
            Destroy(gameObject);
        }
    }

    public override void Init(Transform target, float damage)
    {
        this.target = target;
        this.damage = damage;
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
