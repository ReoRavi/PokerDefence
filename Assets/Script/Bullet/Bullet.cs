using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour {

    // Target Transform
    protected Transform target;
    // Damage
    protected float damage;

    // Target Set
    public abstract void Init(Transform target, float damage);
}
