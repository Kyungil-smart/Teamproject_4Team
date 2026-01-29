using UnityEngine;

public class Turret_Rotate : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed = 30.0f;
    [SerializeField] private Turret _turret;

    void Awake()
    {
        _turret = GetComponentInParent<Turret>();
    }

    void Update()
    {
        if (_turret == null)
        {
            Debug.Log("터렛이 존재하지 않습니다.");
            return;
        }

        if (_turret.IsEnemy && _turret._currentTarget != null)
        {
            transform.LookAt(_turret._currentTarget);
        }
        else
        {
            transform.Rotate(Vector3.up * _rotateSpeed * Time.deltaTime);
        }
    }
}