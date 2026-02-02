using System.Collections.Generic;
using UnityEngine;

public class Turret_Grade : MonoBehaviour
{
    // protected로 선언해야 자식 클래스에서 이 변수를 그대로 사용할 수 있습니다.
    [SerializeField] protected List<GunTowerData> _towerData = new List<GunTowerData>();
    public virtual List<GunTowerData> TowerDatas => _towerData;

    [SerializeField] protected GameObject[] _towerPrefabs;
    public virtual GameObject[] TowerPrefabs => _towerPrefabs;

    protected virtual void Awake()
    {
    }
}