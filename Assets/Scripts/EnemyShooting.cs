using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EnemyShooting : MonoBehaviour
{
    [SerializeField] float minShootInterfal = 0.2f;
    [SerializeField] float maxShootInterfal = 1.5f;
    [SerializeField] GameObject projectilePrefab;

    [Header("Sound effects")]
    [SerializeField] AudioClip shootingSound;
    [SerializeField] float shootingSoundVolume = 1f;

    void Awake() {
        AudioClip loadedAudioClip = FindObjectOfType<AudioLoader>().GetAudioClipByName("enemy_shoot");

        if (loadedAudioClip) {
            shootingSound = loadedAudioClip;
        }
    }

    void Start() {
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot() {
        while(true) {
            yield return new WaitForSeconds(Random.Range(minShootInterfal, maxShootInterfal));

            Instantiate(projectilePrefab, transform.position, transform.rotation);
            
            if (shootingSound) {
                AudioSource.PlayClipAtPoint(shootingSound, Camera.main.transform.position, shootingSoundVolume);
            }
        }
    }
}
