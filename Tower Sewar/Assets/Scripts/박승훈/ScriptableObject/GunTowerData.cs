using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
[CreateAssetMenu(fileName = "Gun Tower Data", menuName = "Scriptable Object/Tower/Gun Tower Data", order = 0)]
public class GunTowerData : ScriptableObject
{
    [Header("기본 생성 정보")]
    [SerializeField] private string _towerName;
    public string TowerName => _towerName;
    [SerializeField] private int _towerBuildCost;
    public int TowerBuildCost => _towerBuildCost;
    
    [Space(20)]
    [Header("타워 스탯")]
    [SerializeField] private int _towerUpCost;
    public int TowerUpCost => _towerUpCost;
    [SerializeField] private float _towerAtt;
    public float TowerAtt => _towerAtt;
    [SerializeField] private float _towerAttDelay;
    public float TowerAttDelay => _towerAttDelay;
    [SerializeField] private float _towerRange;
    public float TowerRange => _towerRange;
}
