using UnityEngine;

// 워크래프트3 / RTS 스타일 쿼터뷰 카메라 컨트롤러
// 마우스로 카메라 "회전"이 아니라 "이동"을 처리한다
public class CameraRigController : MonoBehaviour
{
    // 마우스 이동에 따른 카메라 이동 속도
    [SerializeField] private float panSpeed = 10f;

    // ControlStateManager 참조
    private ControlStateManager controlStateManager;

    private void Awake()
    {
        // 씬에 존재하는 ControlStateManager를 찾는다
        controlStateManager = FindObjectOfType<ControlStateManager>();
    }

    private void Update()
    {
        HandleMouseLockInput();
        HandleCameraPan();
    }

    // 마우스 클릭 / ESC 입력으로 마우스 락 상태를 관리한다
    private void HandleMouseLockInput()
    {
        // 마우스 왼쪽 버튼을 누르면 마우스를 락한다
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // ESC 키를 누르면 마우스 락을 해제한다
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    // 마우스 이동에 따라 카메라를 이동시키는 로직
    private void HandleCameraPan()
    {
        // 카메라 회전이 가능한 상태가 아니면 처리하지 않는다
        if (!controlStateManager.CanLook)
            return;

        // 마우스가 락 상태가 아니면 카메라 이동을 하지 않는다
        if (Cursor.lockState != CursorLockMode.Locked)
            return;

        // 마우스 이동 값 가져오기
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // 카메라 기준 오른쪽 방향 벡터
        Vector3 right = transform.right;

        // 카메라 기준 앞 방향 벡터 (Y축 제거해서 수평 이동만)
        Vector3 forward = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;

        // 마우스 이동 방향에 따라 카메라 이동 벡터 계산
        Vector3 move =
            (right * mouseX + forward * mouseY) * panSpeed * Time.deltaTime;

        // CameraRig의 위치를 이동시킨다
        transform.position += move;
    }
}
