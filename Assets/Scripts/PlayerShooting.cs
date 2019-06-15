using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerShooting : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float firingRate = 3f;
    
    [Header("Sound effects")]
    [SerializeField] AudioClip shootingSound;
    float shootingSoundVolume = 1f;

    Coroutine firingCoroutine;
    AudioSource audioSource;
    PlayerMovement playerMovement;

    bool shootingLasers = false;

    void Awake() {
        AudioClip loadedAudioClip = FindObjectOfType<AudioLoader>().GetAudioClipByName("player_shoot");

        if (loadedAudioClip) {
            shootingSound = loadedAudioClip;
        }
    }

    void Start() {
        audioSource = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update(){
        CheckInputForFiring();
    }

    void CheckInputForFiring() {
        //if (Input.GetButtonDown("Fire1")) {
        if (playerMovement.isMoving && shootingLasers == false) {
            firingCoroutine = StartCoroutine(ShootLaserAfterWait());

            shootingLasers = true;
        } 
        
        if (!playerMovement.isMoving && shootingLasers == true) {
            StopCoroutine(firingCoroutine);

            shootingLasers = false;
        }
    }

    IEnumerator ShootLaserAfterWait() {
        while (true) {
            ShootLaser();
            yield return new WaitForSeconds(firingRate);
        }
    }

    void ShootLaser() {
        Instantiate(laserPrefab, transform.position, transform.rotation);

        audioSource.PlayOneShot(shootingSound, shootingSoundVolume);
    }
}
