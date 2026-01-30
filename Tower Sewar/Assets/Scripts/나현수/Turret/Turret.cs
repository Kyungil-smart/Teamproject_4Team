using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    // 단계 컨트롤러
    private Turret_Grade _gradeController;
    private int _curGrade = 0;
    private GunTowerData _currentData;

    // 타워 모델 프리팹
    [SerializeField] private Transform _towerModelParent;
    private GameObject _currentModel;

    // Enemy List
    [SerializeField] private List<Transform> _enemyList = new List<Transform>();
    [SerializeField] private bool _isEnemy;
    public bool IsEnemy => _isEnemy;
    public Transform _currentTarget { get; set; }

    private Muzzle[] _muzzleScripts;
    private int _muzzleIndex = 0;   

    [Header("Firing Settings")]
    private float _attDelay  = 0.5f; 
    private float _attTimer = 0f;

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

        if (_isEnemy && _currentTarget != null)
        {
            HandleFiring();
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            Upgrade();
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

    private void HandleFiring()
    {
        _attTimer += Time.deltaTime;

        if (_attTimer >= _attDelay)
        {
            FireSequential();
            _attTimer = 0f;
        }
    }

    private void FireSequential()
    {
        if (_muzzleScripts == null || _muzzleScripts.Length == 0 || _currentTarget == null) return;

        _muzzleScripts[_muzzleIndex].SetRocket(_currentTarget);

        _muzzleIndex = (_muzzleIndex + 1) % _muzzleScripts.Length;
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
   

    private void UpgradeTower()
    {
        if (_curGrade < 0) return;

        _currentData = _gradeController.TowerDatas[_curGrade];
        // 수치 조정
        _attDelay = _currentData.TowerAttDelay;

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

            _muzzleScripts = _currentModel.GetComponentsInChildren<Muzzle>();
            _muzzleIndex = 0;
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