using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public AudioSource jumpsound;

    public bool isgrounded = false;

    public Rigidbody rb;

    public float sidewaysForce = 600f;

    public float JumpForce = 200f;


    [SerializeField] PhysicsMovement _movement;
    // Start is called before the first frame update

    // Update is called once per frame

    void Start()
    {

    }
    void FixedUpdate()
    {
        _movement.PhysicsMove(new Vector3(0, 0, 1));

        

        if (Input.GetKey("w") && isgrounded)
        {
            jumpsound.Play();
            rb.AddForce(new Vector3(0, 1, 0) * JumpForce, ForceMode.Impulse);
        }

        if (Input.GetKey("d"))
        {
            rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if(Input.GetKey("a"))
        {
            rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (rb.position.y < - 1f)
        {
            FindObjectOfType<GameManager>().EndGame();
        }

        if (isgrounded == false)
        {
            transform.Rotate(200f * Time.deltaTime, 0, 0, Space.World);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Floor")
        {
            isgrounded = true;
            Debug.Log("isg");
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.collider.name == "Floor")
        {
            isgrounded = false;
            Debug.Log("notg");
        }
    }
}
