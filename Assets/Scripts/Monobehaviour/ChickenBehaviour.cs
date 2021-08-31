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
        Debug.Log(FindWeapon());
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
        chicken.Update(Time.deltaTime);
        if(chicken.alive != true)
        {
            Die();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        anim.Play("chicken_damage");
        AnimatorClipInfo[] clipinfo =  anim.GetCurrentAnimatorClipInfo(0);
        clipinfo[0].clip
    }
    private void Die()
    {
        AudioManager.GetComponent<AudioManagerBehaviour>().PlaySound("chicken_damage");
        AudioManager.GetComponent<AudioManagerBehaviour>().PlaySound("puff");
        GameObject particles = Instantiate(GameAssets.instance.dustParticles, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        //GameObject particles2 = Instantiate(GameAssets.instance.featherParticles, this.transform.position, Quaternion.Euler(-90, 0, 0));
    }
    private void FixedUpdate()
    {
        if(rb.position != chicken.position)
        {
            rb.MovePosition(chicken.position);
        }
    }
}
