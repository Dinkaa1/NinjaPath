using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elements : MonoBehaviour
{
    private float _speed;
    private float _endPos;
    void Update()
    {
        transform.Translate(Vector3.right * (Time.deltaTime * _speed));

        if (transform.position.x > _endPos)
        {
            Destroy(gameObject);
        }
    }

    public void StartFloating(float speed, float endPos)
    {
        _speed = speed;
        _endPos = endPos;
    }
}
