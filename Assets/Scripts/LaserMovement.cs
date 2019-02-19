using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMovement : MonoBehaviour
{
    [SerializeField, Range(300, 1000)] float speed = 350f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * speed);
    }

    void Update() {
        if (transform.position.y > 20f) {
            Destroy(gameObject);
        }
    }
}
