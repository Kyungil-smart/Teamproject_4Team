using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    // Turret Grade
    private Turret_Grade _gradeController;

    // 터렛 회전
    [SerializeField] private Transform _towerHead; // 회전시킬 머리(상부) 오브젝트

    // 터렛 정보
    [SerializeField] private Transform _towerModelParent; // 프리팹이 생성될 부모 위치
    private GameObject _currentModel; // 현재 화면에 보이는 모델

    private int _curGrade = 0;
    private GunTowerData _currentData;

    // 적 정보
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
        if (_gradeController.TowerDatas.Count > 0)
        {
            RefreshTower();
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
            RefreshTower();
        }
        else
        {
            Debug.Log("이미 최고 등급입니다.");
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

    private void RefreshTower()
    {
        _currentData = _gradeController.TowerDatas[_curGrade];
        Debug.Log($"업그레이드 완료! 현재 이름: {_currentData.TowerName}, 공격력: {_currentData.TowerAtt}");

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