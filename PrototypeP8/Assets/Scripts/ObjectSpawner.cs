using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabToSpawn;
    
    [SerializeField]
    private float spawnPerSec;
    private float actualSpawnSec;

    // Start is called before the first frame update
    void Start()
    {
        actualSpawnSec = spawnPerSec;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        SpawnLoop();
    }

    private void SpawnLoop()
    {
        if (actualSpawnSec <= 0)
        {
            GameObject newSpawnedObject = Instantiate(prefabToSpawn, transform);
            actualSpawnSec = spawnPerSec;
        }
        else
        {
            actualSpawnSec -= 1 * Time.deltaTime; 
        }
    }
}
