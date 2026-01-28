using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Turret_Grade : MonoBehaviour
{
    [SerializeField] List<GunTowerData> _towerData = new List<GunTowerData>(4);
    public List<GunTowerData> TowerDatas => _towerData;
    private int _curGrade = 0;
    private int _maxGrade = 3;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        // if (Console.ReadKey(KeyCode.V))
        // {
        //     UpgradeTurret();
        // }
    }

    private void UpgradeTurret()
    {
        _curGrade++;
    }

    private void Init()
    {
        _towerData[0].TowerName      = "Lv1";
        _towerData[0].TowerBuildCost = 100;
        _towerData[0].TowerUpCost    = 100;
        _towerData[0].TowerAtt       = 10.0f;
        _towerData[0].TowerAttDelay  = 1.0f;
        _towerData[0].TowerRange     = 1000.0f;

        _towerData[1].TowerName = "Lv2";
        _towerData[1].TowerBuildCost = 10000000;
        _towerData[1].TowerUpCost = 100;
        _towerData[1].TowerAtt = 20.0f;
        _towerData[1].TowerAttDelay = 1.0f;
        _towerData[1].TowerRange = 1500.0f;
    }
}
