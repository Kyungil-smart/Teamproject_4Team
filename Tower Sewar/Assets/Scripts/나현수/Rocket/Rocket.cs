using UnityEngine;


public class Rocket : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] bool  _isLaunched;
    [SerializeField] float _activeTime    = 0;
    [SerializeField] float _maxActiveTime = 5;
    [SerializeField] int   _speed         = 20;

    // 자기회전
    [SerializeField] private Vector3 _rotationAngle = new Vector3(0, 0, 500); // Z축(앞방향)으로 회전

    public void Launch(Transform target)
    {
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
        transform.forward = direction;
        transform.Rotate(_rotationAngle * Time.deltaTime, Space.Self);
    }

    private void ReturnToPool()
    {
        _isLaunched = false;
        gameObject.SetActive(false);
    }
}



