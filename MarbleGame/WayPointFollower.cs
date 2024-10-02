using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] wayPoints;
    private int currentWaypointIndex = 0;
    [SerializeField] private float speed = 2f;
    [SerializeField] private bool stopAtEnd;

    void Update()
    {
        // Check if we have reached the current waypoint and need to move on to the next one
        if (currentWaypointIndex < wayPoints.Length 
            && Vector3.Distance(wayPoints[currentWaypointIndex].transform.position, transform.position) 
            <= speed * Time.deltaTime)
            {
            // Move on to the next waypoint
            currentWaypointIndex++;

            // If we've reached the end of the path, either stop or loop back to the beginning
            if (currentWaypointIndex >= wayPoints.Length)
            {
                if (stopAtEnd)
                {
                    return;
                }
                currentWaypointIndex = 0;
            }
       }

        // Move towards the current waypoint
        if (currentWaypointIndex < wayPoints.Length)
        {
            transform.position = Vector3.MoveTowards(transform.position, wayPoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
        }
      
    }
}
