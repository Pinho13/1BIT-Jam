using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BloodDropScript : MonoBehaviour
{
    [SerializeField]GameObject[] splash;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if(rb.velocity.magnitude > 0.5f)
        {
            StartCoroutine(ExampleCoroutine());
        }
    }

    IEnumerator ExampleCoroutine()
    {
        //Print the time of when the function is first called.

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(0.3f);

        //After we have waited 5 seconds print the time again.
        Instantiate(splash[Random.Range(0, splash.Length)], transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Walls") || other.gameObject.CompareTag("Player"))
        {
            Instantiate(splash[Random.Range(0, splash.Length)], transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
            Destroy(gameObject);
        }
    }
}
