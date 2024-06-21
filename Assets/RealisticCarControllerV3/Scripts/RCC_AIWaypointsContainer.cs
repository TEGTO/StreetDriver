//----------------------------------------------
//            Realistic Car Controller
//
// Copyright © 2014 - 2022 BoneCracker Games
// http://www.bonecrackergames.com
// Buğra Özdoğanlar
//
//----------------------------------------------

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Used for holding a list for waypoints, and drawing gizmos for all of them.
/// </summary>
[AddComponentMenu("BoneCracker Games/Realistic Car Controller/AI/RCC AI Waypoints Container")]
public class RCC_AIWaypointsContainer : MonoBehaviour
{
    public static RCC_AIWaypointsContainer Instance { get; private set; }

    public List<RCC_Waypoint> waypoints = new List<RCC_Waypoint>();

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    void Start()
    {

        RCC_Waypoint[] childWaypoints = GetComponentsInChildren<RCC_Waypoint>();
        if (childWaypoints.Length > 0)
            waypoints = childWaypoints.ToList();

    }

    // Used for drawing gizmos on Editor.
    void OnDrawGizmos()
    {

        if (waypoints == null)
            return;

        for (int i = 0; i < waypoints.Count; i++)
        {

            if (waypoints[i] == null)
                return;

            Gizmos.color = new Color(0.0f, 1.0f, 1.0f, 0.3f);
            Gizmos.DrawSphere(waypoints[i].transform.position, 2);
            Gizmos.DrawWireSphere(waypoints[i].transform.position, 20f);

            if (i < waypoints.Count - 1)
            {

                if (waypoints[i] && waypoints[i + 1])
                {

                    if (waypoints.Count > 0)
                    {

                        Gizmos.color = Color.green;

                        if (i < waypoints.Count - 1)
                            Gizmos.DrawLine(waypoints[i].transform.position, waypoints[i + 1].transform.position);
                        if (i < waypoints.Count - 2)
                            Gizmos.DrawLine(waypoints[waypoints.Count - 1].transform.position, waypoints[0].transform.position);

                    }

                }

            }

        }

    }

}
