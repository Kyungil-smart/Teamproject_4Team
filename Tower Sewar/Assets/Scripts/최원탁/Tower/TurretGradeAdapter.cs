using UnityEngine;

public static class TurretGradeAdapter
{
    /// <summary>
    /// 타워 프리팹에서 "설치 비용"을 가져온다
    /// </summary>
    /// <param name="turretPrefab">설치하려는 타워 프리팹</param>
    /// <param name="cost">설치 비용 (out)</param>
    /// <returns>비용 조회 성공 여부</returns>
    public static bool TryGetBuildCost(GameObject turretPrefab, out int cost)
    {
        cost = 0;

        // 프리팹 자체가 없는 경우
        if (turretPrefab == null)
        {
            Debug.LogError("TurretGradeAdapter : turretPrefab이 null입니다.");
            return false;
        }

        // Turret_Grade 컴포넌트 확인
        Turret_Grade grade = turretPrefab.GetComponent<Turret_Grade>();
        if (grade == null)
        {
            Debug.LogError("TurretGradeAdapter : Turret_Grade 컴포넌트가 없습니다.");
            return false;
        }

        // ScriptableObject 데이터 유효성 검사
        if (grade._towerData == null || grade._towerData.Count == 0)
        {
            Debug.LogError("TurretGradeAdapter : towerData가 비어 있습니다.");
            return false;
        }

        // 0번 = 1티어 설치 비용 (규칙은 여기서만 앎)
        cost = grade._towerData[0].TowerBuildCost;
        return true;
    }
}
