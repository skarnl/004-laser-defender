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
    [SerializeField] protected AudioClip hitSound;
    [SerializeField] float hitSoundVolume = 0.5f;
    [SerializeField] protected AudioClip dieSound;
    [SerializeField] float dieSoundVolume = 0.5f;

    [SerializeField] int points = 10;

    protected GameSession gameSession;

    public virtual void HandleDeath(){}

    public void Awake() {
        AudioClip loadedHitAudioClip = FindObjectOfType<AudioLoader>().GetAudioClipByName("enemy_hit");

        if (loadedHitAudioClip) {
            hitSound = loadedHitAudioClip;
        }

        AudioClip loadedDieAudioClip = FindObjectOfType<AudioLoader>().GetAudioClipByName("enemy_die");

        if (loadedDieAudioClip) {
            dieSound = loadedDieAudioClip;
        }
    }

    public void Start() {
        gameSession = FindObjectOfType<GameSession>();
    }

    public virtual void TakeDamage(float damageTaken) {
        healthPoints -= damageTaken;

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
            if (hitSound) {
                AudioSource.PlayClipAtPoint(hitSound, Camera.main.transform.position, hitSoundVolume);
            }
        }
    }
}
