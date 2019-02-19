using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] List<GameObject> waypoints;

    GameObject targetWaypoint;
    int currentWaypointIndex = 0;

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
            float speed = 5f;

            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.transform.position, step);
        }
    }
}
