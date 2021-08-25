using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBullet : Bullet
{
    public float explosionRadius {get; private set;}
    private Timer timer;
    private string state;
    private float flytime;
    private float explosiontime;
    public List<Entity> entitysToDamage { get; private set;} = new List<Entity>();

    public GrenadeBullet()
    {
        this.explosionRadius = 3;
        this.flytime = 1f;
        this.explosiontime = 0.6f;
        this.lifetime = this.flytime + this.explosiontime;
        this.damage = 60;
        this.state = "flying";
        timer = new Timer(this.lifetime);
    }
    public string GetCurrentState()
    {
        return this.state;
    }
    public void AddEntityToDamage(Entity entity)
    {
        if(entitysToDamage.Contains(entity) == false)
        {
            entitysToDamage.Add(entity);
        }
    }
    public void RemoveEntityToDamage(Entity entity)
    {
        if (entitysToDamage.Contains(entity) == true)
        {
            entitysToDamage.Remove(entity);
        }
    }
    public void Move(float deltaTime)
    {
        if(state == "flying")
        {
            if (timer.RemainingTime() <= explosiontime)
            {
                this.state = "exploding";
            }
            timer.Tick(deltaTime);
        }
        else if(state == "exploding")
        {
            if(timer.TimerUp())
            {
                DealDamage(entitysToDamage.ToArray());
                this.lifetime = 0;
            }
            timer.Tick(deltaTime);
        }
        timer.Tick(deltaTime);
        Debug.Log("mist");
    }

    public override void DealDamage(Entity[] targets)
    {
        foreach (Entity target in targets)
        {
            target.TakeDamage(this.damage);
        }
    }
    public override void DealDamage(Entity target)
    {
        throw new System.NotImplementedException();
    }
}
