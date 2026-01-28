using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
public class DataTest : MonoBehaviour
{
    [SerializeField] List<GunTowerData> GunTowers = new List<GunTowerData>();
   
    
    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PrintTowerInfo();
        }
    }
    

    private void Init()
    {
    }

    private void PrintTowerInfo()
    {
        for (int i = 0; i < GunTowers.Count; i++)
        {
            Debug.Log($"타워 이름 : {GunTowers[i].TowerName}");
            Debug.Log($"타워 건설 비용 : {GunTowers[i].TowerBuildCost}");
            Debug.Log($"타워 업그레이드 비용 : {GunTowers[i].TowerUpCost}");
            Debug.Log($"타워 공격 속도  : {GunTowers[i].TowerAtt}");
            Debug.Log($"타워 공격 범위 : {GunTowers[i].TowerRange}");
            Debug.Log("----------------------------------------------");
        }
    }
}
