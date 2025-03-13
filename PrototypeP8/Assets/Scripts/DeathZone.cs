using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CubeGlouton"))
        {
            if (GameObject.Find("SpawnPlayer") != null)
            {
                other.transform.position = GameObject.Find("SpawnPlayer").transform.position;
            }
            else
            {
                other.transform.position = new Vector3(0, 0, 0);
            }

        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}