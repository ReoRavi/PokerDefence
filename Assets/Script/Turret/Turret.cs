using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    public Transform target;

    private float attackSpeed;
    private int attackCount;

    private GameObject bullet;
    private float createTime;

    public bool move;

    private int damage;

    private GameObject selectImage;

    // Use this for initialization
    void Start () {
        createTime = 0;
        move = false;
        selectImage = transform.Find("TurretSelect").gameObject;
        selectImage.SetActive(false);
    }

    public void Init(int turretLevel)
    {
        damage = (turretLevel + 1) * 5;

        if (turretLevel % 2 == 0)
            bullet = Resources.Load("Prefab/GuidedBullet") as GameObject;
        else
            bullet = Resources.Load("Prefab/PromptBullet") as GameObject;

        attackSpeed = 1F - ((turretLevel + 1) * 0.05F);
        attackCount = (turretLevel / 8) + 1;
    }

    // Update is called once per frame
    void Update() {
        if (target != null)
        {
            Vector3 dir = transform.position - target.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        createTime += GameManager.Instance.GetDeltaTimeByGameSpeed();


        if (move && TouchManager.Instance.unitMove)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            transform.position = new Vector3(pos.x, pos.y, transform.position.z);
        }
        else
        {
            if (createTime > attackSpeed && target != null)
            {
                for (int i = 0; i < attackCount; i++)
                {
                    Bullet bulletObject = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y, bullet.transform.position.z), Quaternion.identity).GetComponent<Bullet>();
                    bulletObject.Init(target, damage + (GameManager.Instance.upgradeCount + (2 * GameManager.Instance.upgradeCount)));
                }

                createTime = 0;
            }
        }
    }

    public void ActiveSelectImage(bool active)
    {
        selectImage.SetActive(active);
    }

    private void OnMouseDown()
    {
        ActiveSelectImage(true);

        move = true;
    }

    private void OnMouseUp()
    {
        ActiveSelectImage(false);

        move = false;
    }
}
