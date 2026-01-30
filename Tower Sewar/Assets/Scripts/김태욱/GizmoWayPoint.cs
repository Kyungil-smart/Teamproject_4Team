using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GizmoWayPoint : MonoBehaviour
{
    [SerializeField]
    WayPoint _wayPoint;

    [SerializeField]
    int _mapIndex;

    private void Awake()
    {
        _mapIndex = 0;
    }

    private void OnDrawGizmos()
    {
        var paths = _wayPoint.Maps[_mapIndex].PathPoints;
        if (paths == null) return;

        for(int i=0; i< paths.Count -1; i++)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(paths[i], paths[i+1]);

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(paths[i], 0.3f);
        }
        Gizmos.DrawSphere(paths[paths.Count - 1], 0.3f);
    }

}
