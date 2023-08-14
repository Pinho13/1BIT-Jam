using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    [SerializeField] GameObject particles;
    public float Damage;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Walls"))
        {
            Instantiate(particles, transform.position, Quaternion.Euler(transform.rotation.eulerAngles.z, 90, 0));
            Destroy(this.gameObject);
        }
    }
}
