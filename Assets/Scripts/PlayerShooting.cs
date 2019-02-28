using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float firingRate = 3f;

    Coroutine firingCoroutine;

    void Update()
    {
        CheckInputForFiring();
    }

    void CheckInputForFiring() {
        print("CheckInputForFiring");

        if (Input.GetButtonDown("Fire1")) {
            firingCoroutine = StartCoroutine(ShootLaserAfterWait());
        }
        if (Input.GetButtonUp("Fire1")) {
            StopCoroutine(firingCoroutine);
        }
    }

    void ShootLaser() {
        Instantiate(laserPrefab, transform.position, transform.rotation);
    }

    IEnumerator ShootLaserAfterWait() {
        print("ShootLaserAfterWait");

        while (Input.GetButton("Fire1")) {
            ShootLaser();
            yield return new WaitForSeconds(firingRate);
        }
    }
}
