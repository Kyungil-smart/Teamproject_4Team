using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enemy_VFX_Manager.instance.TakenDamage() Base 공격받는 VFX
public class Base_VFX_Manager : MonoBehaviour
{
    public static Base_VFX_Manager instance;

    [SerializeField] private GameObject _damageVfxPrefab;
    [SerializeField] private Transform _damage;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // 베이스 피격시 피격 위치에서 피격 VFX 생성 및 이펙트 크기 증가
    public void TakenDamage(Transform target)
    {
        GameObject baseVfx = Instantiate(_damageVfxPrefab, target.position, target.rotation);
        Destroy(baseVfx, 0.5f);
    }
}