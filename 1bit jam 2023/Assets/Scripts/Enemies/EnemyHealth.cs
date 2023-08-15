using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    Rigidbody2D rb;
    WeaponsManagement weaponScript;
    [Header("Enemy Stats")]
    [SerializeField]float MaxHealth;
    public float currentHealth;
    [SerializeField] float knockbackVel;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        weaponScript = FindObjectOfType<WeaponsManagement>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Bullet"))
        {
            var bulletScript = other.gameObject.GetComponent<BulletScript>();
            currentHealth -= bulletScript.Damage;
            transform.position = Vector2.Lerp(transform.position, transform.position + ((transform.position - GameObject.FindGameObjectWithTag("Player").transform.position).normalized * bulletScript.knockback), knockbackVel);
            weaponScript.currentEnergy += bulletScript.EnergyReturned;
            Instantiate(bulletScript.particles, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
    }
}
