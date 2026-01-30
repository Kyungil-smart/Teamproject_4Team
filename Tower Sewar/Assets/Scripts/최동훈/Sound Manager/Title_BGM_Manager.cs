using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title_BGM_Manager : MonoBehaviour
{
    private static Title_BGM_Manager instance;

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

    public void PlayBGM(AudioClip clip)
    {
        bgmPlayer.clip = clip;
        bgmPlayer.loop = true;
        bgmPlayer.Play();
    }

}
