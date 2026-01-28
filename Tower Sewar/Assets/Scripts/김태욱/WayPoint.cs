using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WayPoint", menuName = "Scriptable Object/WayPoint", order = 0)]
public class WayPoint : ScriptableObject
{
    [Header("¸Ê º° WayPoint Á¤º¸")]
    [SerializeField]
    private List<WayPointList> _mapList;


}


[System.Serializable]
public class WayPointList
{
    [SerializeField]
    string _mapName;
    [SerializeField] 
    private List<Vector3> _wayPointList;
}

