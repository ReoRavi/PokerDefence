using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    private GameObject bullet;
    public Transform target;
    private float createTime;
    public Vector3 dd;
	// Use this for initialization
	void Start () {
        target = GameObject.Find("DumpEnemy").transform;
        bullet = Resources.Load("Prefab/Bullet") as GameObject;
        createTime = 0;
    }
	
	// Update is called once per frame
	void Update () {
        createTime += Time.deltaTime;

        if (createTime > 0.3F)
        {
            Vector3 dir = transform.position - target.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            
            Instantiate(bullet, new Vector3(transform.position.x, transform.position.y, bullet.transform.position.z), Quaternion.identity);
            createTime = 0;
        }
    }
}
