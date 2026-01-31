using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 아래의 코드를 추가 해서 사운드 호출 가능
// Enemy_Sound_Manager.instance.PlaySfx() 몬스터 죽음
public class Enemy_Sound_Manager : MonoBehaviour
{
    public static Enemy_Sound_Manager instance;

    [Header("Sound Player")]
    public AudioSource SfxPlayer;

    [Header("Sound Volume Settings")]

    [Range(0f, 1f)] public float sfxVolume = 1.0f;

    [Header("Sound Clip")]
    public AudioClip deathSfx;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            PlaySfx();
            Debug.Log("몬스터 죽음");
        }
    }
    public void PlaySfx()
    {
        if (deathSfx != null && SfxPlayer != null)
        {
            SfxPlayer.PlayOneShot(deathSfx, sfxVolume);
        }
    }
}