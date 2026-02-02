using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GizmoWayPoint : MonoBehaviour
{
    [SerializeField]
    WayPoint _wayPoint1;
    [SerializeField]
    WayPoint _wayPoint2;


    private void OnDrawGizmos()
    {
        if (_wayPoint1 == null) return;
        var paths = _wayPoint1.PathPoints;
        for(int i=0; i< paths.Count -1; i++)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(paths[i], paths[i+1]);

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(paths[i], 0.3f);
        }
        Gizmos.DrawSphere(paths[paths.Count - 1], 0.3f);

        if (_wayPoint2 == null) return;
        paths = _wayPoint2.PathPoints;

        for (int i = 0; i < paths.Count - 1; i++)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(paths[i], paths[i + 1]);

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(paths[i], 0.3f);
        }
        Gizmos.DrawSphere(paths[paths.Count - 1], 0.3f);


    }

}
