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
    public void Death(Transform target)
    {
        GameObject enemyVfx = Instantiate(_deathVfxPrefab, target.position, target.rotation);
        Destroy(enemyVfx, 0.5f);
    }
}
