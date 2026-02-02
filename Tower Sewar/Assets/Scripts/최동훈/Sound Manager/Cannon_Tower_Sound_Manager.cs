using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 아래의 코드를 추가 해서 사운드 호출 가능
// Cannon_Tower_Sound_Manager.instance.PlaySFX("Attack"); 공격 시
// Cannon_Tower_Sound_Manager.instance.PlaySFX("Build"); 타워 설치 시
// Cannon_Tower_Sound_Manager.instance.PlaySFX("Upgrade"); 타워 업그레이드 시
// Cannon_Tower_Sound_Manager.instance.PlaySFX("Destroy"); 타워 철거 시
// Cannon_Tower_Sound_Manager.instance.PlaySFX("Explosion"); 공격 적중 시

public class Cannon_Tower_Sound_Manager : MonoBehaviour
{
    // 싱글톤 패턴
    public static Cannon_Tower_Sound_Manager instance;

    // 인스펙터 생성
    [Header("Sound Player")]
    public AudioSource towerSound;

    [Header("Sound Volume Settings")]
    [Range(0f, 1f)] public float attackVolume = 0.25f;
    [Range(0f, 1f)] public float buildVolume = 0.35f;
    [Range(0f, 1f)] public float upgradeVolume = 0.2f;
    [Range(0f, 1f)] public float destroyVolume = 0.15f;
    [Range(0f, 1f)] public float explosionVolume = 0.15f;

    [Header("Tower Clip")]
    public AudioClip attackSfx;
    public AudioClip buildSfx;
    public AudioClip UpgradeSfx;
    public AudioClip destroySfx;
    public AudioClip explosionSfx;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // 타워 상태에 따라 사운드 호출  메서드
    public void PlaySFX(string state)
    {
        if (towerSound == null)
            return;
        
        switch (state)
        {
            case "Attack":
                towerSound.PlayOneShot(attackSfx, attackVolume);
                break;

            case "Build":
                towerSound.PlayOneShot(buildSfx, buildVolume);
                break;

            case "Upgrade":
                towerSound.PlayOneShot(UpgradeSfx, upgradeVolume);
                break;

            case "Destroy":
                towerSound.PlayOneShot(destroySfx, destroyVolume);
                break;

            case "Explosion":
                towerSound.PlayOneShot(explosionSfx, explosionVolume);
                break;
        }
    }
}

