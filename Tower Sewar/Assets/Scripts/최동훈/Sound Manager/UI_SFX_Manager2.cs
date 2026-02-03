using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SFX_Manager2 : MonoBehaviour
{
    // 싱글톤 패턴
    public static UI_SFX_Manager2 instance;

    // 인스펙터 생성
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

            DontDestroyOnLoad(gameObject); // 씬 전환시 파괴 방지
        }

        else
        {
            Destroy(gameObject);
        }
    }

    // Ui 버튼 클릭시 일회성 사운드 호출 메서드
    public void PopButtonSound()
    {
        sfxPlayer.PlayOneShot(normalSound);
    }

    public void PopExitSound()
    {
        sfxPlayer.PlayOneShot(exitSound);
    }
}
