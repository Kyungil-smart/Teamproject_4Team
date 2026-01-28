using UnityEngine;

public class Turret_Rotate : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed = 30.0f;
    private Turret _turret;

    void Awake()
    {
        _turret = GetComponentInParent<Turret>();
    }

    void Update()
    {
        if (_turret == null) return;

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