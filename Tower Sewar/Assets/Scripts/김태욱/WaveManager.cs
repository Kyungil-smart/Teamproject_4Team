using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    public static WaveManager _instance;

    //스테이지 정보
    [SerializeField]
    StageData _stageData;

    [SerializeField]
    WayPoint _wayPoint1;
    [SerializeField]
    WayPoint _wayPoint2;

    //현재 Wave 단계
    private int _wave;
    public int Wave { get { return _wave; } }

    //Wave Timer
    private float _waveTimer;
    public float WaveTimer { get { return _waveTimer; } }

    //준비(휴식)시간인지?
    private bool _isReadyTime;

    public bool IsReadyTime
    {
        get { return _isReadyTime; }
        set
        {
            _isReadyTime = value;
            if (_isReadyTime)
            {
                Debug.Log("Wait");
                Stage_Sound_Manager.instance.SettingSound("Waiting");
            }
            else
            {
                Debug.Log("Start");
                Stage_Sound_Manager.instance.SettingSound("Wave");
            }
        }
    }

    //스폰 주기를 조절하기위한 변수
    private float _spawnCoolTime;
    //현재 wave에서 스폰한 몹 개수
    private int _numsOfSpawnMonster;

    //맵에 스폰된 몬스터의 총 개수
    public int NumsOfMonsters
    { get { return MonsterSpawner.Instance.MonsterCount; } }
     
    void Awake()
    {
        _instance = this;
        Init();
    }

    void Start()
    {
        Debug.Log($"[{Wave}]단계 [준비]시간입니다. ({_waveTimer:00}초)");
    }

    void Update()
    {
        //웨이브 시간 차감
        _waveTimer -= Time.deltaTime;

        //타이머 로그
        //if(_isReadyTime)
        //    Debug.Log($"[{_wave}단계] 준비시간 {_waveTimer:000}");
        //else Debug.Log($"[{_wave}단계] 전투시간 {_waveTimer:000}");

        //타이머가 끝나면 상태변경(준비시간 or 웨이브시간)
        if (_waveTimer <= 0)
        {
            IsReadyTime = !_isReadyTime;

            //다음Wave로 전환을위해 초기화작업
            if (_isReadyTime)
            {
                //웨이브를 모두 깼다면 클리어 처리
                if (_wave == _stageData.WaveDatas.Count - 1)
                {
                    Debug.Log("웨이브 올클리어");
                    //TODO: 씬전환필요함

                    return;
                }

                //다음 웨이브로 gogo~!!
                _wave++;
                _spawnCoolTime = _stageData.WaveDatas[_wave].SpawnDelay;
                _numsOfSpawnMonster = 0;
                _waveTimer = _stageData.WaveDatas[_wave].WaveReadyTime;
                Debug.Log($"[{Wave}]단계 [준비]시간입니다. ({_waveTimer:00}초)");
            }
            else
            {
                _waveTimer = _stageData.WaveDatas[_wave].WaveLimitTime;
                Debug.Log($"[{Wave}]단계 [전투]시간입니다. ({_waveTimer:00}초)");
            }
        }

        SpawnMonster();

    }

    void Init()
    {
        _wave = 0;
        IsReadyTime = true;
        _spawnCoolTime = _stageData.WaveDatas[_wave].SpawnDelay;
        _numsOfSpawnMonster = 0;
        _waveTimer = _stageData.WaveDatas[_wave].WaveReadyTime;
    }

    void SpawnMonster()
    {
        //스폰처리
        if (_isReadyTime) return;
        if (_numsOfSpawnMonster == _stageData.WaveDatas[_wave].SpawnAmount) return;

        //스폰 쿨타임 계산
        _spawnCoolTime += Time.deltaTime;
        if (_spawnCoolTime >= _stageData.WaveDatas[_wave].SpawnDelay)
        {

            MonsterSpawner.Instance.SpawnMonster(_stageData.WaveDatas[_wave].MonsterData, _wayPoint1);

            //스폰처리 !!!!!!!! enum을하든 설정을 해줘야함.
            // if (_stageData.WaveDatas[_wave].MonsterName == "bat")
            // {
            //     MonsterSpawner.Instance.SpawnBat(null, _wayPoint1);
            // }
            // else if (_stageData.WaveDatas[_wave].MonsterName == "ghost")
            // {
            //     MonsterSpawner.Instance.SpawnGhost(null, _wayPoint1);
            // }
            // else if (_stageData.WaveDatas[_wave].MonsterName == "rabbit")
            // {
            //     MonsterSpawner.Instance.SpawnRabbit(null, _wayPoint1);
            // }
            // }
            // else if(_stageData.WaveDatas [_wave].MonsterName == "slime")
            // {
            //     MonsterSpawner.Instance.SpawnSlime(null, _wayPoint1);
            // }

            _numsOfSpawnMonster++;
            _spawnCoolTime -= _stageData.WaveDatas[_wave].SpawnDelay;
        }
        return;
    }

    private void OnDrawGizmos()
    {
        if (_wayPoint1 == null) return;
        var paths = _wayPoint1.PathPoints;
        for (int i = 0; i < paths.Count - 1; i++)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(paths[i], paths[i + 1]);

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(paths[i], 0.3f);
        }
        Gizmos.DrawSphere(paths[paths.Count - 1], 0.3f);

        if (_wayPoint2 == null) return;
        paths = _wayPoint2.PathPoints;

        for (int i = 0; i < paths.Count - 1; i++)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(paths[i], paths[i + 1]);

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(paths[i], 0.3f);
        }
        Gizmos.DrawSphere(paths[paths.Count - 1], 0.3f);


    }
}
