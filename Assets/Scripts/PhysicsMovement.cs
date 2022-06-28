using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private PlayerCollision _surfaceSlider;
    [SerializeField] private float _speed;

    public void PhysicsMove(Vector3 direction)
    {
        Vector3 directionAlongSurface = _surfaceSlider.Project(direction.normalized);
        Vector3 offset = directionAlongSurface * (_speed * Time.deltaTime);
        _rigidbody.MovePosition(_rigidbody.position + offset);
    }
}
