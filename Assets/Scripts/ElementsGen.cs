using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementsGen : MonoBehaviour
{
    [SerializeField] GameObject[] elements;
    [SerializeField] float spawnInterval;
    [SerializeField] GameObject endPoint;
    public Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
        Prewarm();
        Invoke("SpawnElems", spawnInterval);
    }

    void SpawnElem(Vector3 start)
    {
        GameObject elem = Instantiate(elements[UnityEngine.Random.Range(0, elements.Length)]);
        float startY = UnityEngine.Random.Range(start.y - 2f, start.y + 0.5f);
        elem.transform.position = new Vector3(start.x, startY, start.z);
        float scale = UnityEngine.Random.Range(0.1f, 0.35f);
        elem.transform.localScale = new Vector2(scale, scale);
        float speed = UnityEngine.Random.Range(0.3f, 0.68f);
        elem.GetComponent<Elements>().StartFloating(speed, endPoint.transform.position.x);
        
    }

    void SpawnElems()
    {
        SpawnElem(startPos);
        Invoke("SpawnElems", spawnInterval);
    }

    void Prewarm()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 spawnPos = startPos + Vector3.right * (i * 2);
            SpawnElem(spawnPos);
        }
        
    }
}

