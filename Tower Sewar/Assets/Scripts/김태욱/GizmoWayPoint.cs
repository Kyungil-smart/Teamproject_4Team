using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GizmoWayPoint : MonoBehaviour
{
    [Header("Scene 미 실행시 표현할 경로의 맵 index")]
    [SerializeField]
    int _index;

    [SerializeField]
    List<WayPoint> _wayPoints;


    private void OnDrawGizmos()
    {
        
        var paths = _wayPoints[_index].PathPoints;
        if (WaveManager._instance != null)
        {
            paths = _wayPoints[WaveManager._instance.currentMap].PathPoints;
        }
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
