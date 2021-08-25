using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    public weaponScriptableObject weaponScriptable;
    public Weapon weapon;
    [Header("Bullet")]
    public GameObject bulletObj;
    private bool shooting = false;

    private void Start()
    {
        if(weapon == null)
        {
            Debug.Log(weaponScriptable.shotPower + " " +weaponScriptable.fireRate + " " + weaponScriptable.bloom + " " + weaponScriptable.reloadTime + " " + weaponScriptable.magazineSize + " " + weaponScriptable.burstSize);
            //weapon = new Weapon(10, 5, 20, 0.2f, 10, 2);
            weapon = new Weapon(weaponScriptable);
        }
    }
    void Update()
    {
        switch (weapon.state)
        {
            case "reloading":
                weapon.Reload(Time.deltaTime);
                break;
            case "cooldown":
                weapon.CoolDown(Time.deltaTime);
                break;
            case "ready":
                if (weapon.bulletsToShoot.Count > 0)
                {
                    if (shooting == false)
                    {
                        StartCoroutine("Shoot");
                    }
                    weapon.bulletsToShoot.RemoveAt(weapon.bulletsToShoot.Count - 1);
                }
                break;
        }
    }

    private IEnumerator Shoot()
    {
        shooting = true;
        for (int i = 0; i < weapon.burstSize; i++)
        {
            Debug.Log("Schuss");
            Bullet bullet = new StandartBullet();
            GameObject bulletInstance = Instantiate(this.bulletObj, this.gameObject.transform);
            var angle = Mathf.Atan2(weapon.direction.x, weapon.direction.y) * Mathf.Rad2Deg;
            bulletInstance.transform.rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
            bulletInstance.GetComponent<Rigidbody2D>().AddForce(weapon.CalculateBloom() * weapon.shotPower * 40);
            bulletInstance.transform.parent = null;
            GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerBehaviour>().PlaySound("plop");
            yield return new WaitForSeconds(1 / (weapon.fireRate * 2));
        }
        shooting = false;

    }
    private void Shoot1()
    {
        
    }
}
