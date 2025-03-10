using UnityEngine;

public class Nourriture : MonoBehaviour
{
    private Vector3 positionMin = new Vector3(-2.5f, 0.5f, -2.5f); 
    private Vector3 positionMax = new Vector3(2.5f, 0.5f, 2.5f);   

    public void Respawn()
    {
        float x = Random.Range(positionMin.x, positionMax.x);
        float z = Random.Range(positionMin.z, positionMax.z);
        transform.position = new Vector3(x, positionMin.y, z); 
    }

    private void Start()
    {
        Respawn(); 
    }
}

