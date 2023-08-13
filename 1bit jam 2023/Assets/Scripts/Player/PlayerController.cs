using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float currentSpeed;

    [Header("Walking")]
    [SerializeField] float maxSpeed;
    [SerializeField] float speedGain;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        currentSpeed = rb.velocity.magnitude;

        if(currentSpeed < maxSpeed)
        {
            rb.AddForce((Vector2.up * verticalInput + Vector2.right * horizontalInput).normalized * speedGain * Time.deltaTime * 1000);
        }
    }
}
