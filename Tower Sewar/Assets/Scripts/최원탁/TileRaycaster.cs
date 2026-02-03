using UnityEngine;

public class TileRaycaster : MonoBehaviour
{
    [Header("Ray Settings")]
    [SerializeField] private float rayDistance = 2f;
    [SerializeField] private float rayHeightOffset = 0.5f;
    [SerializeField] private LayerMask tileLayer;

    private GameObject previousHoverObject;

    public GameObject CurrentHoverObject { get; private set; }
    public GameObject SelectedObject { get; private set; }

    private RaycastHit hit;

    private Turret selectedTurret;
    public Turret SelectedTurret => selectedTurret;

    [SerializeField] private UIcontroller uiController;

    [SerializeField] private GameObject[] turretPrefab;


    // =========================
    // 상태 플래그 (기존 구조 유지)
    // =========================
    private bool isBuildMode = false;          // 설치 모드 여부
    private bool isBuildConfirmUIOpen = false; // 확인 UI 여부
    private bool isUpgradeUIOpen = false; // 업그레이드 여부

    private void Update()
    {
        // Ray / Gizmo 시각 피드백은 항상 유지
        HandleRaycast();

        // 확인 UI가 떠 있으면 키보드 입력 완전 차단
        if (isBuildConfirmUIOpen || isUpgradeUIOpen)
            return;

        // ESC 이후 자유 마우스 상태에서 좌클릭 → 다시 락온
        if (!isBuildMode && Cursor.lockState == CursorLockMode.None)
        {
            if (Input.GetMouseButtonDown(0))
            {
                LockCursor();
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
            HandleInteractInput();

        if (Input.GetKeyDown(KeyCode.Escape))
            HandleCancelInput();
    }

    // =========================
    // Raycast 처리
    // =========================
    private void HandleRaycast()
    {
        Vector3 rayOrigin = transform.position + Vector3.up * rayHeightOffset;
        Ray ray = new Ray(rayOrigin, transform.forward);

        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);

        GameObject newHoverObject = null;

        if (Physics.Raycast(ray, out hit, rayDistance, tileLayer))
            newHoverObject = hit.collider.gameObject;

        if (previousHoverObject != newHoverObject)
        {
            if (previousHoverObject != null &&
                previousHoverObject.TryGetComponent<IHoverable>(out var prevHover))
                prevHover.OnHoverExit();

            if (newHoverObject != null &&
                newHoverObject.TryGetComponent<IHoverable>(out var newHover))
                newHover.OnHoverEnter();

            previousHoverObject = newHoverObject;
        }

        CurrentHoverObject = newHoverObject;
    }

    // =========================
    // F 키 상호작용
    // =========================
    private void HandleInteractInput()
    {
        if (isBuildMode)
            return;

        if (CurrentHoverObject == null)
            return;

        // ===============================
        // ⭐ 설치된 타워만 → 업그레이드 UI
        // ===============================
        if (CurrentHoverObject.TryGetComponent<Foundation>(out var foundation))
        {
            // Foundation에 이미 타워가 설치된 경우만
            if (!foundation.CanBuild())
            {
                Debug.Log("이미 설치됨");
                selectedTurret = foundation.BuiltTurret;

                isUpgradeUIOpen = true;

                UnlockCursor();

                if (uiController != null)
                    uiController.OpenUpgradeConfirmUI();

                return;
            }
        }

        if (!CurrentHoverObject.TryGetComponent<IPlayerInteractable>(out var interactable))
            return;

        SelectedObject = CurrentHoverObject;
        interactable.OnPlayerInteract();

        EnterBuildMode();
    }

    // =========================
    // ESC 취소
    // =========================
    private void HandleCancelInput()
    {
        if (!isBuildMode)
            return;

        ExitBuildMode();
    }

    // =========================
    // 설치 모드 진입
    // =========================
    private void EnterBuildMode()
    {
        isBuildMode = true;

        UnlockCursor();

        if (uiController != null)
            uiController.OpenTowerSelection();
    }

    // =========================
    // UI → Raycaster
    // =========================
    public void OnTowerSelectedFromUI()
    {
        isBuildConfirmUIOpen = true;

        if (uiController != null)
            uiController.OpenBuildConfirmUI();
    }

    public void ConfirmBuildFromUI(int tower)
    {
        Debug.Log("✔ 타워 설치 확정");

        if (SelectedObject == null)
        {
            Debug.LogWarning("선택된 타일이 없습니다.");
            ExitBuildConfirm();
            return;
        }

        if (!SelectedObject.TryGetComponent<Foundation>(out var foundation))
        {
            Debug.LogWarning("SelectedObject에 Foundation이 없습니다.");
            ExitBuildConfirm();
            return;
        }

        foundation.BuildTower(turretPrefab[tower]);

        ExitBuildConfirm(); // 모든 처리 끝난 뒤
    }


    public void CancelBuildFromUI()
    {
        Debug.Log("✖ 타워 설치 취소");

        ExitBuildConfirm();
    }

    // =========================
    // 업그레이드 UI → Raycaster
    // =========================
    public void OnUpgradeConfirm()
    {
        Debug.Log("▶ 업그레이드 CONFIRM 처리");

        selectedTurret.Upgrade();

        selectedTurret = null;
        CloseUpgradeUI();
    }

    public void OnUpgradeCancel()
    {
        Debug.Log("▶ 업그레이드 CANCEL 처리");

        CloseUpgradeUI();
    }

    private void CloseUpgradeUI()
    {
        isUpgradeUIOpen = false;

        // 업그레이드 UI 끄기
        if (uiController != null)
            uiController.CloseUpgradeConfirmUI();

        // 상태 정리
        isBuildMode = false;
        isBuildConfirmUIOpen = false;
        SelectedObject = null;

        // 커서 복구
        LockCursor();
    }


    private void ExitBuildConfirm()
    {
        isBuildConfirmUIOpen = false;
        ExitBuildMode();
    }

    // =========================
    // 설치 모드 종료
    // =========================
    private void ExitBuildMode()
    {
        isBuildMode = false;

        // ❗ 설치 확정이 끝난 경우에만 정리
        if (!isBuildConfirmUIOpen)
            SelectedObject = null;

        if (uiController != null)
        {
            uiController.CloseBuildConfirmUI();
            uiController.CloseTowerSelection();
        }

        LockCursor();
    }

    // =========================
    // 커서 제어
    // =========================
    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // =========================
    // Gizmos (뚜렷하게 개선)
    // =========================
    private void OnDrawGizmos()
    {
        Vector3 rayOrigin = transform.position + Vector3.up * rayHeightOffset;
        Vector3 rayEnd = rayOrigin + transform.forward * rayDistance;

        Gizmos.color = Color.red;

        float thickness = 0.05f; // ⭐ 기즈모 두께

        // 중앙
        Gizmos.DrawLine(rayOrigin, rayEnd);
        // 좌우
        Gizmos.DrawLine(rayOrigin + transform.right * thickness, rayEnd + transform.right * thickness);
        Gizmos.DrawLine(rayOrigin - transform.right * thickness, rayEnd - transform.right * thickness);
        // 상하
        Gizmos.DrawLine(rayOrigin + transform.up * thickness, rayEnd + transform.up * thickness);
        Gizmos.DrawLine(rayOrigin - transform.up * thickness, rayEnd - transform.up * thickness);

        if (CurrentHoverObject != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(CurrentHoverObject.transform.position, Vector3.one);
        }

        if (SelectedObject != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(SelectedObject.transform.position, Vector3.one * 1.2f);
        }
    }
}
