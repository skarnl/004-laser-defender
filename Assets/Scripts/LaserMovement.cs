using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum DIRECTION { Up, Down };

public class LaserMovement : MonoBehaviour
{
    [SerializeField, Range(300, 1000)] float speed = 350f;
    [SerializeField] DIRECTION direction = DIRECTION.Up;

    // Start is called before the first frame update
    void Start() {
        GetComponent<Rigidbody2D>().velocity = ((direction == DIRECTION.Up ? Vector2.up : Vector2.down) * speed / 100);
    }
}
