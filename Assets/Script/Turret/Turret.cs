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
    public Vector3 moveTarget;

    private int damage;

    private GameObject selectImage;

    [SerializeField]
    private float collisionTime;
    [SerializeField]
    private bool collisionState;

    // Use this for initialization
    void Start () {
        createTime = 0;
        move = false;
        selectImage = transform.Find("TurretSelect").gameObject;
        selectImage.SetActive(false);

        collisionTime = 0;
        collisionState = false;
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
        if (target != null)
        {
            Vector3 dir = transform.position - target.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        createTime += GameManager.Instance.GetDeltaTimeByGameSpeed();

        if (move)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(moveTarget.x, moveTarget.y, transform.position.z), 0.1F);
            float dis = Vector2.Distance(transform.position, moveTarget);
            //selectImage.transform.position = new Vector3(transform.position.x, transform.position.y, selectImage.transform.position.z);

            if (dis < 0.01f || collisionTime > 2F)
            {
                move = false;
                collisionTime = 0;
                collisionState = false;
                ActiveSelectImage(false);
            }

            if (collisionState)
                collisionTime += GameManager.Instance.GetDeltaTimeByGameSpeed();

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

    //public void Move(Vector3 moveTarget)
    //{
    //    move = true;
    //    GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    //    GetComponent<Rigidbody2D>().isKinematic = false;
    //    this.moveTarget = moveTarget;
    //}

    public void ActiveSelectImage(bool active)
    {
        if (!active)
            GameManager.Instance.selectedTurrets.Remove(this);

        selectImage.SetActive(active);
    }

    private void OnMouseDown()
    {
        Turret[] beforeTurret = GameManager.Instance.selectedTurrets.ToArray();

        if (beforeTurret.Length != 0)
        {
            foreach (Turret turret in beforeTurret)
            {
                if (turret.move)
                    StartCoroutine(KinematicCorutine(turret));
                else
                {
                    turret.GetComponent<Rigidbody2D>().isKinematic = true;
                    turret.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                }
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

        GameManager.Instance.selectedTurrets.Clear();
        GameManager.Instance.selectedTurrets.Add(this);
        ActiveSelectImage(true);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Turret")
        {
            collisionState = true;
        }
    }

    private void OnCollisionExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Turret")
        {
            collisionTime = 0;
            collisionState = false;
        }
    }
}
