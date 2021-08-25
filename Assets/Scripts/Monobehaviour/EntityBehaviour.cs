using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityBehaviour : MonoBehaviour
{
    [Header("Inherited Attributes")]
    public Rigidbody2D rb;
    public Collider2D hitDetector;
    protected Entity entity;
    public WeaponBehaviour weaponGameObject;

    public virtual Weapon FindWeapon()
    {
        weaponGameObject = gameObject.GetComponentInChildren<WeaponBehaviour>();
        if(weaponGameObject != null)
        {
            return this.weaponGameObject.weapon;
        }
        else
        {
            Debug.Log("Entity " + this.gameObject.name + " hat kein Waffenobject gefunden");
            return null;
        }
    }
    public abstract Entity GetEntity();
    public abstract void SetEntity(Entity entity);
}
