using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : Entity
{
    public Chicken (Vector2 position, Vector2 direction, Weapon weapon)
    {
        this.alive = true;
        this.health = 50;
        this.speed = 0;
        this.timeBetweenShots = 0.5f;
        this.position = position;
        this.direction = direction;
        this.weapon = weapon;
    }
    public override Vector2 Move(Vector2 direction, float speed, float deltaTime)
        {
            throw new System.NotImplementedException();
        }
}
