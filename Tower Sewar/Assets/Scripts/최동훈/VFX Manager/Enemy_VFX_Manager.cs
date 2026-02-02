using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// Enemy_VFX_Manager.instance.Death() 몬스터 죽음 VFX
public class Enemy_VFX_Manager : MonoBehaviour
{
    // 싱글톤 패턴
    public static Enemy_VFX_Manager instance;

    [SerializeField] private GameObject _deathVfxPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // 몬스터 사망시 VFX 출력 메서드
    public void Death(Transform target)
    {
        GameObject enemyVfx = Instantiate(_deathVfxPrefab, target.position, target.rotation);
        enemyVfx.transform.localScale = new Vector3(5f, 5f, 5f);
        Destroy(enemyVfx, 0.5f);
    }
}
