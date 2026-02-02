using System.Collections.Generic;
using UnityEngine;

public class Turret_Grade : MonoBehaviour
{
    // protected로 선언해야 자식 클래스에서 이 변수를 그대로 사용할 수 있습니다.
    [SerializeField] public List<TowerData> _towerData = new List<TowerData>();
    

    [SerializeField] protected GameObject[] _towerPrefabs;
    public GameObject[] TowerPrefabs => _towerPrefabs;
}