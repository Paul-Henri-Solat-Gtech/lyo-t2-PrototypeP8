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

    [SerializeField]
    private int prefabLimit;
    private int prefabLimitCount;

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
        if (prefabLimit > prefabLimitCount || prefabLimit == 0)
        {
            if (actualSpawnSec <= 0)
            {
                GameObject newSpawnedObject = Instantiate(prefabToSpawn, transform);
                prefabLimitCount++;
                actualSpawnSec = spawnPerSec;
            }
            else
            {
                actualSpawnSec -= 1 * Time.deltaTime;
            }
        }
    }
}
