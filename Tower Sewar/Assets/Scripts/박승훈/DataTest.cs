using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
public class DataTest : MonoBehaviour
{
    [SerializeField] List<MonsterData> monsterDatas = new List<MonsterData>();
    private MonsterData bat;
    
    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PrintMonsterInfo();
        }
    }
    

    private void Init()
    {
    }

    private void PrintMonsterInfo()
    {
        for (int i = 0; i < monsterDatas.Count; i++)
        {
            Debug.Log($"몬스터 이름 : {monsterDatas[i].Name}");
            Debug.Log($"몬스터 체력 : {monsterDatas[i].Hp}");
            Debug.Log($"몬스터 이동속도 : {monsterDatas[i].MoveSpeed}");
            Debug.Log($"몬스터 처치 시 드랍 골드  : {monsterDatas[i].DropGold}");
            Debug.Log("----------------------------------------------");
        }
    }
}

public struct MonData
{
    private float _hp;
    public float Hp {get => _hp; set => _hp = value;}
    private float _moveSpeed;
    public float MoveSpeed {get => _moveSpeed; set => _moveSpeed = value;}
    private float _dropGold;
    public float DropGold {get => _dropGold; set => _dropGold = value;}
}
