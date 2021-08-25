using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet
{
    public float damage;
    public float lifetime { get; protected set; }
    protected float knockbackPower;
    public abstract void DealDamage(Entity target);
    public abstract void DealDamage(Entity[] targets);
    public abstract void Knockback(Vector2 direction, knockbackable target);
}
