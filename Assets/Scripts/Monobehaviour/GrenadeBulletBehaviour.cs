using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBulletBehaviour : BulletBehaviour
{
    [SerializeField] private CircleCollider2D explosionArea;
    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private List<GameObject> explosionVisuals = new List<GameObject>();
    private Animator anim;
    private GrenadeBullet grenadeBullet;
    private GameObject trail;


    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        grenadeBullet = new GrenadeBullet();
        anim = GetComponent<Animator>();
        explosionArea.radius = grenadeBullet.explosionRadius;
        trail = Instantiate(GameAssets.instance.trailParticles, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        trail.transform.parent = this.gameObject.transform;

    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 3);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject trigger = collision.gameObject;
        if(trigger.gameObject.GetComponent<EntityBehaviour>() != null)
        {
            grenadeBullet.AddEntityToDamage(trigger.gameObject.GetComponent<EntityBehaviour>().GetEntity());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject trigger = collision.gameObject;
        if (trigger.gameObject.GetComponent<EntityBehaviour>() != null)
        {
            grenadeBullet.RemoveEntityToDamage(trigger.gameObject.GetComponent<EntityBehaviour>().GetEntity());
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //grenadeBullet.DealDamage(grenadeBullet.entitysToDamage.ToArray());
    }

    // Update is called once per frame
    void Update()
    {
        switch(grenadeBullet.GetCurrentState())
        {
            case "flying":
                this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            break;
            case "exploding":
                this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                Destroy(trail);
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("grenade_exploding") == false)
                {
                    anim.Play("grenade_exploding");
                }
                if (grenadeBullet.lifetime <= 0)
                {
                    Explode();
                }
            break;

        } 
    }
    private void FixedUpdate()
    {
        if(rb != null)
        {
            Move();
        }
    }
    public override Bullet GetBullet()
    {
        return this.grenadeBullet;
    }

    //Flug
    private void Move()
    {
        grenadeBullet.Move(Time.deltaTime);
    }

    //Explosionsverhalten
    public void Explode()
    {
        grenadeBullet.DealDamage(grenadeBullet.entitysToDamage.ToArray());
        GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerBehaviour>().PlaySound("grenade_splash");
        InstantiateExplosionVisuals();
        InstantiateExplosionParticles();
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamShake>().ShakeCamera();
        Destroy(this.gameObject);
    }

    //Instantiate Explosion Visuals
    private void InstantiateExplosionVisuals ()
    {
        Instantiate(explosionVisuals[0], this.transform.position, Quaternion.identity);
        for (int i = 0; i <= Random.Range(2, 18); i++)
        {
            float explosionRadius = 1.5f;
            Vector3 visualPosition = new Vector3();
            visualPosition.x = Random.Range(-explosionRadius, explosionRadius);
            visualPosition.y = Random.Range(-explosionRadius, explosionRadius);
            Instantiate(explosionVisuals[Random.Range(1, explosionVisuals.Count)], this.transform.position + visualPosition.normalized * explosionRadius,Quaternion.identity);
        }
    }
    private void InstantiateExplosionParticles()
    {
        //Instantiate ExplosionParticles
        ParticleSystem particles = Instantiate(explosionParticle, this.transform.position, Quaternion.Euler(-180, 0, 0));
        var main = particles.main;
        main.startLifetime = grenadeBullet.explosionRadius / 10f;
    }
}
