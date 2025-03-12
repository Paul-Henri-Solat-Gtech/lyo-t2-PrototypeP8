using UnityEngine;

public class Apple : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            QuestManager.instance.CollectApple();
            Destroy(gameObject);
        }
    }
}
