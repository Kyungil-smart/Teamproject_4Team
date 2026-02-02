using UnityEngine;

/// <summary>
/// 월드 UI를 항상 카메라 정면으로 보이게 하는 Billboard
/// </summary>
public class MonsterHealthUIBillboard : MonoBehaviour
{
    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void LateUpdate()
    {
        if (mainCam == null)
            return;

        // 카메라를 정면으로 바라보게
        transform.forward = mainCam.transform.forward;
    }
}
