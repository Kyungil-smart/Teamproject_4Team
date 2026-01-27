using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRigController : MonoBehaviour
{
    [Header("이동 설정")]
    [SerializeField] private float moveSpeed = 10f;

    [Header("맵 경계")]
    [SerializeField] private Vector2 minBounds;
    [SerializeField] private Vector2 maxBounds;

    [Header("상태 관리자")]
    [SerializeField] private ControlStateManager controlStateManager;

    private void Update()
    {
        if (controlStateManager == null)
        {
            Debug.LogError("ControlStateManager 연결 안 됨");
            return;
        }

        Debug.Log("현재 상태: " + controlStateManager.CurrentState);

        if (controlStateManager.CurrentState != ControlStateManager.ControlState.TowerUI)
            return;

        HandleEdgeScroll();
        // 게임 플레이 상태가 아니면 카메라 이동 불가
        if (controlStateManager.CurrentState != ControlStateManager.ControlState.TowerUI)
            return;

        HandleMousePan();
    }

    private void HandleEdgeScroll()
    {
        Vector3 mousePos = Input.mousePosition;
        Debug.Log($"MousePos: {mousePos}");

    }
    private void HandleMousePan()
    {
        // 마우스 이동량(델타) 입력 받기
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // 마우스를 안 움직였으면 아무 것도 안 함
        if (mouseX == 0f && mouseY == 0f)
            return;

        // 월드 기준 이동 방향 계산
        Vector3 moveDirection = new Vector3(mouseX, 0f, mouseY);

        // 위치 계산
        Vector3 newPosition = transform.position;
        newPosition += moveDirection * moveSpeed * Time.deltaTime;

        // 맵 경계 제한
        newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x);
        newPosition.z = Mathf.Clamp(newPosition.z, minBounds.y, maxBounds.y);

        // 적용
        transform.position = newPosition;
    }
}
