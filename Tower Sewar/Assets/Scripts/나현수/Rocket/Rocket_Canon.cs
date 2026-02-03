using System;
using UnityEngine;

public class Rocket_Canon : Rocket
{
    [SerializeField] private float _launchTime  = 0.1f;
    [SerializeField] private float _launchSpeed = 0.5f;
    [SerializeField] private float _downSpeed   = 45.0f;
    
    [SerializeField] private float explosionRadius = 3.0f;
    private float _elapsedTime = 0f;

    private Transform targetTransform;

    public override void Launch(Transform target, TowerData towerData)
    {
        base.Launch(target, towerData);
        targetTransform = target;
        _elapsedTime = 0f;
    }

    protected override void Update()
    {
        if (!_isLaunched) return;

        base.Update();

        if (_target == null) return;

        RocketMove();

        HitEnemy();
    }

    protected void RocketMove()
    {
        _elapsedTime += Time.deltaTime;

        Vector3 moveDirection;

        if (_elapsedTime < _launchTime)
        {
            moveDirection = (transform.forward).normalized;

            transform.position += moveDirection * ((_speed * _launchSpeed) * Time.deltaTime);
        }
        else
        {
            moveDirection = (targetTransform.position - transform.position).normalized;
            transform.position += moveDirection * (_downSpeed * Time.deltaTime);
        }

        if (moveDirection != Vector3.zero)
        {
            transform.forward = moveDirection;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    protected void HitEnemy()
    {
        if (Vector3.Distance(_target.position, transform.position) <= 0.2f)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);

            foreach (var hitCollider in hitColliders)
            {
                MonsterBehavior monster = hitCollider.GetComponentInParent<MonsterBehavior>();

                if (monster != null)
                {
                    Debug.Log(_tempTowerData.TowerAtt);
                    Cannon_Tower_Sound_Manager.instance.PlaySFX("Explosion");
                    monster.TakeDamage(_tempTowerData.TowerAtt);
                }
            }
            ReturnToPool();
        }
    }
}