using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandartBullet : Bullet
{
    public StandartBullet()
    {
        this.knockbackPower = 2;
        this.damage = 20;
        this.lifetime = 0.5f;
    }

    public override void Knockback(Vector2 direction, knockbackable target)
    {
        target.ApplyKnockback(direction.normalized * knockbackPower);
    }
    public override void DealDamage(Entity target)
    {
        target.TakeDamage(this.damage);
    }

    public override void DealDamage(Entity[] targets)
    {
        throw new System.NotImplementedException();
    }
}
