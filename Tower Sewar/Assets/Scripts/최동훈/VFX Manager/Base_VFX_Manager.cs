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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            TakenDamage();
        }
    }

    public void TakenDamage()
    {
        GameObject baseVfx = Instantiate(_damageVfxPrefab, _damage.position, _damage.rotation);
        Destroy(baseVfx, 0.5f);
    }
}