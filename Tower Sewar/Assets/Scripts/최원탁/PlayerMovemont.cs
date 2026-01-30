using UnityEngine;

// 카메라가 보고 있는 방향을 기준으로 플레이어를 이동시키는 스크립트
public class PlayerMovement : MonoBehaviour
{
    // ===============================
    // Collision (구조물 충돌)
    // ===============================
    [Header("Collision")]
    [SerializeField] private LayerMask obstacleLayer;     // 플레이어가 막혀야 하는 레이어
    [SerializeField] private float collisionRadius = 0.45f; // 캡슐 반지름 (작은 구조물 통과 방지)
    [SerializeField] private float collisionHeight = 1.8f;  // 캡슐 높이 (플레이어 키)

    // ===============================
    // Movement
    // ===============================
    [SerializeField] private float moveSpeed = 5f;

    // ===============================
    // Map Boundary
    // ===============================
    [Header("Map Boundary")]
    [SerializeField] private Collider mapBoundary;        // 맵 이동 가능 영역

    // ===============================
    // References
    // ===============================
    private Animator animator;
    private ControlStateManager controlStateManager;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        controlStateManager = FindObjectOfType<ControlStateManager>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        // -------------------------------------------------
        // 이동 가능 상태 체크
        // -------------------------------------------------
        if (controlStateManager == null || !controlStateManager.CanMove)
        {
            animator.SetBool("IsRunning", false);
            return;
        }

        if (Cursor.lockState != CursorLockMode.Locked)
        {
            animator.SetBool("IsRunning", false);
            return;
        }

        // -------------------------------------------------
        // 입력 처리
        // -------------------------------------------------
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 input = new Vector3(h, 0f, v);

        if (input == Vector3.zero)
        {
            animator.SetBool("IsRunning", false);
            return;
        }

        input.Normalize();

        // -------------------------------------------------
        // 카메라 기준 이동 방향 계산
        // -------------------------------------------------
        Transform cam = Camera.main.transform;

        Vector3 camForward = new Vector3(cam.forward.x, 0f, cam.forward.z).normalized;
        Vector3 camRight = new Vector3(cam.right.x, 0f, cam.right.z).normalized;

        Vector3 moveDir = camForward * input.z + camRight * input.x;

        // -------------------------------------------------
        // 이동량 계산
        // -------------------------------------------------
        Vector3 moveDelta = moveDir * moveSpeed * Time.deltaTime;

        // -------------------------------------------------
        // Capsule 기준점 설정 (반드시 먼저!)
        // -------------------------------------------------
        Vector3 capsuleBottom = transform.position + Vector3.up * 0.1f;   // 발 근처
        Vector3 capsuleTop = capsuleBottom + Vector3.up * collisionHeight; // 머리 근처

        // -------------------------------------------------
        // 이미 구조물과 겹쳐 있는지 검사 (터널링 방지)
        // -------------------------------------------------
        bool overlapping = Physics.CheckCapsule(
            capsuleBottom,
            capsuleTop,
            collisionRadius,
            obstacleLayer
        );

        if (overlapping)
        {
            // 끼임 탈출용: 아주 약하게 반대 방향으로 밀어냄
            Vector3 pushOutDir = -moveDir.normalized;

            // 너무 세게 밀지 않도록 아주 작은 값
            float pushOutDistance = 0.05f;

            Vector3 escapePos = transform.position + pushOutDir * pushOutDistance;

            // 맵 경계는 지켜준다
            if (mapBoundary != null)
            {
                Bounds b = mapBoundary.bounds;
                escapePos.x = Mathf.Clamp(escapePos.x, b.min.x, b.max.x);
                escapePos.z = Mathf.Clamp(escapePos.z, b.min.z, b.max.z);
            }

            transform.position = escapePos;

            animator.SetBool("IsRunning", false);
            return;
        }


        // -------------------------------------------------
        // X / Z 축 분리 이동 (벽 미끄러짐 구현)
        // -------------------------------------------------
        Vector3 finalMove = Vector3.zero;

        // X 방향 검사
        Vector3 xMove = new Vector3(moveDelta.x, 0f, 0f);
        if (xMove != Vector3.zero)
        {
            bool xBlocked = Physics.CapsuleCast(
                capsuleBottom,
                capsuleTop,
                collisionRadius,
                xMove.normalized,
                Mathf.Abs(xMove.x),
                obstacleLayer
            );

            if (!xBlocked)
                finalMove.x = xMove.x;
        }

        // Z 방향 검사
        Vector3 zMove = new Vector3(0f, 0f, moveDelta.z);
        if (zMove != Vector3.zero)
        {
            bool zBlocked = Physics.CapsuleCast(
                capsuleBottom,
                capsuleTop,
                collisionRadius,
                zMove.normalized,
                Mathf.Abs(zMove.z),
                obstacleLayer
            );

            if (!zBlocked)
                finalMove.z = zMove.z;
        }

        // -------------------------------------------------
        // 최종 위치 계산
        // -------------------------------------------------
        Vector3 nextPos = transform.position + finalMove;

        // -------------------------------------------------
        // 맵 경계 제한
        // -------------------------------------------------
        if (mapBoundary != null)
        {
            Bounds b = mapBoundary.bounds;
            nextPos.x = Mathf.Clamp(nextPos.x, b.min.x, b.max.x);
            nextPos.z = Mathf.Clamp(nextPos.z, b.min.z, b.max.z);
        }

        // -------------------------------------------------
        // 위치 적용
        // -------------------------------------------------
        transform.position = nextPos;

        // -------------------------------------------------
        // 회전 & 애니메이션
        // -------------------------------------------------
        transform.rotation = Quaternion.LookRotation(moveDir);
        animator.SetBool("IsRunning", true);
    }
}
