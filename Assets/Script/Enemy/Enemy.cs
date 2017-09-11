using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public Vector2 direction;

    public GameObject hpBar;

    private GameObject damagedHpBar;

    private Vector3 hpBarlocalScale;

    private float fullHp;
    private float hp;

    private int moveState;

    private float attackedTime;

    // Use this for initialization
    void Start () {
        direction = Vector2.down;
        fullHp = 50;
        hp = 50;

        hpBar = Instantiate(Resources.Load("Prefab/HpBar"), transform.position, Quaternion.identity) as GameObject;
        damagedHpBar = hpBar.transform.Find("DamagedHP").gameObject;
        hpBarlocalScale = damagedHpBar.transform.localScale;

        attackedTime = 0;
    }

    public void Init(float hp)
    {
        fullHp = hp;
        this.hp = hp;
    }

    // Update is called once per frame
    void Update () {
        transform.Translate(Vector2.down * 4 * GameManager.Instance.GetDeltaTimeByGameSpeed());
        hpBar.transform.position = transform.position;

        attackedTime += Time.deltaTime;

    }

    public void Attacked(float damage)
    {
        hp -= damage;

        damagedHpBar.transform.localScale = new Vector3(hpBarlocalScale.x * (hp / fullHp), hpBarlocalScale.y, hpBarlocalScale.z);

        if (hp <= 0)
        {
            Debug.Log(attackedTime);
            GameManager.Instance.RemoveEnemy(gameObject);
            Destroy(hpBar);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PathZone")
        {
            Vector3 pos = collision.GetComponent<PathObject>().pos;

            transform.position = new Vector3(pos.x, pos.y, transform.position.z);
            transform.eulerAngles = collision.GetComponent<PathObject>().path;
        }
    }
}
