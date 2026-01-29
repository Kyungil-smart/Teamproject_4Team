using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 아래의 코드를 추가 해서 사운드 호출 가능
// Stage_Sound_Manager.instance.ChangeBGM("Wave"); 웨이브 시작
// Stage_Sound_Manager.instance.ChangeBGM("Boss"); 보스 등장 시
// Stage_Sound_Manager.instance.ChangeBGM("Waiting"); 대기 시간
// Stage_Sound_Manager.instance.ChangeBGM("Clear"); 클리어시
// Stage_Sound_Manager.instance.ChangeBGM("Fail"); 실패시

public class Stage_Sound_Manager : MonoBehaviour
{
    public static Stage_Sound_Manager instance;

    [Header("Sound Player")]
    public AudioSource SoundPlayer;

    [Header("Sound Volume Settings")]
    [Range(0f, 1f)] public float waitingVolume = 1.0f;
    [Range(0f, 1f)] public float waveVolume = 0.8f;
    [Range(0f, 1f)] public float bossVolume = 0.8f;
    [Range(0f, 1f)] public float clearVolume = 1.0f;
    [Range(0f, 1f)] public float failVolume = 1.0f;

    [Header("Sound Clip")]
    public AudioClip waitingBgm;
    public AudioClip waveBgm;
    public AudioClip bossBgm;
    public AudioClip clearSfx;
    public AudioClip failSfx;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeBGM("Waiting");
            Debug.Log("대기 BGM");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeBGM("Wave");
            Debug.Log("웨이브 BGM");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeBGM("Boss");
            Debug.Log("보스 BGM");
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeBGM("Clear");
            Debug.Log("클리어 SFX");
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ChangeBGM("Fail");
            Debug.Log("패배 SFX");
        }
    }
    public void ChangeBGM(string state)
    {
        if (SoundPlayer == null)
            return;

        SoundPlayer.Stop();

        switch (state)
        {
            case "Waiting":
                SoundPlayer.clip = waitingBgm;
                SoundPlayer.volume = waitingVolume;
                SoundPlayer.loop = true;
                break;

            case "Wave":
                SoundPlayer.clip = waveBgm;
                SoundPlayer.volume = waveVolume;
                SoundPlayer.loop = true;
                break;

            case "Boss":
                SoundPlayer.clip = bossBgm;
                SoundPlayer.volume = bossVolume;
                SoundPlayer.loop = true;
                break;

            case "Clear":
                SoundPlayer.clip = clearSfx;
                SoundPlayer.volume = clearVolume;
                SoundPlayer.loop = false;
                break;

            case "Fail":
                SoundPlayer.clip = failSfx;
                SoundPlayer.volume = failVolume;
                SoundPlayer.loop = false;
                break;
        }

        SoundPlayer.Play();
    }


}
