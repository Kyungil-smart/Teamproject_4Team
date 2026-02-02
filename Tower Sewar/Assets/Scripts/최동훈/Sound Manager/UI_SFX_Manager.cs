using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SFX_Manager : MonoBehaviour
{
    public static UI_SFX_Manager instance;

    // 
    [Header("Sound Player")]
    public AudioSource sfxPlayer;

    // 
    [Header("SFX Clip")]
    public AudioClip normalSound;
    public AudioClip exitSound;

    // 씬이 넘어가도 오브젝트 파괴 안되는 메서드
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public void PopButtonSound()
    {
        sfxPlayer.PlayOneShot(normalSound);
    }

    public void PopExitSound()
    {
        sfxPlayer.PlayOneShot(exitSound);
        
    }
}
