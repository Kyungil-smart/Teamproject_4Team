using UnityEngine;

public class InteractableCube : MonoBehaviour, IPlayerInteractable
{
    public void OnPlayerInteract()
    {
        Debug.Log("큐브 상호작용됨");

        // 테스트용: 마우스 락 해제
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
