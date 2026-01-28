using UnityEngine;

// Player의 자식으로 동작하는 RTS 쿼터뷰 카메라 컨트롤러
public class CameraRigController : MonoBehaviour
{
    
    [Header("카메라 줌")]
    [SerializeField] private float minDistance = 3f;
    [SerializeField] private float maxDistance = 10f;
    [SerializeField] private float zoomSpeed = 2f;

    [Header("카메라 공전")]
    [SerializeField] private Transform playerTarget;
    [SerializeField] private float rotateSpeed = 120f;

    [Header("카메라 거리")]
    [SerializeField] private float followDistance = 6f;
    [SerializeField] private float followHeight = 2f;

    private float currentYaw;
    private float currentPitch = 20f;

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
        HandleOrbit();
        HandleZoom();
        
    }

    private void LateUpdate()
    {
        if (playerTarget == null)
            return;

        // 회전값으로 방향 계산
        Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0f);

        // 거리 유지한 위치 계산
        Vector3 offset = rotation * new Vector3(0f, 0f, -followDistance);
        Vector3 targetPos = playerTarget.position + Vector3.up * followHeight;

        transform.position = targetPos + offset;

        // 항상 플레이어를 바라본다
        transform.LookAt(targetPos);
    }


    private void HandleOrbit()
    {
        if (!controlStateManager.CanLook)
            return;

        if (Cursor.lockState != CursorLockMode.Locked)
            return;

        if (playerTarget == null)
            return;

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        currentYaw += mouseX * rotateSpeed * Time.deltaTime;
        currentPitch -= mouseY * rotateSpeed * Time.deltaTime;
        currentPitch = Mathf.Clamp(currentPitch, 10f, 70f);

        // 항상 플레이어를 바라보게 한다
        transform.LookAt(playerTarget);
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

    

    

    // 마우스 휠로 카메라 줌을 처리한다
    private void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll == 0f)
            return;

        followDistance -= scroll * zoomSpeed;
        followDistance = Mathf.Clamp(followDistance, minDistance, maxDistance);
    }

}