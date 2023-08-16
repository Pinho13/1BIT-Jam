using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    Rigidbody2D rb;
    WeaponsManagement weaponScript;
    [SerializeField] GameObject DamageText;
    Vector3 vector;
    [Header("Enemy Stats")]
    [SerializeField]float MaxHealth;
    public float currentHealth;
    [SerializeField] float knockbackVel;
    [Range(0, 1)]
    [SerializeField] int EnemyType;


    [Header("Death")]
    [SerializeField] GameObject corpse;
    [SerializeField] GameObject[] bloodDrops;
    [SerializeField] float deathSpeedMultiplier;
    [SerializeField] Vector2 dropSpeed;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        weaponScript = FindObjectOfType<WeaponsManagement>();
        currentHealth = MaxHealth;
    }

    void Update()
    {
        if(currentHealth <= 0)
        {
            GameObject corp = Instantiate(corpse, transform.position, transform.rotation);
            corp.GetComponent<Rigidbody2D>().AddForce(vector.normalized * deathSpeedMultiplier, ForceMode2D.Impulse);
            Destroy(gameObject);
        }
        if(currentHealth > MaxHealth)
        {
            currentHealth = MaxHealth;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Bullet"))
        {
            var bulletScript = other.gameObject.GetComponent<BulletScript>();
            vector = transform.position + ((transform.position - GameObject.FindGameObjectWithTag("Player").transform.position).normalized * bulletScript.knockback);
            if(EnemyType == bulletScript.bulletType)
            {
                currentHealth -= bulletScript.Damage;
                DamageText.transform.GetChild(0).GetComponent<TMP_Text>().text = "-" + bulletScript.Damage;
                Instantiate(DamageText, transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0), Quaternion.identity);
                transform.position = Vector3.MoveTowards(transform.position, vector, knockbackVel);
                weaponScript.currentEnergy += bulletScript.EnergyReturned;
                Instantiate(bulletScript.particles, transform.position, Quaternion.identity);
                Destroy(other.gameObject);
                GameObject drop = Instantiate(bloodDrops[Random.Range(0, bloodDrops.Length)], transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
                drop.GetComponent<Rigidbody2D>().AddForce(drop.transform.up * Random.Range(dropSpeed.x, dropSpeed.y), ForceMode2D.Impulse);
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
