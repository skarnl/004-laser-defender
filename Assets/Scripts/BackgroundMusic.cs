using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : Singleton
{
    public override void Awake() {
        base.Awake();

        AudioSource audioSource = GetComponent<AudioSource>();
        
        AudioClip audioClip = FindObjectOfType<AudioLoader>().GetAudioClipByName("background_music.ogg");

        if (audioClip) {
            audioSource.clip = audioClip;
        } else {
            Debug.Log("No suitable audioclip found");
        }

        StartCoroutine(FadeIn(audioSource, 6.0f));
    }

    private IEnumerator FadeIn(AudioSource audioSource, float FadeTime) {
        audioSource.Play();
        audioSource.volume = 0f;
        while (audioSource.volume < 1) {
            audioSource.volume += Time.deltaTime / FadeTime;
            yield return null;
        }
    }
}
