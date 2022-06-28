using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_Movement : MonoBehaviour
{
    private Vector3 StartPosition;

    [SerializeField] private float frequency = 2f;

    [SerializeField] private float magnitude = 2f;

    [SerializeField] private float offset = 0f;

    // Start is called before the first frame update
    void Start()
    {
        StartPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = StartPosition + transform.right * Mathf.Sin(Time.time * frequency + offset) * magnitude;
    }
}