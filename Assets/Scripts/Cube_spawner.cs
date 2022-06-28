using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_spawner : MonoBehaviour
{
    private float nextActionTime = 0.0f;
    public float period = 0.1f;
    public GameObject cubePrefab;
    void Update()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime += period;
            GameObject Instance1;
            GameObject Instance2;
            GameObject Instance3;
            GameObject Instance4;
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-10, 10), -9, Random.Range(7, 8));
            Instance1 = Instantiate(cubePrefab, randomSpawnPosition, Quaternion.identity);
            Instance2 = Instantiate(cubePrefab, randomSpawnPosition, Quaternion.identity);
            Instance3 = Instantiate(cubePrefab, randomSpawnPosition, Quaternion.identity);
            Instance4 = Instantiate(cubePrefab, randomSpawnPosition, Quaternion.identity);
        }
    }
}
