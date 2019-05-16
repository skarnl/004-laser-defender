using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    [SerializeField] Image damageImage;
    [SerializeField] float flashSpeed = 5f;
    [SerializeField] Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    [SerializeField] Level levelManager;

    bool damaged = false;

    public override void HandleDeath() {
        FindObjectOfType<Level>().LoadGameOver();
    }

    void Awake() {
        base.Awake();

        AudioClip loadedDieAudioClip = FindObjectOfType<AudioLoader>().GetAudioClipByName("player_die");

        if (loadedDieAudioClip) {
            dieSound = loadedDieAudioClip;
        }

        AudioClip loadedHitAudioClip = FindObjectOfType<AudioLoader>().GetAudioClipByName("player_hit");

        if (loadedHitAudioClip) {
            hitSound = loadedHitAudioClip;
        }
    }

    public void Update() {
        if(damaged && damageImage) {
            // ... set the colour of the damageImage to the flash colour.
            damageImage.color = flashColour;
        } else {
            // ... transition the colour back to clear.
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        // Reset the damaged flag.
        damaged = false;
    }

    public override void TakeDamage(float damageTaken) {
        damaged = true;

        gameSession.SubstractHealth((int) damageTaken);

        base.TakeDamage(damageTaken);
    }
}
