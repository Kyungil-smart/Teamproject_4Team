using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// Enemy_VFX_Manager.instance.Death() 몬스터 죽음 VFX
public class Enemy_VFX_Manager : MonoBehaviour
{
    public static Enemy_VFX_Manager instance;

    [SerializeField] private GameObject _deathVfxPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // 몬스터 사망시 몬스터 위치에서 사망 VFX 생성 및 이펙트 크기 증가
    public void Death(Transform target)
    {
        GameObject enemyVfx = Instantiate(_deathVfxPrefab, target.position, target.rotation);
        enemyVfx.transform.localScale = new Vector3(5f, 5f, 5f);
        Destroy(enemyVfx, 0.5f);
    }
}
