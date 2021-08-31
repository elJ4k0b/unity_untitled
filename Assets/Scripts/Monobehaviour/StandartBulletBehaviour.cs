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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        EntityBehaviour target = collision.gameObject.GetComponent<EntityBehaviour>();
        if (target != null)
        {
            if (target.GetEntity() is knockbackable)
            {
                Vector2 contactPoint = collision.contacts[((collision.contacts.Length - 1) / 2)].point;
                Vector2 targetPosition = collision.gameObject.transform.position;
                Vector2 direction = targetPosition - contactPoint;
                standartBullet.Knockback(direction, target.GetEntity() as knockbackable);
            }
            standartBullet.DealDamage(target.GetEntity());
        }
        Destroy(this.gameObject);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
