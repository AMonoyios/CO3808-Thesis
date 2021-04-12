using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNavigator : MonoBehaviour
{
    public AiNavigationController controller;
    public Waypoint currentWaypoint;
    [Range(0.0f, 1.0f)]
    public float directionSwapChance = 0.0f;
    GameObject parentWaypoint;

    bool navigateToNextWaypoint;

    private void Awake()
    {
        navigateToNextWaypoint = (Random.Range(0, 2) == 1);

        controller = GetComponent<AiNavigationController>();

        parentWaypoint = GameObject.Find(controller.navigationName);
    }

    // Start is called before the first frame update
    void Start()
    {
        Transform[] waypointsTransforms = parentWaypoint.GetComponentsInChildren<Transform>();
        this.transform.position = waypointsTransforms[Random.Range(0, waypointsTransforms.Length)].position;

        Waypoint[] waypoints = parentWaypoint.GetComponentsInChildren<Waypoint>();
        currentWaypoint = waypoints[Random.Range(0, waypoints.Length)];

        controller.SetDestination(currentWaypoint.GetPosition());
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.reachedDesination)
        {
            bool willBranch = false;
            
            if (currentWaypoint.branches != null && currentWaypoint.branches.Count > 0)
            {
                willBranch = Random.Range(0.0f, 1.0f) <= currentWaypoint.branchProbability ? true : false;
            }
            
            if (willBranch)
            {
                currentWaypoint = currentWaypoint.branches[Random.Range(0, currentWaypoint.branches.Count - 1)];
            
                if (directionSwapChance > 0.0f)
                {
                    navigateToNextWaypoint = Random.Range(0.0f, 1.0f) <= directionSwapChance ? true : false;
                }
            }
            else
            {
                if (navigateToNextWaypoint)
                {
                    if (currentWaypoint.nextWaypoint != null)
                    {
                        currentWaypoint = currentWaypoint.nextWaypoint;
                    }
                    else
                    {
                        currentWaypoint = currentWaypoint.previousWaypoint;
                        navigateToNextWaypoint = false;
                    }
                }
                else
                {
                    if (currentWaypoint.previousWaypoint != null)
                    {
                        currentWaypoint = currentWaypoint.previousWaypoint;
                    }
                    else
                    {
                        currentWaypoint = currentWaypoint.nextWaypoint;
                        navigateToNextWaypoint = true;
                    }
                }
            }

            controller.SetDestination(currentWaypoint.GetPosition());
        }
    }
}
