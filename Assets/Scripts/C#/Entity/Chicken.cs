using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : Entity, knockbackable
{
    public State stateObj;
    private Timer shootTimer = new Timer(0);
    public Chicken (Vector2 position, Vector2 direction, Weapon weapon)
    {
        this.alive = true;
        this.health = 50;
        this.speed = 0;
        this.timeBetweenShots = 1f;
        shootTimer.StartTimer(timeBetweenShots);
        this.position = position;
        this.direction = direction;
        this.weapon = weapon;
    }


    public override void Update(float deltaTime)
    {
        switch (state)
        {
            case States.idle:
                this.speed = 0;
                break;
            case States.walking:
                break;
            case States.attacking:
                this.direction = new Vector2(0, -1);
                Attack(deltaTime);
                break;
            case States.knockback:
                this.speed = 10;
                if (stateObj.AchievedGoal(this) == true)
                {
                    if(health <= 0)
                    {
                        Kill();
                    }
                    else
                    {
                        this.state = States.attacking;
                    }
                }
                else
                {
                    Move(deltaTime);
                }
                break;
            case States.dead:
                this.speed = 5;
                float RandomCorpsDirection = Random.Range(0, 360) * Mathf.Deg2Rad;
                ApplyKnockback(new Vector2(Mathf.Cos(RandomCorpsDirection), Mathf.Sin(RandomCorpsDirection))*5);
                break;
        }
    }
    public void ApplyKnockback(Vector2 knockback)
    {
        state = States.knockback;
        stateObj = new KnockbackState(this.position + knockback);
        Vector2 goalPosition = this.position + knockback;
        this.direction = goalPosition - this.position;
    }

    private void Attack(float deltaTime)
    {
        if (shootTimer.TimerUp())
        {
            Shoot();
            shootTimer.StartTimer(timeBetweenShots);
        }
        else
        {
            shootTimer.Tick(Time.deltaTime);
        }
    }

    public override void Move(float deltaTime)
    {
        this.position = this.position + direction.normalized * this.speed * deltaTime;
    }
}
