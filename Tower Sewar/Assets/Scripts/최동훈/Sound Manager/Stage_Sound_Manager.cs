using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 아래의 코드를 추가 해서 사운드 호출 가능
// Stage_Sound_Manager.instance.ChangeBGM("Wave"); 웨이브 시작
// Stage_Sound_Manager.instance.ChangeBGM("Boss"); 보스 등장 시
// Stage_Sound_Manager.instance.ChangeBGM("Waiting"); 대기 시간

public class Stage_Sound_Manager : MonoBehaviour
{
    public static Stage_Sound_Manager instance;

    [Header("Audio Player")]
    public AudioSource bgmPlayer;

    [Header("BGM Clip")]
    public AudioClip waveBgm;
    public AudioClip waitingBgm;
    public AudioClip bossBgm;

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
    }
    public void ChangeBGM(string state)
    {
        if (bgmPlayer == null) return;

        bgmPlayer.Stop();

        switch (state)
        {
            case "Waiting":
                bgmPlayer.clip = waitingBgm;
                break;
            case "Wave":
                bgmPlayer.clip = waveBgm;
                break;
            case "Boss":
                bgmPlayer.clip = bossBgm;
                break;
        }

        bgmPlayer.Play();
        bgmPlayer.loop = true;
    }

  
}
