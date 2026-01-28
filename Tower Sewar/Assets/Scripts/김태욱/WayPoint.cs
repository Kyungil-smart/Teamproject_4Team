using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WayPoint", menuName = "Scriptable Object/WayPoint", order = 0)]
public class WayPoint : ScriptableObject
{
    [Header("¸Ê º° WayPoint Á¤º¸")]
    [SerializeField]
    private List<MapPathData> _maps;
    public List<MapPathData> Maps { get { return _maps; } }


}
[System.Serializable]
public class MapPathData
{
    [SerializeField]
    private string _mapName;
    public string MapName { get { return _mapName; } }

    [SerializeField]
    private List<Vector3> _pathPoints;
    public List<Vector3> PathPoints { get { return _pathPoints; } }
}



