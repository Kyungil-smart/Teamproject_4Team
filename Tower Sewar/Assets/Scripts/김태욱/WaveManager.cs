using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    

    //스테이지 정보
    [SerializeField]
    StageData _stageData;

    //현재 Wave 단계
    private int _wave;
    public int Wave { get { return _wave; } }

    //Wave Timer
    private float _waveTimer;
    public float WaveTimer { get { return _waveTimer; } }

    //준비(휴식)시간인지?
    private bool _isReadyTime;
    public bool IsReadyTime {  get { return _isReadyTime; } }

    //스폰 주기를 조절하기위한 변수
    private float _spawnCoolTime;
    //현재 wave에서 스폰한 몹 개수
    private int _numsOfSpawnMonster;

    //맵에 스폰된 몬스터의 총 개수
    public int NumsOfMonsters
    { get { return MonsterSpawner.Instance.MonsterCount; } }
     
    void Awake()
    {
        Init();
        Debug.Log("WaveManager Init");

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
            _isReadyTime = !_isReadyTime;

            //다음Wave로 전환을위해 초기화작업
            if (_isReadyTime)
            {
                //스테이지를 모두 깼다면 클리어 처리
                if (_wave == _stageData.WaveDatas.Count - 1)
                {
                    Debug.Log("스테이지 올클리어");
                    //TODO: 씬전환필요함

                    return;
                }

                //다음 웨이브로 gogo~!!
                _wave++;
                _spawnCoolTime = 0;
                _numsOfSpawnMonster = 0;
                _waveTimer = _stageData.WaveDatas[_wave].WaveReadyTime;
            }
            else
                _waveTimer = _stageData.WaveDatas[_wave].WaveLimitTime;
        }

        SpawnMonster();
        Debug.Log($"몬스터 개수 {NumsOfMonsters}");

    }

    void Init()
    {
        _wave = 0;
        _isReadyTime = true;
        _spawnCoolTime = 0;
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
            //스폰처리 !!!!!!!! enum을하든 설정을 해줘야함.
            if (_stageData.WaveDatas[_wave].MonsterName == "박쥐")
            {
                MonsterSpawner.Instance.SpawnBat();
                Debug.Log("박쥐 스폰!");
            }
            else if (_stageData.WaveDatas[_wave].MonsterName == "유령")
            {
                MonsterSpawner.Instance.SpawnGhost();
                Debug.Log("유령 스폰!");
            }
            else if (_stageData.WaveDatas[_wave].MonsterName == "토끼")
            {
                MonsterSpawner.Instance.SpawnRabbit();
                Debug.Log("토끼 스폰!");
            }
            else if(_stageData.WaveDatas [_wave].MonsterName == "슬라임")
            {
                MonsterSpawner.Instance.SpawnSlime();
                Debug.Log("슬라임 스폰!");
            }

            _numsOfSpawnMonster++;
            _spawnCoolTime -= _stageData.WaveDatas[_wave].SpawnDelay;
        }
        return;
    }
}
