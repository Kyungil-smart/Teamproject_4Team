using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title_BGM_Manager : MonoBehaviour
{
    private static Title_BGM_Manager instance;

    [Header("Audio Player")]
    public AudioSource bgmPlayer;

    [Header("BGM Clip")]
    public AudioClip titleBgm;

    private void Start()
    {
        if (titleBgm != null)
        { 
            PlayBGM(titleBgm);
        }
    }

    public void PlayBGM(AudioClip clip)
    {
        bgmPlayer.clip = clip;
        bgmPlayer.loop = true;
        bgmPlayer.Play();
    }

}
