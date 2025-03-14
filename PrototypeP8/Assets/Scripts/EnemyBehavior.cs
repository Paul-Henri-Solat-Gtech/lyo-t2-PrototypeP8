using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float health = 100f;
    public float damageMultiplier = 10f;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ObjectToAbsorb"))
        {
            ObjectProperty objectProperty = collision.gameObject.GetComponent<ObjectProperty>();
            if (objectProperty != null)
            {
                float objectSize = objectProperty.GetObjectSize();
                float damage = objectSize * damageMultiplier;
                TakeDamage(damage);
            }
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}

