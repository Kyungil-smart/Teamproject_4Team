using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SFX_Manager : MonoBehaviour
{
    public static UI_SFX_Manager instance;

    [Header("Sound Player")]
    public AudioSource sfxPlayer;

    [Header("SFX Clip")]
    public AudioClip normalSound;
    public AudioClip exitSound;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
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
