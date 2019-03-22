using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 100;

    void OnTriggerEnter2D(Collider2D other)
    {
        print(gameObject.tag);
        print(other.tag);

        // bail if the collision is the wrong way (enemy with laser)
        if ( gameObject.tag == "Enemy" && other.tag == "Laser" ) {
            return;
        }

        var health = other.gameObject.GetComponent<Health>();
        
        if (health) {
            health.TakeDamage(damage);
        }

        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
