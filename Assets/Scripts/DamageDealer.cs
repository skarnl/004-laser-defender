using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 100;
    [SerializeField] List<string> ignoredTags;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (ignoredTags.Contains(other.tag)) {
            return;
        }

        var health = other.gameObject.GetComponent<Health>();
        
        if (health) {
            health.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
