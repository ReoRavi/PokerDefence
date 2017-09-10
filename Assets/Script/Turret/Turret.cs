using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    public Transform target;

    [SerializeField]
    private float attackSpeed;
    [SerializeField]
    private int attackCount;

    private GameObject bullet;
    private float createTime;

    public bool move;
    public Vector3 moveTarget;

    [SerializeField]
    private int damage;

	// Use this for initialization
	void Start () {
        createTime = 0;
        move = false;
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
    void Update () {
        createTime += Time.deltaTime;

        if (GameManager.Instance.currentTurret == this)
            GameManager.Instance.SetTurretImagePosition(transform.position.x, transform.position.y);

        if (move)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(moveTarget.x, moveTarget.y, transform.position.z), 0.1F);
            float dis = Vector2.Distance(transform.position, moveTarget);

            if (dis < 0.01f)
            {
                move = false;
            }
        }
        else
        {
            if (createTime > attackSpeed && target != null)
            {
                Vector3 dir = transform.position - target.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                for (int i = 0; i < attackCount; i++)
                {
                    Bullet bulletObject = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y, bullet.transform.position.z), Quaternion.identity).GetComponent<Bullet>();
                    bulletObject.Init(target, damage + (GameManager.Instance.upgradeCount + (2 * GameManager.Instance.upgradeCount)));
                }

                createTime = 0;
            }
        }
    }

    public void Move(Vector3 moveTarget)
    {
        move = true;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        GetComponent<Rigidbody2D>().isKinematic = false;
        this.moveTarget = moveTarget;
    }

    private void OnMouseDown()
    {
        Turret beforeTurret = GameManager.Instance.currentTurret;

        if (beforeTurret != null)
        {
            if (beforeTurret.move)
                StartCoroutine(KinematicCorutine(beforeTurret));
            else
            {
                beforeTurret.GetComponent<Rigidbody2D>().isKinematic = true;
                beforeTurret.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            }
        }

        if (move)
        {
            GetComponent<Rigidbody2D>().isKinematic = false;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            GetComponent<Rigidbody2D>().isKinematic = true;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }

        GameManager.Instance.currentTurret = this;
        GameManager.Instance.SetTurretImagePosition(transform.position.x, transform.position.y);
    }

    private IEnumerator KinematicCorutine(Turret turret)
    {
        while (true)
        {
            // Finish Move
            if (!turret.move)
            {
                turret.GetComponent<Rigidbody2D>().isKinematic = true;
                turret.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;

                break;
            }

            yield return null;
        }
    }
}
