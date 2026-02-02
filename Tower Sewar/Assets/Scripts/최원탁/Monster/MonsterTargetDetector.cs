using UnityEngine;

public class MonsterTargetDetector : MonoBehaviour
{
    [Header("Distance")]
    [SerializeField] private float detectEnterRadius = 6f; //거리안 들어오면 켜지고
    [SerializeField] private float detectExitRadius = 7f; // 거리 바깥 나가면 꺼짐

    [Header("Retarget Control")]
    [SerializeField] private float retargetDelay = 0.3f; // 깜빡임 방지 핵심

    [SerializeField] private LayerMask monsterLayer;

    private Monster currentTarget;
    private float nextSearchTime = 0f;

    private void Update()
    {
        UpdateTarget();
    }

    private void UpdateTarget()
    {
        // =========================
        // 타겟이 있는 경우
        // =========================
        if (currentTarget != null)
        {
            float dist = Vector3.Distance(
                transform.position,
                currentTarget.transform.position
            );

            if (currentTarget.IsDead || dist > detectExitRadius)
            {
                currentTarget.HideHealthUI();
                currentTarget = null;

                // ★ 타겟 해제 후 바로 다시 잡지 못하게 함
                nextSearchTime = Time.time + retargetDelay;
            }

            return;
        }

        // =========================
        // 쿨타임 중이면 탐색 안 함
        // =========================
        if (Time.time < nextSearchTime)
            return;

        // =========================
        // 새 타겟 탐색
        // =========================
        Collider[] hits = Physics.OverlapSphere(
            transform.position,
            detectEnterRadius,
            monsterLayer
        );

        if (hits.Length == 0)
            return;

        Monster nearest = SelectNearestMonster(hits);

        if (nearest != null)
        {
            currentTarget = nearest;
            currentTarget.ShowHealthUI();
        }
    }

    private Monster SelectNearestMonster(Collider[] candidates)
    {
        Monster nearest = null;
        float minDist = float.MaxValue;

        foreach (var col in candidates)
        {
            Monster monster = col.GetComponentInParent<Monster>();
            if (monster == null)
                continue;

            float dist = Vector3.Distance(
                transform.position,
                monster.transform.position
            );

            if (dist < minDist)
            {
                minDist = dist;
                nearest = monster;
            }
        }

        return nearest;
    }
}
