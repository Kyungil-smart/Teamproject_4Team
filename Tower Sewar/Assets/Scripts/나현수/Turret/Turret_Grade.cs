using System.Collections.Generic;
using UnityEngine;

public class Turret_Grade : MonoBehaviour
{
    [SerializeField] public List<TowerData> _towerData = new List<TowerData>();
    
    [SerializeField] protected GameObject[] _towerPrefabs;
    public GameObject[] TowerPrefabs => _towerPrefabs;
}