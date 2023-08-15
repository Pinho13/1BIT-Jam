using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
using EZCameraShake;
using TMPro;

public enum WeaponType {pistol, shotgun, Smg}
public class WeaponsManagement : MonoBehaviour
{

    [SerializeField] WeaponType weaponType;

    [Header("AllWeaponStats")]
    public WeaponStats[] weaponStats;


    [Header("CurrentWeapon")]
    public WeaponStats currentWeaponStats;

    

    [Header("Energy")]
    [SerializeField] int MaxEnergy;
    [SerializeField] int currentEnergy;
    int typeOfBullet;


    [Header("References")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject shootEffect;
    [SerializeField] TMP_Text energyText;
    Animator gunAnim;
    float timeToFire;

    void Start()
    {
        gunAnim = GetComponent<Animator>();
    }


    void Update()
    {
        WeaponChosen();
        ShootDelay();
        Displays();
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
                typeOfBullet = 0;
                gunAnim.SetBool("Shooting", true);
            }else if(Input.GetMouseButtonDown(1))
            {
                typeOfBullet = 1;
                gunAnim.SetBool("Shooting", true);
            }
        }
        else
        {
            if(Time.time > timeToFire)
            {
                if(Input.GetMouseButton(0))
                {
                    typeOfBullet = 0;
                    timeToFire = Time.time + 1/currentWeaponStats.fireRate;
                    gunAnim.SetBool("Shooting", true);
                }else if(Input.GetMouseButton(1))
                {
                    typeOfBullet = 1;
                    timeToFire = Time.time + 1/currentWeaponStats.fireRate;
                    gunAnim.SetBool("Shooting", true);
                }

                if(!Input.GetMouseButton(0) && !Input.GetMouseButton(1))
                {
                    gunAnim.SetBool("Shooting", false);
                }
            }
        }
    }

    void shoot(int n)
    {
        if(currentEnergy >= currentWeaponStats.EnergyConsumed)
        {
            currentEnergy -= currentWeaponStats.EnergyConsumed;
            for(int i = 0; i < currentWeaponStats.bulletPerShot; i++)
            {
                float currentSpread = Random.Range(-currentWeaponStats.spread, currentWeaponStats.spread);
                GameObject bullet = Instantiate(currentWeaponStats.bullet[typeOfBullet], currentWeaponStats.firepoints[n].position, Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + currentSpread));
                bullet.GetComponent<BulletScript>(). Damage = currentWeaponStats.Damage;
                bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * currentWeaponStats.bulletForce, ForceMode2D.Impulse);
                Instantiate(shootEffect, currentWeaponStats.firepoints[n].position, Quaternion.Euler(-transform.rotation.eulerAngles.z, 90, 0));
            }
            rb.AddForce(-currentWeaponStats.firepoints[n].right * currentWeaponStats.recoil, ForceMode2D.Impulse);
            CameraShaker.Instance.ShakeOnce(currentWeaponStats.Magnitude, currentWeaponStats.roughness, currentWeaponStats.fadeIn, currentWeaponStats.fadeOut);
        }
    }
    void Displays()
    {
        energyText.text = currentEnergy.ToString() + " / " + MaxEnergy.ToString();
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

    public void Shotgun()
    {
        weaponType = WeaponType.shotgun;
        changeWeapons();
    }

    public void pistol()
    {
        weaponType = WeaponType.pistol;
        changeWeapons();
    }

    public void Smg()
    {
        weaponType = WeaponType.Smg;
        changeWeapons();
    }

}

[System.Serializable]
public class WeaponStats
{
    public WeaponType weaponType;
    [Header("WeaponStats")]
    public float bulletForce;
    public float fireRate;
    public float spread;
    public float recoil;
    public int bulletPerShot;
    public float Damage;
    public int EnergyConsumed;

    [Header("References")]
    public GameObject[] bullet;
    public Transform[] firepoints;

    [Header("CameraShake")]
    public float Magnitude;
    public float roughness;
    public float fadeIn;
    public float fadeOut;
}
