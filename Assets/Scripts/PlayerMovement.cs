using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField, Range(5f, 12f)] float horizontalSpeed = 7f;
    [SerializeField, Range(5f, 12f)] float verticalSpeed = 7f;

    float LEFT_BOUNDARY;
    float TOP_BOUNDARY;
    float RIGHT_BOUNDARY;
    float BOTTOM_BOUNDARY;

    void Awake() {
        Camera camera = Camera.main;

        var p1 = gameObject.transform.TransformPoint(0, 0, 0);
        var p2 = gameObject.transform.TransformPoint(1, 1, 0);
        var playerWidth = (p2.x - p1.x) / 2;
        var playerHeight = (p2.y - p1.y) / 2;

        LEFT_BOUNDARY = camera.ViewportToWorldPoint(new Vector2(0,0)).x + playerWidth;
        TOP_BOUNDARY = camera.ViewportToWorldPoint(new Vector2(0,1)).y - playerHeight;
        RIGHT_BOUNDARY = camera.ViewportToWorldPoint(new Vector2(1,0)).x - playerWidth;
        BOTTOM_BOUNDARY = camera.ViewportToWorldPoint(new Vector2(0,0)).y + playerHeight;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
    }

    void Move() {
        float newX = transform.position.x + Input.GetAxis("Horizontal") * Time.deltaTime * horizontalSpeed;
        float newY = transform.position.y + Input.GetAxis("Vertical") * Time.deltaTime * verticalSpeed;

        float horizontalMovement = Mathf.Clamp(newX, LEFT_BOUNDARY, RIGHT_BOUNDARY);
        float verticalMovement = Mathf.Clamp(newY, BOTTOM_BOUNDARY, TOP_BOUNDARY);

        transform.position = new Vector2(horizontalMovement, verticalMovement);
    }

    void Rotate() {
        float smooth = 5.0f;

        // Smoothly tilts a transform towards a target rotation.
        float tiltAroundX = Input.GetAxis("Vertical") * 30f;
        float tiltAroundY = Input.GetAxis("Horizontal") * -40f;

        Quaternion target = Quaternion.Euler(tiltAroundX, tiltAroundY, 0);

        // Dampen towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * smooth);
    }
}
