using UnityEngine;

public class Foundation : MonoBehaviour
{
    [Header("Tower Build Settings")]
    [SerializeField] private Transform buildPoint;   // 타워 생성 위치
    [SerializeField] private GameObject builtTower;  // 생성된 타워 참조 (부모 아님)
    public Turret BuiltTurret { get; private set; }
    /// <summary>
    /// 이 파운데이션에 타워를 지을 수 있는지
    /// </summary>
    public bool CanBuild() => builtTower == null;

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

        BuiltTurret = turret.GetComponent<Turret>();

        builtTower = turret;
    }

    /// <summary>
    /// [역할]
    /// - 타워 설치를 "시도"한다
    /// - 골드가 충분하면 설치 + 차감
    /// - 부족하면 아무 일도 하지 않고 실패 반환
    /// </summary>
    public bool TryBuildTower(GameObject turretPrefab)
    {
        // 이미 타워가 설치된 상태면 실패
        if (!CanBuild())
        {
            Debug.Log("Foundation : 이미 타워가 설치되어 있습니다.");
            return false;
        }

        // Adapter를 통해 설치 비용 조회
        if (!TurretGradeAdapter.TryGetBuildCost(turretPrefab, out int buildCost))
        {
            Debug.LogError("Foundation : 타워 비용 조회 실패");
            return false;
        }

        // 골드 부족 체크 (DataManager는 수정 안 함)
        if (DataManager.Instance.PlayerGold < buildCost)
        {
            Debug.Log($"골드 부족! 필요:{buildCost}, 보유:{DataManager.Instance.PlayerGold}");
            return false;
        }

        // 골드 차감
        DataManager.Instance.PlayerGold -= buildCost;
        
        Transform spawnTransform = buildPoint != null ? buildPoint : transform;

        // 실제 타워 설치
        GameObject turretObj = Instantiate(
            turretPrefab,
            transform.position,
            Quaternion.identity
        );

        builtTower = turretObj;
        BuiltTurret = turretObj.GetComponent<Turret>();

        Debug.Log($"타워 설치 성공! 남은 골드: {DataManager.Instance.PlayerGold}");
        return true;
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
        BuiltTurret = null;
    }
}
