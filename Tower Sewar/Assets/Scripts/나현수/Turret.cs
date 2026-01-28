using System.Collections.Generic; // ����Ʈ ����� ���� �ʿ��մϴ�.
using UnityEngine;

public class Rotate : MonoBehaviour
{
    // �ͷ� ����
    [SerializeField] private float _rotateSpeed = 30.0f;
    private Turret_Grade turret_Grade = new Turret_Grade();

    // �� ����
    [SerializeField] private List<Transform> _enemyList = new List<Transform>();

    [SerializeField] private bool _isEnemy;
    private Transform _currentTarget;

    private void Awake()
    {
        _isEnemy = false;
        _rotateSpeed = 30.0f;
    }

    private void Update()
    {
        UpdateTarget();

        if (_isEnemy && _currentTarget != null)
        {
            transform.LookAt(_currentTarget);
        }
        else
        {
            transform.Rotate(Vector3.up * _rotateSpeed * Time.deltaTime);
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
}