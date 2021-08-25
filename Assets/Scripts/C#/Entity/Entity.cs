using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity
{
    public bool alive { get; protected set;}
    protected float health;
    protected float speed;
    public float timeBetweenShots {get; protected set;}
    protected Vector2 position;
    protected Vector2 direction;
    public Weapon weapon;

    //Getter
    public float GetSpeed()
    {
        return this.speed;
    }
    public Vector2 GetDirection()
    {
        return this.direction;
    }

    //Abstract Movement
    public abstract Vector2 Move(Vector2 direction, float speed, float deltaTime);

    //Take Damage Function
    public void TakeDamage(float damage)
    {
        if (damage <= 0)
        {
            return;
        }
        else
        {
            health -= damage;
            if (health <= 0)
            {
                Kill();
            }
        }
        health -= damage;
    }

    //Shoot Fuction
    public virtual void Shoot()
    {
        if(this.speed <= 0)
        {
            this.speed = 1;
        }
        weapon.Shoot(direction);
    }

    // Destroy Function
    protected void Kill()
    {
        alive = false;
        health = 0;

    }

}
