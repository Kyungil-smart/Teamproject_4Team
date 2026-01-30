using UnityEngine;

/// <summary>
/// 플레이어 앞쪽으로 Ray를 쏘아
/// - 어떤 오브젝트를 바라보고 있는지(Hover)
/// - F 키로 상호작용
/// - 설치 모드 진입 / 취소
/// 를 관리하는 스크립트
///
/// Ray의 거리 / 방향 / 높이는 여기서만 관리한다.
/// </summary>
public class TileRaycaster : MonoBehaviour
{
    // =========================
    // Ray 설정값
    // =========================

    // Ray가 얼마나 멀리까지 나갈지
    [SerializeField] private float rayDistance = 2f;

    // Ray 시작 지점을 캐릭터 발밑이 아닌
    // 약간 위에서 시작하기 위한 높이 보정값
    [SerializeField] private float rayHeightOffset = 0.5f;

    // Ray가 감지할 레이어 (타일, 설치 포인트 등)
    [SerializeField] private LayerMask tileLayer;

    // 
    private GameObject previousHoverObject;

    // 외부에서 Ray 거리 정보를 읽기만 할 수 있도록 공개
    public float RayDistance => rayDistance;
    public float RayHeightOffset => rayHeightOffset;

    // =========================
    // Ray 결과 상태
    // =========================

    // 현재 Ray에 맞고 있는 오브젝트 (Hover 대상)
    public GameObject CurrentHoverObject { get; private set; }

    // 상호작용으로 선택된 오브젝트
    public GameObject SelectedObject { get; private set; }

    // Raycast 결과를 저장할 변수
    private RaycastHit hit;

    // 플레이어 상호작용 상태
    private PlayerInteractionState currentState = PlayerInteractionState.Normal;

    // TileRaycaster에 UIcontroller 참조 추가
    [SerializeField] private UIcontroller uiController;

    // =========================
    // Unity Update
    // =========================

    private void Update()
    {
        // 1. 매 프레임 Ray를 쏴서 바라보는 대상 갱신
        HandleRaycast();

        // 2. F 키 상호작용 입력 처리
        HandleInteractInput();

        // 3. 설치 취소 입력 처리 (ESC)
        HandleCancelInput();
    }

    // =========================
    // Raycast 처리
    // =========================

    /// <summary>
    /// 플레이어 앞쪽으로 Ray를 쏘아
    /// 현재 바라보고 있는 오브젝트를 찾는다.
    /// </summary>
    private void HandleRaycast()
    {
        Vector3 rayOrigin = transform.position + Vector3.up * rayHeightOffset;
        Ray ray = new Ray(rayOrigin, transform.forward);

        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);

        GameObject newHoverObject = null;

        if (Physics.Raycast(ray, out hit, rayDistance, tileLayer))
        {
            newHoverObject = hit.collider.gameObject;
        }

        // Hover 대상이 바뀌었을 때만 처리
        if (previousHoverObject != newHoverObject)
        {
            // 이전 대상 Hover 해제
            if (previousHoverObject != null &&
                previousHoverObject.TryGetComponent<IHoverable>(out var prevHover))
            {
                prevHover.OnHoverExit();
            }

            // 새로운 대상 Hover 진입
            if (newHoverObject != null &&
                newHoverObject.TryGetComponent<IHoverable>(out var newHover))
            {
                newHover.OnHoverEnter();
            }

            previousHoverObject = newHoverObject;
        }

        CurrentHoverObject = newHoverObject;
    }


    // =========================
    // F 키 상호작용 처리
    // =========================

    /// <summary>
    /// F 키를 눌렀을 때
    /// 현재 Hover 중인 대상과 상호작용을 시도한다.
    /// </summary>
    private void HandleInteractInput()
    {
        // 설치 모드가 아닐 때만 상호작용 가능
        if (currentState != PlayerInteractionState.Normal)
            return;

        // F 키가 눌리지 않았으면 처리 안 함
        if (!Input.GetKeyDown(KeyCode.F))
            return;

        // 바라보고 있는 대상이 없을 때
        if (CurrentHoverObject == null)
        {
            Debug.Log("상호작용할 대상이 없습니다.");
            return;
        }

        // 대상은 있지만 IPlayerInteractable이 없을 때
        if (!CurrentHoverObject.TryGetComponent<IPlayerInteractable>(out var interactable))
        {
            Debug.Log("이 대상은 상호작용할 수 없습니다.");
            return;
        }

        // 정상 상호작용
        SelectedObject = CurrentHoverObject;

        // 대상에게 상호작용 이벤트 전달
        interactable.OnPlayerInteract();

        // 설치 모드 진입
        EnterBuildMode();
    }

    // =========================
    // 설치 취소 처리
    // =========================

    /// <summary>
    /// 설치 모드 중 ESC 키를 누르면
    /// 설치를 취소하고 원래 상태로 복귀한다.
    /// </summary>
    private void HandleCancelInput()
    {
        // 설치 모드일 때만 취소 가능
        if (currentState != PlayerInteractionState.BuildMode)
            return;

        // ESC 키가 아니면 무시
        if (!Input.GetKeyDown(KeyCode.Escape))
            return;

        CancelBuildMode();
    }

    // =========================
    // 상태 전환 처리
    // =========================

    /// <summary>
    /// 설치 모드 진입
    /// - 마우스 락 해제
    /// </summary>
    private void EnterBuildMode()
    {
        currentState = PlayerInteractionState.BuildMode;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (uiController != null)
            uiController.OpenTowerSelection();

        Debug.Log("▶ 설치 모드 진입 (락 해제 + 타워 선택 UI 오픈)");
    }

    /// <summary>
    /// 설치 모드 취소
    /// - 설치 취소 호출
    /// - 마우스 다시 락온
    /// </summary>
    private void CancelBuildMode()
    {
        currentState = PlayerInteractionState.Normal;

        // 선택된 오브젝트가 설치 취소를 지원하면 호출
        if (SelectedObject != null &&
            SelectedObject.TryGetComponent<IBuildCancelable>(out var cancelable))
        {
            cancelable.CancelBuild();
        }

        SelectedObject = null;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (uiController != null)
            uiController.CloseTowerSelection();

        Debug.Log("◀ 설치 모드 취소 (락온 + 타워 선택 UI 닫힘)");
    }

    // =========================
    // Gizmos (씬 뷰 디버깅)
    // =========================

    private void OnDrawGizmos()
    {
        // Ray 표시
        Gizmos.color = Color.red;

        Vector3 rayOrigin = transform.position + Vector3.up * rayHeightOffset;
        Vector3 rayEnd = rayOrigin + transform.forward * rayDistance;

        Gizmos.DrawLine(rayOrigin, rayEnd);
        Gizmos.DrawLine(rayOrigin + transform.right * 0.05f, rayEnd + transform.right * 0.05f);
        Gizmos.DrawLine(rayOrigin - transform.right * 0.05f, rayEnd - transform.right * 0.05f);

        // Hover 대상 표시
        if (CurrentHoverObject != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(CurrentHoverObject.transform.position, Vector3.one);
        }

        // 선택된 대상 표시
        if (SelectedObject != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(SelectedObject.transform.position, Vector3.one * 1.2f);
        }
    }
}

/// <summary>
/// 플레이어 상호작용 상태
/// </summary>
public enum PlayerInteractionState
{
    Normal,     // 기본 상태 (마우스 락온)
    BuildMode   // 설치 모드 (마우스 자유)
}
