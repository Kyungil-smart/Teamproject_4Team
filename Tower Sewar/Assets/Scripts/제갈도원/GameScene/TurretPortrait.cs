using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretPortrait : MonoBehaviour
{
    private Image _portrait;
    private Turret _turret;
    private TileRaycaster _raycaster;
    
    private void Awake()
    {
        _portrait = GetComponent<Image>();
        _raycaster = GetComponentInParent<TileRaycaster>();
    }
    private void Update()
    {
        _turret = _raycaster.SelectedTurret;
        if (_turret != null)
        {
            if (_turret.CurGrade + 1 >= _turret.gradeController._towerData.Count)
                return;
            _portrait.sprite = _turret.gradeController._towerData[_turret.CurGrade + 1].TowerIcon;
        }
    }
}
