using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryCheck : MonoBehaviour
{
    void Update()
    {
        if (CheckIfOutsideScreen()) {
            Destroy(gameObject);
        }
    }

    bool CheckIfOutsideScreen() {
        return transform.position.x < 0 || transform.position.x > 20f || transform.position.y < 0 || transform.position.y > 20f;
    }
}
