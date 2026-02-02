using UnityEngine;

public class Foundation : MonoBehaviour
{
    [Header("Tower Build Settings")]
    [SerializeField] private Transform buildPoint;   // 타워 생성 위치
    [SerializeField] private GameObject builtTower;  // 생성된 타워 참조 (부모 아님)

    /// <summary>
    /// 이 파운데이션에 타워를 지을 수 있는지
    /// </summary>
    public bool CanBuild()
    {
        return builtTower == null;
    }

    /// <summary>
    /// 타워 생성 (월드에 독립적으로 생성)
    /// </summary>
    public void BuildTower(GameObject turretPrefab)
    {
        if (!CanBuild())
        {
            Debug.LogWarning("이미 타워가 설치된 Foundation입니다.");
            return;
        }

        if (turretPrefab == null)
        {
            Debug.LogError("Turret Prefab이 설정되지 않았습니다.");
            return;
        }

        Transform spawnTransform = buildPoint != null ? buildPoint : transform;

        // 부모 없이 생성 (스케일 상속 완전 차단)
        GameObject turret = Instantiate(
            turretPrefab,
            spawnTransform.position,
            spawnTransform.rotation
        );

        builtTower = turret;
    }

    /// <summary>
    /// (선택) 타워 제거
    /// </summary>
    public void RemoveTower()
    {
        if (builtTower == null)
            return;

        Destroy(builtTower);
        builtTower = null;
    }
}
