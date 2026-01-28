using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Turret_Grade : MonoBehaviour
{
    [SerializeField] List<GunTowerData> _towerData = new List<GunTowerData>(4);
    public List<GunTowerData> TowerDatas { get => _towerData; set => _towerData = value; }

    [SerializeField] private GameObject[] _towerPrefabs; // 등급별 타워 외형
    public GameObject[] TowerPrefabs => _towerPrefabs;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (_towerData.Count > 0) return;

        _towerData.Clear();

        GunTowerData lv1 = ScriptableObject.CreateInstance<GunTowerData>();
        lv1.TowerName      = "Lv1";
        lv1.TowerBuildCost = 100;
        lv1.TowerUpCost    = 100;
        lv1.TowerAtt       = 10.0f;
        lv1.TowerAttDelay  = 1.0f;
        lv1.TowerRange     = 1000.0f;
        _towerData.Add(lv1);

        GunTowerData lv2 = ScriptableObject.CreateInstance<GunTowerData>();
        lv2.TowerName      = "Lv2";
        lv2.TowerBuildCost = 10000000;
        lv2.TowerUpCost    = 100;
        lv2.TowerAtt       = 20.0f;
        lv2.TowerAttDelay  = 1.0f;
        lv2.TowerRange     = 1500.0f;
        _towerData.Add(lv2);
    }
}
