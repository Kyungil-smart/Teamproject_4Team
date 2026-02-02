using UnityEngine;

public class Rocket_Canon : Rocket
{
    [SerializeField] private float _launchTime = 2.0f;
    [SerializeField] private float _launchSpeed = 1.0f;
    [SerializeField] private float _downSpeed = 30.0f;

    private float _elapsedTime = 0f;

    public override void Launch(Transform target, GunTowerData towerData)
    {
        base.Launch(target, towerData);
        _elapsedTime = 0f;
    }

    protected override void Update()
    {
        if (!_isLaunched) return;

        base.Update();

        if (_target == null) return;

        _elapsedTime += Time.deltaTime;

        Vector3 moveDirection;
        
        if(_elapsedTime < _launchTime)
        {
            moveDirection = (transform.forward).normalized;

            transform.position += moveDirection * (_speed * _launchSpeed) * Time.deltaTime;
        }
        else
        {
            moveDirection = (_target.position - transform.position).normalized;
            transform.position += moveDirection * _downSpeed * Time.deltaTime;
        }

        if (moveDirection != Vector3.zero)
        {
            transform.forward = moveDirection;
        }
    }
}