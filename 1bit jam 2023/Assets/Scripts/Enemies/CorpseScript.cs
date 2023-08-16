using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseScript : MonoBehaviour
{
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Walls"))
        {
            rb.velocity = new Vector2(0, 0);
        }
    }
}
