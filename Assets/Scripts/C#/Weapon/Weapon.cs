﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    public float shotPower;
    public float fireRate;
    public float bloom;
    private float reloadTime;
    private int remainingAmmo;
    private int magazineSize;
    public int burstSize { get; private set; }
    public string oldState;
    public States state = States.ready;
    private bool readyToShoot = true;
    private Bullet bullet = new GrenadeBullet();
    private Timer shotTimer = new Timer(0);
    private Timer reloadTimer = new Timer(0);
    public List<Bullet> bulletsToShoot = new List<Bullet>();
    public Vector2 direction;

    public enum States
    {
        ready,
        shooting,
        reloading,
        cooldown
    }

    public Weapon(float shotPower, float fireRate, float bloom, float reloadTime, int magazineSize, int burstSize)
    {
        this.state = States.ready;
        this.oldState = "ready";
        this.reloadTime = reloadTime;
        this.magazineSize = magazineSize;
        this.shotPower = shotPower;
        this.fireRate = fireRate;
        this.bloom = bloom;
        this.burstSize = burstSize;
        this.remainingAmmo = magazineSize;
        shotTimer.StartTimer(1 / fireRate);
        shotTimer.StartTimer(reloadTime);
    }
    public Weapon(weaponScriptableObject weapon)
    {
        this.state = States.ready;
        this.oldState = "ready";
        this.reloadTime = weapon.reloadTime;
        this.magazineSize = weapon.magazineSize;
        this.shotPower = weapon.shotPower;
        this.fireRate = weapon.fireRate;
        this.bloom = weapon.bloom;
        this.burstSize = weapon.burstSize;
        this.remainingAmmo = magazineSize;
        shotTimer.StartTimer(1 / fireRate);
        shotTimer.StartTimer(reloadTime);
    }

    public void Update(float deltaTime)
    {
        switch(state)
        {
            case States.ready:
                break;
            case States.shooting:
                break;
            case States.reloading:
                Reload(deltaTime);
                break;
            case States.cooldown:
                CoolDown(deltaTime);
                break;
        }
    }
    public virtual void Shoot(Vector2 shotDir)
    {
        if(oldState == "ready")
        {
            for (int i = 0; i < burstSize; i++)
            {
                if (remainingAmmo > 0)
                {
                    remainingAmmo -= 1;
                    bullet = new GrenadeBullet();
                    bulletsToShoot.Add(bullet);
                    this.direction = shotDir;
                }
                else
                {
                    reloadTimer.StartTimer(reloadTime);
                    oldState = "reloading";
                    break;
                }
            }
            if(oldState != "reloading")
            {
                shotTimer.StartTimer(1 / fireRate);
                oldState = "cooldown";
            }
        }
    }
    public void Reload(float deltaTime)
    {
        reloadTimer.Tick(deltaTime);
        if(reloadTimer.TimerUp())
        {
            oldState = "ready";
            reloadTimer.StartTimer(reloadTime);
            this.remainingAmmo = magazineSize;
        }
    }
    public void CoolDown(float deltaTime)
    {
        shotTimer.Tick(deltaTime);
        if (shotTimer.TimerUp())
        {
            oldState = "ready";
        }
    }
    public Vector2 CalculateBloom()
    {
        float angle = Vector2.Angle(new Vector2(1, 0), this.direction);
        angle += Random.Range(-bloom, bloom);
        Vector2 newDirection = new Vector2();
        newDirection.x = Mathf.Cos(angle * Mathf.Deg2Rad);
        newDirection.y = Mathf.Sin(angle * Mathf.Deg2Rad);
        newDirection.Normalize();
        if(direction.x < 0)
        {
            newDirection.x *= -1;
        }
        else if(direction.y < 0)
        {
            newDirection.y *= -1;
        }
        return newDirection;
    }
    public bool ReadyToShoot()
    {
        return readyToShoot;
    }
}
