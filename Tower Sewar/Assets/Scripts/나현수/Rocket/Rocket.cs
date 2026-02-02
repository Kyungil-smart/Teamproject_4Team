using UnityEngine;

public class Rocket : MonoBehaviour
{
    // 데이터
    private GunTowerData _tempTowerData;

    // 
    [SerializeField] Transform _target;
    [SerializeField] bool  _isLaunched;
    [SerializeField] float _activeTime    = 0;
    [SerializeField] float _maxActiveTime = 5;
    [SerializeField] int   _speed         = 20;
    [SerializeField] int   _damage;

    // 자기회전
    [SerializeField] private Vector3 _rotationAngle = new Vector3(0, 0, 500); // Z축(앞방향)으로 회전

    // 스탯

    public void Launch(Transform target, GunTowerData towerData)
    {
        _tempTowerData = towerData;

        _target     = target;
        _isLaunched = true;
        _activeTime = 0;
    }

    private void Update()
    {
        if (!_isLaunched) return;

        _activeTime += Time.deltaTime;

        if (_target == null || _activeTime >= _maxActiveTime)
        {
            ReturnToPool();
            return;
        }

        Vector3 direction = (_target.position - transform.position).normalized;

        transform.position += direction * _speed * Time.deltaTime;
        transform.forward   = direction;

        transform.Rotate(_rotationAngle * Time.deltaTime, Space.Self);

        // 피격 확인
        HitEnemy();
    }

    private void ReturnToPool()
    {
        _isLaunched = false;
        gameObject.SetActive(false);
    }

    private void HitEnemy()
    {
        if (_target == null) return;

        if (Vector3.Distance(_target.position, this.transform.position) <= 0.2f)
        {
            MonsterBehavior monster = _target.GetComponent<MonsterBehavior>();

            if (monster != null)
            {
                monster.TakeDamage(_tempTowerData.TowerAtt);
            }

            _isLaunched = false;
            ReturnToPool();
        }
    }
}