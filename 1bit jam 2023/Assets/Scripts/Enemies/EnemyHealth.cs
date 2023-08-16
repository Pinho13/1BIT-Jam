using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    Rigidbody2D rb;
    WeaponsManagement weaponScript;
    [SerializeField] GameObject DamageText;
    [Header("Enemy Stats")]
    [SerializeField]float MaxHealth;
    public float currentHealth;
    [SerializeField] float knockbackVel;
    [Range(0, 1)]
    [SerializeField] int EnemyType;


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
            if(EnemyType == bulletScript.bulletType)
            {
                currentHealth -= bulletScript.Damage;
                DamageText.transform.GetChild(0).GetComponent<TMP_Text>().text = "-" + bulletScript.Damage;
                Instantiate(DamageText, transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0), Quaternion.identity);
                Vector2 vector = transform.position + ((transform.position - GameObject.FindGameObjectWithTag("Player").transform.position).normalized * bulletScript.knockback);
                transform.position = Vector3.MoveTowards(transform.position, vector, knockbackVel);
                weaponScript.currentEnergy += bulletScript.EnergyReturned;
                Instantiate(bulletScript.particles, transform.position, Quaternion.identity);
                Destroy(other.gameObject);
            }else
            {
                currentHealth += bulletScript.Damage;
                DamageText.transform.GetChild(0).GetComponent<TMP_Text>().text = "+" + bulletScript.Damage;
                var text = Instantiate(DamageText, transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0), Quaternion.identity);
                weaponScript.currentEnergy -= bulletScript.EnergyReturned;
                Instantiate(bulletScript.particles, transform.position, Quaternion.identity);
                Destroy(other.gameObject);
            }
        }
    }
}
