using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet
{
    public float damage;
    public float lifetime { get; protected set; }
    public abstract void DealDamage(Entity target);
    public abstract void DealDamage(Entity[] targets);
}
