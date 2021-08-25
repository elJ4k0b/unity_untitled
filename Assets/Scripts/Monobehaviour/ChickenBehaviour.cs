using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenBehaviour : EntityBehaviour
{
    [SerializeField] private GameObject tempProj;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Animator anim;
    public GameObject deathParticles;
    public GameObject AudioManager;
    public Transform weaponPos;
    private Timer shootTimer;
    private Chicken chicken;

    // Start is called before the first frame update
    void Start()
    {
        chicken = new Chicken(this.gameObject.transform.position, new Vector2(0, -1), FindWeapon());
        shootTimer = new Timer(chicken.timeBetweenShots);
        this.AudioManager = GameObject.FindGameObjectWithTag("Audio");
        AudioManager.GetComponent<AudioManagerBehaviour>().PlaySound("chicken_idle", true);
    }

    override
    public Entity GetEntity()
    {
        return chicken;
    }
    override
    public void SetEntity(Entity entity)
    {
        this.chicken = entity as Chicken;
    }

    // Update is called once per frame
    void Update()
    {
        if(shootTimer.TimerUp())
        {
            chicken.Shoot();
            shootTimer.StartTimer(chicken.timeBetweenShots);
        }
        else
        {
            shootTimer.Tick(Time.deltaTime);
        }

        if(chicken.alive != true)
        {
            Die();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        anim.Play("chicken_damage");
        Instantiate(GameAssets.instance.dustParticles, this.transform);
    }
    private void Die()
    {
        AudioManager.GetComponent<AudioManagerBehaviour>().PlaySound("chicken_damage");
        AudioManager.GetComponent<AudioManagerBehaviour>().PlaySound("puff");
        GameObject particles = Instantiate(deathParticles, this.transform.position, Quaternion.Euler(-90, 0, 0));
        Destroy(this.gameObject);
    }
}
