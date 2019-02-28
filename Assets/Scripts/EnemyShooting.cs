using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] float minShootInterfal = 0.2f;
    [SerializeField] float maxShootInterfal = 1.5f;
    [SerializeField] GameObject projectilePrefab;

    void Start() {
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot() {
        while(true) {
            Instantiate(projectilePrefab, transform.position, transform.rotation);

            yield return new WaitForSeconds(Random.Range(minShootInterfal, maxShootInterfal));
        }
    }
}
