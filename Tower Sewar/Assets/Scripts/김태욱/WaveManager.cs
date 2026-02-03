using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField] private HUDManager hudManager;
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
                // Debug.Log("Wait");
                Stage_Sound_Manager.instance?.SettingSound("Waiting");
            }
            else
            {
                // Debug.Log("Start");
                Stage_Sound_Manager.instance?.SettingSound("Wave");
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

    bool _isNextStageReserved;

    void Awake()
    {
        _instance = this;
        
    }

    void Start()
    {
        Init();
        // Debug.Log($"[{Wave}]단계 [준비]시간입니다. ({_waveTimer:00}초)");
    }

    void Update()
    {
        if(_isNextStageReserved)
        {
            if (NumsOfMonsters == 0)
            {
                if (GameSceneManager.Instance?.CurrentSceneIndex()
                    >= SceneManager.sceneCountInBuildSettings - 1)
                {
                    Debug.Log($"{SceneManager.sceneCountInBuildSettings} 씬 카운트");
                    hudManager?.ShowVictoryPanel();
                    Destroy(this.gameObject);
                    return;
                }
                Debug.Log("로드 다음 스테이지");
                GameSceneManager.Instance?.LoadNextStage();
            }
            return;
        }

        //웨이브 시간 차감
        _waveTimer -= Time.deltaTime;

        //타이머 로그
        //if(_isReadyTime)
        //    Debug.Log($"[{_wave}단계] 준비시간 {_waveTimer:000}");
        //else Debug.Log($"[{_wave}단계] 전투시간 {_waveTimer:000}");

        //타이머가 끝나면 상태변경(준비시간 or 웨이브시간)
        if (_waveTimer <= 0)
        {

            //다음Wave로 전환을위해 초기화작업
            if (!IsReadyTime)
            {
                //웨이브 클리어 골드 추가
                DataManager.Instance.PlayerGold += _stageData.WaveDatas[_wave].ClearGold;

                //웨이브를 모두 깼다면 클리어 처리
                if (_wave == _stageData.WaveDatas.Count - 1)
                {
                    //Scene전환
                    _isNextStageReserved = true;
                    return;
                }

                //다음 웨이브로 gogo~!!
                _wave++;
                _spawnCoolTime = _stageData.WaveDatas[_wave].SpawnDelay;
                _numsOfSpawnMonster = 0;
                _waveTimer = _stageData.WaveDatas[_wave].WaveReadyTime;
                // Debug.Log($"[{Wave}]단계 [준비]시간입니다. ({_waveTimer:00}초)");
            }
            else
            {
                _waveTimer = _stageData.WaveDatas[_wave].WaveLimitTime;
                // Debug.Log($"[{Wave}]단계 [전투]시간입니다. ({_waveTimer:00}초)");
            }

            IsReadyTime = !IsReadyTime;
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
        DataManager.Instance.PlayerGold = _stageData.StartGold;
        _isNextStageReserved = false;
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
            //Spawn 위치가 지정되어있으면 그에 맞게 스폰.
            var d = _stageData.WaveDatas[_wave];
            if (d.Spawn_Left || d.Spawn_Right)
            {
                if (d.Spawn_Left)
                {
                    MonsterSpawner.Instance.SpawnMonster(_stageData.WaveDatas[_wave].MonsterData, _wayPoint1);
                    // _numsOfSpawnMonster++;
                }
                if (d.Spawn_Right)
                {
                    MonsterSpawner.Instance.SpawnMonster(_stageData.WaveDatas[_wave].MonsterData, _wayPoint2);
                    // _numsOfSpawnMonster++;
                }
            }
            //일반적인 스폰이라면 그냥 스폰.
            else
            {
                MonsterSpawner.Instance.SpawnMonster(_stageData.WaveDatas[_wave].MonsterData, _wayPoint1);
            }

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
