using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "weapon", menuName = "ScriptableObjects/weapon", order = 1)]
public class weaponScriptableObject : ScriptableObject
{
    public float shotPower;
    public float fireRate;
    public float bloom;
    public float reloadTime;
    public int magazineSize;
    public int burstSize;
    public GameObject bullet;
}
