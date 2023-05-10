using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformMovement_noParent : MonoBehaviour
{
    [SerializeField]
    private waypointFinder waypointPath;

    [SerializeField]
    private float speed;

    private int targetWaypointIndex;

    private Transform firstWaypoint;
    private Transform secondWaypoint;

    private float timeToWaypoint;
    private float elapsedTime;


    void Start()
    {
        TargetNextWaypoint();
    }

    void FixedUpdate()
    {
        elapsedTime += Time.deltaTime;

        float elapsedPercentage = elapsedTime / timeToWaypoint;
        elapsedPercentage = Mathf.SmoothStep(0, 1, elapsedPercentage);
        transform.position = Vector3.Lerp(firstWaypoint.position, secondWaypoint.position, elapsedPercentage);

        if (elapsedPercentage >= 1)
        {
            TargetNextWaypoint();
        }
    }
    private void TargetNextWaypoint()
    {
        firstWaypoint = waypointPath.getWaypoint(targetWaypointIndex);
        targetWaypointIndex = waypointPath.getNextWaypointIndex(targetWaypointIndex);
        secondWaypoint = waypointPath.getWaypoint(targetWaypointIndex);

        elapsedTime = 0;

        float distanceToWaypoint = Vector3.Distance(firstWaypoint.position, secondWaypoint.position);
        timeToWaypoint = distanceToWaypoint / speed;
    }
}