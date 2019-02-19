using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoundaryCheck : MonoBehaviour
{
    void Update()
    {
        if (CheckIfOutsideScreen()) {
            Destroy(gameObject);
        }
    }

    bool CheckIfOutsideScreen() {
        return transform.position.x < 0 || transform.position.x > 10f || transform.position.y < 0 || transform.position.y > 20f;
    }
}
