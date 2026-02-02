using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage Data", menuName = "Scriptable Object/Stage Data", order = 0)]
public class StageData : ScriptableObject
{
    [SerializeField] List<WaveData> _waveDatas = new List<WaveData>();
    public List<WaveData> WaveDatas => _waveDatas;
}

[System.Serializable]
public class WaveData
{
    [Header("몬스터 데이터")]
    [SerializeField] private MonsterData _monsterData;
    public MonsterData MonsterData => _monsterData;

    [Header("몹 스폰 조절")]
    [SerializeField] private int _spawnAmount;
    public int SpawnAmount => _spawnAmount;
    [SerializeField] private int _spawnDelay;
    public int SpawnDelay => _spawnDelay;
    [SerializeField] bool spawn_Left;
    public bool Spawn_Left => spawn_Left;
    [SerializeField] bool spawn_Right;
    public bool Spawn_Right => spawn_Right;

    [Header("웨이브 시간")]
    [SerializeField] private int _waveReadyTime;
    public int WaveReadyTime => _waveReadyTime;
    [SerializeField] private int _waveLimitTime;
    public int WaveLimitTime => _waveLimitTime;

    [Header("웨이브 클리어 골드")]
    [SerializeField] private int _clearGold;
    public int ClearGold => _clearGold;
}
