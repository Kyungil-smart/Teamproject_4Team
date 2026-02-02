using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WayPoint", menuName = "Scriptable Object/WayPoint", order = 0)]
public class WayPoint : ScriptableObject
{
    [SerializeField]
    private List<Vector3> _pathPoints;
    public List<Vector3> PathPoints { get { return _pathPoints; } }
}




