using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage Data", menuName = "Scriptable Object/Stage Data", order = 0)]
public class StageData : ScriptableObject
{
    [SerializeField] List<WaveData> _waveDatas = new List<WaveData>();
}

[System.Serializable]
public class WaveData
{
    [Header("웨이브 몬스터 데이터")]
    [SerializeField] private string _monsterName;
    public string MonsterName => _monsterName;
    [SerializeField] private int _spawnAmount;
    public int SpawnAmount => _spawnAmount;
    [SerializeField] private int _spawnDelay;
    public int SpawnDelay => _spawnDelay;
    
    [Space(15)]
    [Header("웨이브 시간 데이터")]
    [SerializeField] private int _waveReadyTime;
    public int WaveReadyTime => _waveReadyTime;
    [SerializeField] private int _waveLimitTime;
    public int WaveLimitTime => _waveLimitTime;

    [Space(15)]
    [Header("웨이브 데이터")]
    [SerializeField] private int _clearGold;
    public int ClearGold => _clearGold;
}
