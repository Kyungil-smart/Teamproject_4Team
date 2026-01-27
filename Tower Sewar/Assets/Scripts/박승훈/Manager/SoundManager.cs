using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // SFX(효과음) 재생
    public void PlaySFX(AudioClip clip, float volume)
    {
        // SFX 재생 기능 구현
    }
    
    // BGM 재생
    public void PlayBGM(AudioClip clip, float volume)
    {
        // BGM 재생 기능 구현
    }
}
