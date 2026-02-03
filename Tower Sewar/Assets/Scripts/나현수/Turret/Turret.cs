using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private SphereCollider _collider;
    [SerializeField] private float _range;
    
    // 단계 컨트롤러
    Turret_Grade _gradeController;
    public Turret_Grade gradeController => _gradeController;
    private int _curGrade = 0;
    public int CurGrade
    {
        get { return _curGrade;}  private set  { _curGrade = value;}
    }
    
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
    private float _attTimer = 10.0f;

    private void Awake()
    {
        _gradeController = GetComponent<Turret_Grade>();
        _collider = GetComponent<SphereCollider>();
        
        _isEnemy = false;
    }

    private void Start()
    {
        if (_gradeController == null) return;
        if (_curGrade >= 0 && _gradeController._towerData.Count > 0)
        {
            MachineGun_Tower_Sound_Manager.instance.PlaySFX("Build");
            _collider.radius = _gradeController._towerData[_curGrade].TowerRange;
            UpgradeTower();
        }
    }
    

    private void Update()
    {
        UpdateTarget();
        if(_collider != null)
            _range = _collider.radius;
        if (_isEnemy && _currentTarget != null)
        {
            HandleFiring();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;
    
        MonsterBehavior monster = other.GetComponentInParent<MonsterBehavior>();

        if (monster == null) return;
        
        Debug.Log(monster.name);
    
        Transform aim = monster.GetAimPoint();
        if (!_enemyList.Contains(aim))
        {
            _enemyList.Add(aim);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;
    
        MonsterBehavior monster = other.GetComponentInParent<MonsterBehavior>();
        if (monster == null) return;
    
        Transform aim = monster.GetAimPoint();
        if (_enemyList.Contains(aim))
        {
            _enemyList.Remove(aim);
        }
    }

    private void HandleFiring()
    {
        _attTimer += Time.deltaTime;

        if (_attTimer >= _gradeController._towerData[_curGrade].TowerAttDelay)
        {
            FireSequential();
            _attTimer = 0f;
        }
    }

    private void FireSequential()
    {
        if (_muzzleScripts == null || _muzzleScripts.Length == 0 || _currentTarget == null) return;
        
        _muzzleScripts[_muzzleIndex].SetRocket(_currentTarget, _gradeController._towerData[_curGrade]);

        _muzzleIndex = (_muzzleIndex + 1) % _muzzleScripts.Length;

        switch (_gradeController._towerData[_curGrade].TowerName)
        {
            case "GunTower" :
                MachineGun_Tower_Sound_Manager.instance.PlaySFX("Attack");
                break;
            case "CannonTower" :
                Cannon_Tower_Sound_Manager.instance.PlaySFX("Attack");
                break;
        }
    }

    public void Upgrade()
    {
        if (_curGrade + 1 < _gradeController._towerData.Count)
        {
            _curGrade++;
            if(_curGrade > 0)
                MachineGun_Tower_Sound_Manager.instance.PlaySFX("Upgrade");
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

        if (_currentModel != null)
        {
            Destroy(_currentModel);
        }

        if (_gradeController.TowerPrefabs.Length > _curGrade && _gradeController.TowerPrefabs[_curGrade] != null)
        {
            Debug.Log(_gradeController.TowerPrefabs[_curGrade].name);
            _currentModel = Instantiate(_gradeController.TowerPrefabs[_curGrade], _towerModelParent);
            
            _currentModel.transform.localPosition = Vector3.zero;
            _currentModel.transform.localRotation = Quaternion.identity;

            _muzzleScripts = _currentModel.GetComponentsInChildren<Muzzle>();
            _muzzleIndex = 0;
            
            Debug.Log(_gradeController._towerData[_curGrade].TowerRange);
            _collider.radius = _gradeController._towerData[_curGrade].TowerRange;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _range * 2);
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