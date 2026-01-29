using UnityEngine;

public class HoverOutline : MonoBehaviour, IHoverable
{
    [SerializeField] private Behaviour outline;

    private void Awake()
    {
        // outline 컴포넌트가 없으면 아무 것도 하지 않음
        if (outline != null)
            outline.enabled = false;
    }

    public void OnHoverEnter()
    {
        if (outline == null)
            return;
        Debug.Log("Hover ENTER: " + gameObject.name);
        outline.enabled = true;
    }

    public void OnHoverExit()
    {
        if (outline == null)
            return;
        Debug.Log("Hover EXIT: " + gameObject.name);
        outline.enabled = false;
    }
}
