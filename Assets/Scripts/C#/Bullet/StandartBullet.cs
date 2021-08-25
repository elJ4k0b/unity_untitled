using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandartBullet : Bullet
{
    public StandartBullet()
    {
        this.damage = 20;
        this.lifetime = 0.5f;
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
