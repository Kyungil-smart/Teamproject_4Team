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
    //현재 스폰한 몹 개수
    private int _numsOfSpawnMonster;

     
    void Awake()
    {
        Init();
    }

    void Update()
    {
        //웨이브 시간 차감
        _waveTimer -= Time.deltaTime;
        //Debug.Log($"waveTimer {_waveTimer}");

        //타이머가 끝나면 상태변경
        if (_waveTimer <= 0)
        {
            _isReadyTime = !_isReadyTime;
            Debug.Log($"readyTime : {_isReadyTime} 으로 전환");

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
                Debug.Log($"Wave : {_wave}단계 시작!!!");
            }
            else
                _waveTimer = _stageData.WaveDatas[_wave].WaveLimitTime;
        }


        //스폰처리
        if (_isReadyTime) return;
        if (_numsOfSpawnMonster == _stageData.WaveDatas[_wave].SpawnAmount) return;

        //스폰 쿨타임 계산
        _spawnCoolTime += Time.deltaTime;
        if(_spawnCoolTime >= _stageData.WaveDatas[_wave].SpawnDelay)
        {
            //스폰로직
            Debug.Log("스폰처리");

            _numsOfSpawnMonster++;
            _spawnCoolTime -= _stageData.WaveDatas[_wave].SpawnDelay;
        }
        return;


    }

    void Init()
    {
        _wave = 0;
        _isReadyTime = true;
        _spawnCoolTime = 0;
        _numsOfSpawnMonster = 0;
        _waveTimer = _stageData.WaveDatas[_wave].WaveReadyTime;
    }
}
