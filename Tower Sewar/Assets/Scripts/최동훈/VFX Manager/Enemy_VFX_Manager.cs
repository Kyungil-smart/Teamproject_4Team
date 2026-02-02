using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// Enemy_VFX_Manager.instance.Death() 몬스터 죽음 VFX
public class Enemy_VFX_Manager : MonoBehaviour
{
    public static Enemy_VFX_Manager instance;

    [SerializeField] private GameObject _deathVfxPrefab;
    [SerializeField] private Transform _death;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Death();
        }
    }

    public void Death()
    {
        GameObject enemyVfx = Instantiate(_deathVfxPrefab, _death.position, _death.rotation);
        Destroy(enemyVfx, 0.5f);
    }
}
