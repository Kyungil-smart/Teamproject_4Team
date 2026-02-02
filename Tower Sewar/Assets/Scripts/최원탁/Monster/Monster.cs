using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private GameObject healthUI; // 머리 위 체력바

    private void Awake()
    {
        // 시작 시 항상 꺼둔다
        if (healthUI != null)
            healthUI.SetActive(false);
    }

    public void ShowHealthUI()
    {
        if (healthUI != null)
            healthUI.SetActive(true);
    }

    public void HideHealthUI()
    {
        if (healthUI != null)
            healthUI.SetActive(false);
    }

    // 나중에 사용 (죽었는지 체크용)
    public bool IsDead { get; private set; }
}
