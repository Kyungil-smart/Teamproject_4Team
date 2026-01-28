using UnityEngine;

// 플레이어의 키보드 이동과 이동 애니메이션을 담당하는 클래스
public class PlayerMovement : MonoBehaviour
{
    // 이동 애니메이션을 제어하기 위한 Animator
    private Animator _animator;

    // 플레이어 이동 속도
    [SerializeField] private float _moveSpeed;

    // ControlStateManager 참조
    // 현재 플레이어 이동이 가능한 상태인지 확인하기 위해 사용
    private ControlStateManager _controlStateManager;

    private void Awake()
    {
        // Animator 컴포넌트를 가져온다
        _animator = GetComponent<Animator>();

        // 씬에 존재하는 ControlStateManager를 찾는다
        _controlStateManager = FindObjectOfType<ControlStateManager>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        // 현재 상태에서 플레이어 이동이 불가능하면 처리하지 않는다
        if (!_controlStateManager.CanMove)
        {
            _animator.SetBool("IsRunning", false);
            return;
        }

        // 마우스가 락 상태가 아니라면 플레이어 이동을 막는다
        // (UI 조작 중이거나 ESC로 락 해제된 상황)
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            _animator.SetBool("IsRunning", false);
            return;
        }

        // 키보드 입력값을 받는다
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // 이동 벡터를 생성한다
        Vector3 movement = new Vector3(horizontal, 0f, vertical);

        // 입력이 없다면 이동 애니메이션을 끈다
        if (movement == Vector3.zero)
        {
            _animator.SetBool("IsRunning", false);
            return;
        }
        // 대각선 이동 시 속도가 빨라지는 문제를 방지한다
        movement.Normalize();

        // 이동 방향을 바라보도록 플레이어를 회전시킨다
        transform.rotation = Quaternion.LookRotation(movement);

        // 플레이어를 전방으로 이동시킨다
        transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);

        // 이동 애니메이션을 켠다
        _animator.SetBool("IsRunning", true);
    }
}