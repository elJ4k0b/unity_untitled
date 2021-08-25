using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandartBulletBehaviour : BulletBehaviour
{
    public StandartBullet standartBullet;
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        standartBullet = new StandartBullet();
        this.timer = new Timer(standartBullet.lifetime);
        if(standartBullet == null)
        {
            Debug.Log("Bullet Zuweisung klappt nicht");
        }
    }
    override
    public Bullet GetBullet()
    {
        return standartBullet;
    }
    private void OnDrawGizmos()
    {
        rb = GetComponent<Rigidbody2D>();
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(rb.velocity.x, rb.velocity.y, 0));
    }
    // Update is called once per frame
    private void Update()
    {
        this.timer.Tick(Time.deltaTime);
        if(this.timer.TimerUp())
        {
            Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        EntityBehaviour target = collision.gameObject.GetComponent<EntityBehaviour>();
        if (target != null)
        {
            standartBullet.DealDamage(target.GetEntity());
            if(target.GetEntity() is knockbackable)
            {
                standartBullet.Knockback(rb.velocity,target.GetEntity() as knockbackable);
            }
        }
        if(collision.gameObject.CompareTag("projectile"))
        {
            Destroy(this.gameObject);
        }
    }
}
