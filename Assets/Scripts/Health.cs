using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Health : MonoBehaviour
{
    [SerializeField] float healthPoints = 500f;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] float explosionDuration = 2f;
    
    [Header("Sound effects")]
    [SerializeField] AudioClip hitSound;
    [SerializeField] float hitSoundVolume = 0.5f;
    [SerializeField] AudioClip dieSound;
    [SerializeField] float dieSoundVolume = 0.5f;

    [SerializeField] int points = 10;

    protected GameSession gameSession;

    public virtual void HandleDeath(){}

    public void Start() {
        gameSession = FindObjectOfType<GameSession>();
    }

    public virtual void TakeDamage(float damageTaken) {
        print("#### TAKE DAMAGE");

        print("VOOR: " + healthPoints);
        print("DamageTaken: " + damageTaken);
        
        healthPoints -= damageTaken;

        print("NA: " + healthPoints);

        if (healthPoints <= 0) {
            if (explosionPrefab != null) {
                var explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
                Destroy(explosion, explosionDuration);
            }

            if (dieSound) {
                AudioSource.PlayClipAtPoint(dieSound, Camera.main.transform.position, dieSoundVolume);
            }
            
            if (points > 0 && gameSession) {
                gameSession.AddPointsToScore(points);
            }

            HandleDeath();

            Destroy(gameObject);
        } else {
            print("AUH!");
            print("healthPoints left = " + healthPoints);

            if (hitSound) {
                AudioSource.PlayClipAtPoint(hitSound, Camera.main.transform.position, hitSoundVolume);
            }
        }
    }
}
