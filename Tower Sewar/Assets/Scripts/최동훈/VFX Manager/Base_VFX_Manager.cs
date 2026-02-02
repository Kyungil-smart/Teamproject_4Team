using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enemy_VFX_Manager.instance.TakenDamage() Base 공격받는 VFX
public class Base_VFX_Manager : MonoBehaviour
{
    // 싱글톤 패턴
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

    // 베이스 피시 VFX 출력 메서드
    public void TakenDamage()
    {
        GameObject baseVfx = Instantiate(_damageVfxPrefab, _damage.position, _damage.rotation);
        baseVfx.transform.localScale = new Vector3(5f, 5f, 5f);
        Destroy(baseVfx, 0.5f);
    }
}