using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : EntityBehaviour
{
    [Header("Prefab Attributes")]
    public GameObject projectile;
    public GameObject projectile2;
    public ParticleSystem footstepParticles;
    public List<GameObject> footsteps = new List<GameObject>();

    [Header("Graphical Attributes")]
    public Animator anim;
    public CameraMovement mainCam;

    [Header("Sound Attributes")]
    public GameObject audioManager;
    private AudioManagerBehaviour audioManagerBehaviour;

    [Header("Internal")]
    public Vector2 direction;
    public Player player;
    public Transform weaponPos;
    private GameObject tempProj;
    private bool shooting;
    // Start is called before the first frame update
    void Start()
    {
        player = new Player(100, 5, true, FindWeapon());
        audioManagerBehaviour = audioManager.GetComponent<AudioManagerBehaviour>();
    }

    //Getter Setter
    override
    public Entity GetEntity()
    {
        return player;
    }
    override
    public void SetEntity(Entity entity)
    {
        player = entity as Player;
    }
    private void Shoot()
    {
        if(shooting == true)
        {
            player.Shoot();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //On Spacebar down
        if(Input.GetKeyDown("space"))
        {
            shooting = true;
            
            //audioManagerBehaviour.PlaySound("plop");
        }
        if(Input.GetKeyUp("space"))
        {
            shooting = false;
        }
        Shoot();

        //Get Input Axis
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        //No Input Case
        if(direction.magnitude > 0)
        {
            anim.speed = (player.GetSpeed() / mainCam.speed) / 1.5f;
            audioManagerBehaviour.PlaySound("pig_walk",true);
            audioManagerBehaviour.SetPitch("pig_walk", (player.GetSpeed() / mainCam.speed));
        }
        else
        {
            anim.speed = 1;
            audioManagerBehaviour.SetPitch("pig_walk", 1.5f);
        }
        

        //Is Player Dead
        if(player.alive != true)
        {
            Destroy(this.gameObject);

        }
    }

    public void Footstep(int seite)
    {
        //Instantiate Footprints
        int randomFootstepIndex = Random.Range(0, footsteps.Count);
        Vector2 footStepPosition = new Vector2();
        switch(seite)
        {
            case 0:
                footStepPosition.x = this.transform.position.x + 0.27f;
                break;
            case 1:
                footStepPosition.x = this.transform.position.x - 0.27f;
                break;
        }
        footStepPosition.y = this.transform.position.y - 0.8f;
        Instantiate(footstepParticles, footStepPosition, Quaternion.identity);
        Instantiate(footsteps[randomFootstepIndex], footStepPosition, Quaternion.identity);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(player.Move(direction, mainCam.speed, Time.fixedDeltaTime));
    }
}
