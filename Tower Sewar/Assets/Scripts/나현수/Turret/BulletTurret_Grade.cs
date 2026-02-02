using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class  BulletTurret_Grade : Turret_Grade
{
    protected override void Awake()
    {
        Init();
    }

    protected void Init()
    {
        _towerData.Clear();

        GunTowerData lv1 = ScriptableObject.CreateInstance<GunTowerData>();
        lv1.TowerName      = "Lv1";
        lv1.TowerBuildCost = 100;
        lv1.TowerUpCost    = 100;
        lv1.TowerAtt       = 10.0f;
        lv1.TowerAttDelay  = 0.5f;
        lv1.TowerRange     = 1000.0f;
        _towerData.Add(lv1);

        GunTowerData lv2 = ScriptableObject.CreateInstance<GunTowerData>();
        lv2.TowerName      = "Lv2";
        lv2.TowerBuildCost = 10000000;
        lv2.TowerUpCost    = 100;
        lv2.TowerAtt       = 20.0f;
        lv2.TowerAttDelay  = 0.2f;
        lv2.TowerRange     = 1500.0f;
        _towerData.Add(lv2);

        GunTowerData lv3 = ScriptableObject.CreateInstance<GunTowerData>();
        lv3.TowerName      = "Lv3";
        lv3.TowerBuildCost = 10000000;
        lv3.TowerUpCost    = 100;
        lv3.TowerAtt       = 20.0f;
        lv3.TowerAttDelay  = 0.1f;
        lv3.TowerRange     = 1500.0f;
        _towerData.Add(lv3);

        GunTowerData lv4 = ScriptableObject.CreateInstance<GunTowerData>();
        lv4.TowerName      = "Lv4";
        lv4.TowerBuildCost = 10000000;
        lv4.TowerUpCost    = 100;
        lv4.TowerAtt       = 20.0f;
        lv4.TowerAttDelay  = 0.1f;
        lv4.TowerRange     = 1500.0f;
        _towerData.Add(lv4);
    }
}