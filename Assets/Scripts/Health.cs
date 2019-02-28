using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float healthPoints = 500f;

    public void TakeDamage(float damageTaken) {
        healthPoints -= damageTaken;

        if (healthPoints <= 0) {
            Destroy(gameObject);
        }
    }
}
