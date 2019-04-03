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
    [SerializeField] float shootingSoundVolume = 0.5f;

    Coroutine firingCoroutine;
    AudioSource audioSource;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    void Update(){
        CheckInputForFiring();
    }

    void CheckInputForFiring() {
        if (Input.GetButtonDown("Fire1")) {
            firingCoroutine = StartCoroutine(ShootLaserAfterWait());
        }
        if (Input.GetButtonUp("Fire1")) {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator ShootLaserAfterWait() {
        while (Input.GetButton("Fire1")) {
            ShootLaser();
            yield return new WaitForSeconds(firingRate);
        }
    }

    void ShootLaser() {
        Instantiate(laserPrefab, new Vector3(transform.position.x, transform.position.y, 1), transform.rotation);

        audioSource.PlayOneShot(shootingSound, shootingSoundVolume);
    }
}
