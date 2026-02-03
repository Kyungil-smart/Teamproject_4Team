using UnityEngine;
using TMPro;

public class UpgradCost : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private Turret _turret;
    private TileRaycaster _raycaster;
    
    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _raycaster = GetComponentInParent<TileRaycaster>();
    }
    private void Update()
    {
        _turret = _raycaster.SelectedTurret;
        if (_turret != null)
        {
            if (_turret.CurGrade + 1 >= _turret.gradeController._towerData.Count)
                return;
            _text.text = $"{_turret.gradeController._towerData[_turret.CurGrade + 1].TowerUpCost}";
        }
    }
}
