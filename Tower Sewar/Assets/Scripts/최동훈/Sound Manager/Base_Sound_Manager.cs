using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 아래의 코드를 추가 해서 사운드 호출 가능
// Base_Sound_Manager.instance.BaseSFX("Taken_Damage"); 베이스 피격시
// Base_Sound_Manager.instance.BaseSFX("Destroy"); 베이스 파괴 시
public class Base_Sound_Manager : MonoBehaviour
{
    public static Base_Sound_Manager instance;

    [Header("Sound Player")]
    public AudioSource baseSound;

    [Header("Sound Volume Settings")]
    [Range(0f, 1f)] public float takenDamageVolume = 0.15f;
    [Range(0f, 1f)] public float destroyVolume = 0.15f;

    [Header("Base Clip")]
    public AudioClip takenDamageSFX;
    public AudioClip destroySFX;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            BaseSFX("Taken_Damage");
            Debug.Log("베이스 피격");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            BaseSFX("Destroy");
            Debug.Log("베이스 부서짐");
        }
    }

    public void BaseSFX(string state)
    {
        if (baseSound == null)
            return;

        switch (state)
        {
            case "Taken_Damage":
                baseSound.PlayOneShot(takenDamageSFX, takenDamageVolume);
                break;

            case "Destroy":
                baseSound.PlayOneShot(destroySFX, destroyVolume);
                break;
        }
    }
}