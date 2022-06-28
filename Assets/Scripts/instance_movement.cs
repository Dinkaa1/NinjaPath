using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instance_movement : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rb;

    public float force = 100f;

    

    void Update()

    {
        rb.AddForce(transform.up * force * Time.deltaTime);
        transform.Rotate(Random.Range(-100, 100) * Time.deltaTime, Random.Range(-100, 100) * Time.deltaTime, Random.Range(0, 100) * Time.deltaTime);
    }
}
