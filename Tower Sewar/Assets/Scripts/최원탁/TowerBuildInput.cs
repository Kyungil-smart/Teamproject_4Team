using UnityEngine;

// 타일 레이 설정을 공유받아
// 고스트와 상호작용 성공 시에만 설치모드에 진입하는 스크립트
public class TowerBuildInput : MonoBehaviour
{
    [Header("참조")]
    [SerializeField] private TileRaycaster tileRaycaster;
    [SerializeField] private GameObject ghostPrefab;

    private GameObject ghostInstance;
    private bool isBuildMode = false;

    private void Update()
    {
        HandleInteractionInput();
        HandleCancelInput();
        UpdateGhost();
    }

    // =========================
    // 입력 처리
    // =========================

    // F 키 : 상호작용 시도
    private void HandleInteractionInput()
    {
        if (!Input.GetKeyDown(KeyCode.F))
            return;

        // 이미 설치모드면 무시
        if (isBuildMode)
            return;

        // 고스트와 상호작용 성공했을 때만
        if (!IsInteractingWithGhost())
        {
            Debug.Log("상호작용할 수 있는 대상이 없습니다.");
            return;
        }

        EnterBuildMode();
    }

    // ESC 키 : 설치모드일 때만 취소
    private void HandleCancelInput()
    {
        if (!isBuildMode)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitBuildMode();
        }
    }

    // =========================
    // 설치 모드 제어
    // =========================

    private void EnterBuildMode()
    {
        isBuildMode = true;

        if (ghostInstance == null)
        {
            ghostInstance = Instantiate(ghostPrefab);
        }

        ghostInstance.SetActive(true);

        // 마우스 락 해제
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Debug.Log("설치모드 진입");
    }

    private void ExitBuildMode()
    {
        isBuildMode = false;

        if (ghostInstance != null)
        {
            ghostInstance.SetActive(false);
        }

        // 마우스 락 복구 추가
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Debug.Log("설치모드 취소");
    }

    // =========================
    // 고스트 표시 업데이트
    // =========================

    private void UpdateGhost()
    {
        if (!isBuildMode)
            return;

        if (tileRaycaster.SelectedObject == null)
        {
            ghostInstance.SetActive(false);
            return;
        }

        Tile tile = tileRaycaster.SelectedObject.GetComponent<Tile>();
        if (tile == null)
        {
            ghostInstance.SetActive(false);
            return;
        }

        ghostInstance.SetActive(true);

        // 타일 위에 고스트 배치
        ghostInstance.transform.position =
            tile.transform.position + Vector3.up * 0.5f;

        // 설치 가능 / 불가 색상
        Renderer renderer = ghostInstance.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color =
                tile.CanBuild ? Color.green : Color.red;
        }
    }

    // =========================
    // 레이 상호작용 판정
    // =========================

    // TileRaycaster의 레이 설정을 그대로 사용
    private bool IsInteractingWithGhost()
    {
        int ghostLayerMask = LayerMask.GetMask("Ghost");

        float rayDistance = tileRaycaster.RayDistance;
        float rayHeightOffset = tileRaycaster.RayHeightOffset;

        Vector3 rayOrigin =
            transform.position + Vector3.up * rayHeightOffset;

        Ray ray = new Ray(rayOrigin, transform.forward);
        RaycastHit hit;

        Debug.DrawRay(rayOrigin, transform.forward * rayDistance, Color.red, 0.1f);

        if (!Physics.Raycast(ray, out hit, rayDistance, ghostLayerMask))
            return false;

        // 레이어 기준 판정 (프리팹 구조 안전)
        return hit.collider.gameObject.layer ==
               LayerMask.NameToLayer("Ghost");
    }
}
