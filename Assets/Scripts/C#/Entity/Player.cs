using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: Entity
{
    public Player(float health, float speed, bool alive, Weapon weapon)
    {
        this.direction = new Vector2(0, 1);
        this.speed = speed;
        this.health = health;
        this.alive = alive;
        this.position = new Vector2(0, 0);
        this.weapon = weapon;
    }

    public void OldShoot()
    {
        if(weapon != null)
        {
            weapon.Shoot(direction * this.speed);
        }
        //GrenadeBullet bullet = new GrenadeBullet(shotPos, new Vector2(0, 1), this.speed);
        //StandartBullet bullet = new StandartBullet(shotPos, new Vector2(0,1), this.speed);
    }
    override
    public Vector2 Move(Vector2 direction, float speed, float deltaTime)
    {

        Vector2 newPosition;
        if (direction.y != 0)
        {
            newPosition = position + direction.normalized * this.speed * deltaTime;
        }
        else
        {
            newPosition.x = position.x + direction.x * this.speed * deltaTime;
            newPosition.y = position.y + this.direction.y * speed * deltaTime;
        }
        this.position = newPosition;
        return newPosition;
    }
}
