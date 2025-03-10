using UnityEngine;

public class WallScript : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CubeGlouton"))
        {

        }
    }
}
