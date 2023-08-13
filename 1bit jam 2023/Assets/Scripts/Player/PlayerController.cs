using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    [SerializeField] float currentSpeed;

    [Header("Walking")]
    [SerializeField] float maxSpeed;
    [SerializeField] float speedGain;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Movement();
        faceMouse();
    }

    void Movement()
    {
        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        currentSpeed = rb.velocity.magnitude;

        if(currentSpeed < maxSpeed)
        {
            rb.AddForce((Vector2.up * verticalInput + Vector2.right * horizontalInput).normalized * speedGain * Time.deltaTime * 1000);
        }

        if(verticalInput != 0 || horizontalInput != 0)
        {
            anim.SetBool("Walking", true);
        } else
        {
            anim.SetBool("Walking", false);
        }
    }

    void faceMouse()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
    }
}
