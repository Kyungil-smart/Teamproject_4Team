using UnityEngine;

public class Rocket_Canon : Rocket
{
    protected override void Update()
    {
        if (!_isLaunched) return;

        base.Update();

        if (_target != null)
        {
            Vector3 direction = (_target.position - transform.position).normalized;
            transform.position += direction * _speed * Time.deltaTime;
            transform.forward = direction;
            transform.Rotate(_rotationAngle * Time.deltaTime, Space.Self);
        }
    }
}