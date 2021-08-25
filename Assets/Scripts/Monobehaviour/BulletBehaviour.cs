using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletBehaviour : MonoBehaviour
{
    //[SerializeField]  protected Entity attachedEntity;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected BoxCollider2D hitDetector; 
    protected Timer timer = new Timer(0);
    // Start is called before the first frame update
    public abstract Bullet GetBullet();
}
