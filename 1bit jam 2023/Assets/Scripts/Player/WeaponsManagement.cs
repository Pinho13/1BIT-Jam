using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public enum WeaponType {pistol, shotgun, Smg}
public class WeaponsManagement : MonoBehaviour
{

    [SerializeField] WeaponType weaponType;

    [Header("AllWeaponStats")]
    public WeaponStats[] weaponStats;


    [Header("CurrentWeapon")]
    public WeaponStats currentWeaponStats;

    

    [Header("Energy")]
    [SerializeField] float MaxEnergy;
    [SerializeField] float currentEnergy;


    [Header("References")]
    Animator gunAnim;
    float timeToFire;
    [SerializeField] Rigidbody2D rb;

    void Start()
    {
        gunAnim = GetComponent<Animator>();
    }


    void Update()
    {
        WeaponChosen();
        ShootDelay();
    }
    void WeaponChosen()
    {
        foreach(WeaponStats stat in weaponStats)
        {
            if(weaponType == stat.weaponType)
            {
                currentWeaponStats = stat;
            }
        }
    }

    void ShootDelay()
    {
        
        if(currentWeaponStats.fireRate == 0)
        {
            if(Input.GetMouseButtonDown(0))
            {
                gunAnim.SetBool("Shooting", true);
            }
        }
        else
        {
            if(Input.GetMouseButton(0) && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1/currentWeaponStats.fireRate;
                gunAnim.SetBool("Shooting", true);
            }else if(!Input.GetMouseButton(0))
            {
                gunAnim.SetBool("Shooting", false);
            }
        }
    }

    void shoot(int n)
    {
        for(int i = 0; i < currentWeaponStats.bulletPerShot; i++)
        {
            float currentSpread = Random.Range(-currentWeaponStats.spread, currentWeaponStats.spread);
            GameObject bullet = Instantiate(currentWeaponStats.bullet, currentWeaponStats.firepoints[n].position, Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + currentSpread));
            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * currentWeaponStats.bulletForce, ForceMode2D.Impulse);
        }
        rb.AddForce(-currentWeaponStats.firepoints[n].right * currentWeaponStats.recoil, ForceMode2D.Impulse);
    }

    void changeWeapons()
    {
        if(!gunAnim.GetCurrentAnimatorStateInfo(0).IsName(weaponType.ToString()))
        {
            gunAnim.Play(weaponType.ToString());
        }
    }
    void stopShooting()
    {
        gunAnim.SetBool("Shooting", false);
    }

}

[System.Serializable]
public class WeaponStats
{
    public WeaponType weaponType;
    public float bulletForce;
    public float fireRate;
    public float spread;
    public float recoil;
    public int bulletPerShot;
    public float Damage;
    public float EnergyConsumed;
    public GameObject bullet;

    public Transform[] firepoints;
}
