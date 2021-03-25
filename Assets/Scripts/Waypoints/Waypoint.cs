using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Waypoint previousWaypoint;
    public Waypoint nextWaypoint;

    [Range(0.5f, 5.0f)]
    public float width = 1.0f;

    public List<Waypoint> branches;
    [Range(0.0f, 1.0f)]
    public float branchProbability = 0.5f;

    public Vector3 GetPosition()
    {
        Vector3 minBound = transform.position + transform.right * width / 2.0f;
        Vector3 maxBound = transform.position - transform.right * width / 2.0f;

        Vector3 position = Vector3.Lerp(minBound, maxBound, Random.Range(0.0f, 1.0f));

        return position;
    }
}
