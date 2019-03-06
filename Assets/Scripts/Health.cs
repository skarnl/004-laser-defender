using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float healthPoints = 500f;
    [SerializeField] GameObject explosionPrefab;

    public void TakeDamage(float damageTaken) {
        healthPoints -= damageTaken;

        if (healthPoints <= 0) {
            if (explosionPrefab != null) {
                Instantiate(explosionPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}
