using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title_BGM_Manager : MonoBehaviour
{
    // 싱글톤 패턴
    private static Title_BGM_Manager instance;

    // 인스펙터 생성
    [Header("Sound Player")]
    public AudioSource bgmPlayer;

    [Header("Sound Volume Settings")]
    [Range(0f, 1f)] public float bgmVolume = 0.3f;

    [Header("BGM Clip")]
    public AudioClip titleBgm;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        if (titleBgm != null)
        {
            PlayBGM(titleBgm, bgmVolume);
        }
    }

    // 타이틀 BGM 제어 메서드
    public void PlayBGM(AudioClip title, float bgmVolume)
    {
        bgmPlayer.clip = title;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.loop = true;
        bgmPlayer.Play();
    }

}
