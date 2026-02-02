using UnityEngine;

public class Rocket : MonoBehaviour
{
    protected TowerData _tempTowerData;

    [SerializeField] protected Transform _target;
    [SerializeField] protected bool      _isLaunched;
    [SerializeField] protected float     _activeTime    = 0;
    [SerializeField] protected float     _maxActiveTime = 5;
    [SerializeField] protected int       _speed         = 30;
    [SerializeField] protected int       _damage;

    [SerializeField] protected Vector3 _rotationAngle = new Vector3(0, 0, 500);

    public virtual void Launch(Transform target, TowerData towerData)
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
    }

    protected void ReturnToPool()
    {
        _isLaunched = false;
        gameObject.SetActive(false);
    }

    
}