using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanonTurret_Grade : Turret_Grade
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
        lv1.TowerAtt       = 1000.0f;
        lv1.TowerAttDelay  = 3f;
        lv1.TowerRange     = 1000.0f;
        _towerData.Add(lv1);

        GunTowerData lv2 = ScriptableObject.CreateInstance<GunTowerData>();
        lv2.TowerName      = "Lv2";
        lv2.TowerBuildCost = 10000000;
        lv2.TowerUpCost    = 100;
        lv2.TowerAtt       = 20.0f;
        lv2.TowerAttDelay  = 3f;
        lv2.TowerRange     = 1500.0f;
        _towerData.Add(lv2);
    }
}