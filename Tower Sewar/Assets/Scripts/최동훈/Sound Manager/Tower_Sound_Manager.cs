using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 아래의 코드를 추가 해서 사운드 호출 가능
// Tower_Sound_Manager.instance.PlaySFX("Attack"); 공격 시
// Tower_Sound_Manager.instance.PlaySFX("Build"); 타워 설치 시
// Tower_Sound_Manager.instance.PlaySFX("Upgrade"); 타워 업그레이드 시
// Tower_Sound_Manager.instance.PlaySFX("Destroy"); 타워 철거 시

public class Tower_Sound_Manager : MonoBehaviour
{
    public static Tower_Sound_Manager instance;

    [Header("Tower Player")]
    public AudioSource TowerSound;

    [Header("Tower Volume Settings")]
    [Range(0f, 1f)] public float attackVolume = 1.0f;
    [Range(0f, 1f)] public float buildVolume = 0.8f;
    [Range(0f, 1f)] public float upgradeVolume = 0.8f;
    [Range(0f, 1f)] public float destroyVolume = 1.0f;

    [Header("Tower Clip")]
    public AudioClip attackSfx;
    public AudioClip buildSfx;
    public AudioClip UpgradeSfx;
    public AudioClip destroySfx;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlaySFX("Attack");
            Debug.Log("공격 SFX");
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            PlaySFX("Build");
            Debug.Log("건설 SFX");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            PlaySFX("Upgrade");
            Debug.Log("업그레이드 SFX");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            PlaySFX("Destroy");
            Debug.Log("철거 SFX");
        }
    }

    public void PlaySFX(string state)
    {
        if (TowerSound == null)
            return;

        switch (state)
        {
            case "Attack":
                TowerSound.PlayOneShot(attackSfx);
                TowerSound.volume = attackVolume;
                break;

            case "Build":
                TowerSound.PlayOneShot(buildSfx);
                TowerSound.volume = buildVolume;
                break;

            case "Upgrade":
                TowerSound.PlayOneShot(UpgradeSfx);
                TowerSound.volume = upgradeVolume;
                break;

            case "Destroy":
                TowerSound.PlayOneShot(destroySfx);
                TowerSound.volume = destroyVolume;
                break;
        }
    }
}

