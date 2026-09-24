using System.Collections.Generic;
using UnityEngine;

public class WaypointsMgr : MonoBehaviour
{
    public GameObject drone;
    private int nWaypoint;
    private List<Transform> waypoints = new();
    public float moveSpeed = 1f;
    public float rotateSpeed = 2f;
    float arrivalDistance = 0.01f;

    void Start()
    {
        LoadWaypoints();
    }

    void Update()
    {
        if (waypoints.Count == 0) return;
        Transform targetWaypoint = waypoints[nWaypoint];
        drone.transform.position = Vector3.MoveTowards(drone.transform.position, targetWaypoint.position, moveSpeed * Time.deltaTime);
        Vector3 direction = targetWaypoint.position - drone.transform.position;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            drone.transform.rotation = Quaternion.Slerp(drone.transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }
        if (Vector3.Distance(drone.transform.position, targetWaypoint.position) < arrivalDistance)
        {
            nWaypoint = (nWaypoint + 1) % waypoints.Count;
        }
    }

    void LoadWaypoints()
    {
        foreach (Transform t in transform)
        {
            waypoints.Add(t);            
        }
    }
}


