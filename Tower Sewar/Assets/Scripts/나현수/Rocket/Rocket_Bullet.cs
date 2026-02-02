using UnityEngine;

public class Rocket_Bullet : Rocket
{
    protected override void Update()
    {
        if (!_isLaunched) return;

        base.Update();

        RocketMove();

        HitEnemy();
    }

    protected void RocketMove()
    {
        if (_target != null)
        {
            Vector3 direction   = (_target.position - transform.position).normalized;

            transform.position += direction * (_speed * Time.deltaTime);

            transform.forward   = direction;
            //transform.Rotate(_rotationAngle * Time.deltaTime, Space.Self);
        }
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