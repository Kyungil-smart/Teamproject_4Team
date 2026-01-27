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
    int TowerBuildCost => _towerBuildCost;
    
    [Space(20)]
    [Header("1단계 타워 정보")]
    [SerializeField] private int _towerUpCost1;
    public int TowerUpCost1 => _towerUpCost1;
    [SerializeField] private float _towerAtt1;
    public float TowerAtt1 => _towerAtt1;
    [SerializeField] private float _towerAttSpd1;
    public float TowerAttSpd1 => _towerAttSpd1;
    [SerializeField] private float _towerRange1;
    public float TowerRange1 => _towerRange1;
    
    [Space(20)]
    [Header("2단계 타워 정보")]
    [SerializeField] private int _towerUpCost2;
    public int TowerUpCost2 => _towerUpCost2;
    [SerializeField] private float _towerAtt2;
    public float TowerAtt2 => _towerAtt2;
    [SerializeField] private float _towerAttSpd2;
    public float TowerAttSpd2 => _towerAttSpd2;
    [SerializeField] private float _towerRange2;
    public float TowerRange2 => _towerRange2;
    
    [Space(20)]
    [Header("3단계 타워 정보")]
    [SerializeField] private float _towerAtt3;
    public float TowerAtt3 => _towerAtt3;
    [SerializeField] private float _towerAttSpd3;
    public float TowerAttSpd3 => _towerAttSpd3;
    [SerializeField] private float _towerRange3;
    public float TowerRange3 => _towerRange3;
}
