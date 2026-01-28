using UnityEngine;

// Player의 자식으로 동작하는 RTS 쿼터뷰 카메라 컨트롤러
public class CameraRigController : MonoBehaviour
{
    [Header("카메라 이동제한 범위")]
    [SerializeField] private float limitLeft = -10f;
    [SerializeField] private float limitRight = 10f;
    [SerializeField] private float limitBack = -8f;
    [SerializeField] private float limitForward = 8f;

    [Header("카메라 줌")]
    [SerializeField] private float zoomSpeed = 10f;
    [SerializeField] private float minZoomY = 5f;
    [SerializeField] private float maxZoomY = 20f;

    // 마우스 이동에 따른 카메라 이동 속도
    [SerializeField] private float panSpeed = 5f;

    // ControlStateManager 참조
    private ControlStateManager controlStateManager;

    // 카메라의 기본 로컬 위치 (플레이어 기준 시점)
    // Player 기준 “정중앙 시점”
    private Vector3 defaultLocalPosition;

    private void Awake()
    {
        // 씬에 하나 있는 ControlStateManager를 찾음
        // 카메라가 “지금 조작 가능한 상태인지” 판단하기 위함
        controlStateManager = FindObjectOfType<ControlStateManager>();

        // 시작 시 카메라의 기본 로컬 위치를 저장한다
        defaultLocalPosition = transform.localPosition;
    }

    private void Update()
    {
        HandleMouseLockInput();
        HandleCameraPan();
        HandleZoom();
        HandleResetByPlayerMove();
    }

    private void HandleMouseLockInput()
    {
        // 마우스 왼쪽 클릭 시
        //마우스를 화면 중앙에 고정
        // 시점 조작 모드 진입
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // 마우스 락 해제
        // UI 조작 가능 상태
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void HandleCameraPan()
    {
        // 게임 시점 조작이 금지된 경우
        // 카메라 입력 차단
        if (!controlStateManager.CanLook)
            return;
        // 마우스가 락상태가 아니면 카메라 이동 안됨
        if (Cursor.lockState != CursorLockMode.Locked)
            return;

        // 마우스 이동량 획득
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // 마우스 안움직이면 이동 방지
        if (mouseX == 0f && mouseY == 0f)
            return;

        // 플레이어 기준 오른쪽 / 앞 방향으로 로컬 이동
        Vector3 move =
            (Vector3.right * mouseX + Vector3.forward * mouseY)
            * panSpeed * Time.deltaTime;
        
        // 로컬 이동 적용
        transform.localPosition += move;

        // 이동 후 위치를 제한 범위 안으로 clamp 한다
        Vector3 clampedPos = transform.localPosition;

        clampedPos.x = Mathf.Clamp(clampedPos.x, limitLeft, limitRight);
        clampedPos.z = Mathf.Clamp(clampedPos.z, limitBack, limitForward);

        // 제한된 위치를 다시 적용
        transform.localPosition = clampedPos;
    }

    private void HandleResetByPlayerMove()
    {
        // 플레이어 이동 입력이 들어오면 카메라를 기본 위치로 복귀
        bool isPlayerMoving =
            Input.GetAxisRaw("Horizontal") != 0f ||
            Input.GetAxisRaw("Vertical") != 0f;

        if (isPlayerMoving)
        {
            transform.localPosition = defaultLocalPosition;
        }
    }

    // 마우스 휠로 카메라 줌을 처리한다
    private void HandleZoom()
    {
        // 마우스 휠 입력값을 가져온다
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        // 입력이 없으면 처리하지 않는다
        if (scroll == 0f)
            return;

        // 현재 로컬 위치 가져오기
        Vector3 pos = transform.localPosition;

        // 휠 입력에 따라 Y값(높이)을 조절한다
        pos.y -= scroll * zoomSpeed;

        // 줌 범위를 제한한다
        pos.y = Mathf.Clamp(pos.y, minZoomY, maxZoomY);

        // 변경된 위치를 다시 적용한다
        transform.localPosition = pos;
    }

}
