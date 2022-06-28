using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public AudioSource hitsound;

    public PlayerMovement movement;

    private Vector3 _normal;

    public float PlayerHitForce = 6000f;

    private int CollideCounter = 0;

    public Vector3 Project(Vector3 forward)
    {
        return forward - Vector3.Dot(forward, _normal) * _normal;
    }

    public void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.name == "Obstacle")
        {
            hitsound.Play();
            CollideCounter++;
            if (CollideCounter == 1)
            {
                movement.rb.AddForce(0, 0, PlayerHitForce * Time.deltaTime, ForceMode.VelocityChange);
            }
            movement.enabled = false;
            FindObjectOfType<GameManager>().EndGame();
        }
        else
        {
            _normal = collisionInfo.contacts[0].normal;
        }
    }
}
