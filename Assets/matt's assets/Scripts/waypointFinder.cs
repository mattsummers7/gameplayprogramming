using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypointFinder : MonoBehaviour
{
    public Transform getWaypoint(int waypointIndex)
    {
        return transform.GetChild(waypointIndex);
    }

    public int getNextWaypointIndex(int currentWayPointIndex)
    {
        int nextWaypointIndex = currentWayPointIndex + 1;

        if (nextWaypointIndex == transform.childCount)
        {
            nextWaypointIndex = 0;
        }

        return nextWaypointIndex;
    }
}
