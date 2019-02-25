using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    List<Transform> waypoints;
    Transform targetWaypoint;
    int currentWaypointIndex = 1;
    float speed = 5f;

    void Start() {
        targetWaypoint = waypoints[currentWaypointIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if ( targetWaypoint && Vector3.Distance(transform.position, targetWaypoint.transform.position) < 0.2f ) {
            currentWaypointIndex++;

            if (currentWaypointIndex < waypoints.Count) {
                targetWaypoint = waypoints[currentWaypointIndex];
            } else {
                targetWaypoint = null;
                Destroy(gameObject);
            }
        }

        if (targetWaypoint) {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, step);
        }
    }

    public void SetWaypoints(List<Transform> waypoints) {
        this.waypoints = waypoints;
        targetWaypoint = waypoints[currentWaypointIndex];
    }

    public void SetMovementSpeed(float speed) {
        this.speed = speed;
    }
}
