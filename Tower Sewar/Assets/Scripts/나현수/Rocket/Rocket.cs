using UnityEngine;

public class Rocket : MonoBehaviour
{
    protected GunTowerData _tempTowerData;

    [SerializeField] protected Transform _target; // protected로 변경
    [SerializeField] protected bool      _isLaunched;
    [SerializeField] protected float     _activeTime    = 0;
    [SerializeField] protected float     _maxActiveTime = 5;
    [SerializeField] protected int       _speed         = 30;
    [SerializeField] protected int       _damage;

    [SerializeField] protected Vector3 _rotationAngle = new Vector3(0, 0, 500);

    public virtual void Launch(Transform target, GunTowerData towerData)
    {
        _tempTowerData = towerData;
        _target        = target;
        _isLaunched    = true;
        _activeTime    = 0;
    }
    protected virtual void Update()
    {
        if (!_isLaunched) return;

        _activeTime += Time.deltaTime;

        if (_target == null || _activeTime >= _maxActiveTime)
        {
            ReturnToPool();
            return;
        }

        // 공통 기능인 피격 확인은 여기서 수행
        HitEnemy();
    }

    protected void ReturnToPool()
    {
        _isLaunched = false;
        gameObject.SetActive(false);
    }

    protected void HitEnemy()
    {
        if (_target == null) return;

        if (Vector3.Distance(_target.position, transform.position) <= 0.2f)
        {
            MonsterBehavior monster = _target.GetComponent<MonsterBehavior>();
            if (monster != null)
            {
                monster.TakeDamage(_tempTowerData.TowerAtt);
            }

            ReturnToPool();
        }
    }
}