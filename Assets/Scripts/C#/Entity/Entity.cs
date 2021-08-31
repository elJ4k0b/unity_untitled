using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity
{
    public bool alive { get; protected set;}
    protected float health;
    protected float speed;
    public float timeBetweenShots {get; protected set;}
    public Vector2 position;
    protected Vector2 direction;
    public Weapon weapon;
    public States state = States.attacking;


    //Getter
    public float GetSpeed()
    {
        return this.speed;
    }
    public Vector2 GetDirection()
    {
        return this.direction;
    }

    //Update Function
    public abstract void Update(float deltaTime);

    //Abstract Movement
    public abstract void Move(float deltaTime);

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
                health = 0;
                state = States.dead;
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
        if(weapon != null)
        {
            weapon.Shoot(direction);
        }
    }

    // Destroy Function
    protected void Kill()
    {
        alive = false;

    }

}
