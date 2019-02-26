using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int healthPoints = 10;
    [SerializeField] GameObject hitPrefab;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Laser")) {
            Hit();
        }
    }

    private void Hit() {
        /*var hitPosition = gameObject.transform.position;
        // move the hit effect to the foreground
        hitPosition.z = -5;

        var hitEffectObject = Instantiate(hitPrefab, hitPosition, Quaternion.identity);
        Destroy(hitEffectObject, 0.2f);*/

        healthPoints--;

        if (healthPoints <= 0) {
            // spawn rubble
            Destroy(gameObject);
        }
    }
}
