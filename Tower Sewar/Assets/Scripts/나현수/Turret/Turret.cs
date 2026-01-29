using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Turret_Grade _gradeController;

    [SerializeField] private Transform _towerModelParent;
    private GameObject _currentModel;

    private int _curGrade = 0;
    private GunTowerData _currentData;

    [SerializeField] private List<Transform> _enemyList = new List<Transform>();
    [SerializeField] private bool _isEnemy;
    public bool IsEnemy => _isEnemy;
    public Transform _currentTarget { get; set; }

    private void Awake()
    {
        _gradeController = GetComponent<Turret_Grade>();

        _isEnemy = false;
    }

    private void Start()
    {
        if (_curGrade >= 0 && _gradeController.TowerDatas.Count > 0)
        {
            UpgradeTower();
        }
    }

    private void Update()
    {
        UpdateTarget();

        if (Input.GetKeyDown(KeyCode.V))
        {
            Upgrade();
        }
    }

    private void Upgrade()
    {
        if (_curGrade + 1 < _gradeController.TowerDatas.Count)
        {
            _curGrade++;
            UpgradeTower();
        }
        else
        {
            Debug.Log("이미 최고 레벨입니다");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!_enemyList.Contains(other.transform))
            {
                _enemyList.Add(other.transform);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (_enemyList.Contains(other.transform))
            {
                _enemyList.Remove(other.transform);
            }
        }
    }

    private void UpgradeTower()
    {
        if (_curGrade < 0) return;

        _currentData = _gradeController.TowerDatas[_curGrade];
        Debug.Log($"{_currentData.TowerName}, 데미지 : {_currentData.TowerAtt}");

        if (_currentModel != null)
        {
            Destroy(_currentModel);
        }

        if (_gradeController.TowerPrefabs.Length > _curGrade && _gradeController.TowerPrefabs[_curGrade] != null)
        {
            _currentModel = Instantiate(_gradeController.TowerPrefabs[_curGrade], _towerModelParent);

            _currentModel.transform.localPosition = Vector3.zero;
            _currentModel.transform.localRotation = Quaternion.identity;
        }
    }

    private void UpdateTarget()
    {
        _enemyList.RemoveAll(enemy => enemy == null);

        if (_enemyList.Count > 0)
        {
            _isEnemy = true;
            _currentTarget = _enemyList[0];
        }
        else
        {
            _isEnemy = false;
            _currentTarget = null;
        }
    }
}