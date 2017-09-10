using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public Vector2 direction;
    private Transform hpBar;
    private Vector3 hpBarlocalScale;

    private float fullHp;
    private float hp;

    // Use this for initialization
    void Start () {
        direction = Vector2.down;
        fullHp = 50;
        hp = 50;
        hpBar = transform.Find("DamagedHP");
        hpBarlocalScale = hpBar.localScale;
    }

    public void Init(float hp)
    {
        fullHp = hp;
        this.hp = hp;
    }

    // Update is called once per frame
    void Update () {
        transform.Translate(Vector2.down * 4 * Time.deltaTime);
	}

    public void Attacked(float damage)
    {
        hp -= damage;

        
        hpBar.localScale = new Vector3(hpBarlocalScale.x * (hp / fullHp), hpBarlocalScale.y, hpBarlocalScale.z);

        if (hp <= 0)
        {
            GameManager.Instance.RemoveEnemy(gameObject);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PathZone")
        {
            Vector3 pos = collision.transform.position;

            transform.position = new Vector3(pos.x, pos.y, transform.position.z);
            transform.eulerAngles = collision.GetComponent<PathObject>().path;
        }
    }
}
