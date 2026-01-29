using UnityEngine;

// 카메라가 보고 있는 방향을 "플레이어의 정면"으로 인식해서 이동하는 스크립트
public class PlayerMovement : MonoBehaviour
{
    // 애니메이션
    private Animator _animator;

    // 이동 속도
    [SerializeField] private float _moveSpeed = 5f;

    // 상태 관리
    private ControlStateManager _controlStateManager;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _controlStateManager = FindObjectOfType<ControlStateManager>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        // 이동 불가 상태면 중단
        if (_controlStateManager == null || !_controlStateManager.CanMove)
        {
            _animator.SetBool("IsRunning", false);
            return;
        }

        // 마우스 락이 아닐 때 이동 차단
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            _animator.SetBool("IsRunning", false);
            return;
        }

        // 입력
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 input = new Vector3(h, 0f, v);

        // 입력 없으면 정지
        if (input == Vector3.zero)
        {
            _animator.SetBool("IsRunning", false);
            return;
        }

        // 대각선 속도 보정
        input.Normalize();

        // 카메라의 Y축 회전(Yaw)만 사용
        Transform cam = Camera.main.transform;

        Vector3 camForward = new Vector3(
            cam.forward.x,
            0f,
            cam.forward.z
        ).normalized;

        Vector3 camRight = new Vector3(
            cam.right.x,
            0f,
            cam.right.z
        ).normalized;

        // 카메라 기준 이동 방향
        Vector3 moveDir = camForward * input.z + camRight * input.x;

        // 이동
        transform.position += moveDir * _moveSpeed * Time.deltaTime;

        // 이동 방향으로만 회전
        transform.rotation = Quaternion.LookRotation(moveDir);

        // 애니메이션 ON
        _animator.SetBool("IsRunning", true);
    }
}
