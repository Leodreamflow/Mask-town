using UnityEngine;
using UnityEngine.EventSystems;

public class notes : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler
{
    [Header("Target Image")]
    public RectTransform icon;

    [Header("Hover Move Settings")]
    public float moveUpY = 10f;
    public float smoothSpeed = 12f;

    private Vector2 originPos;
    private Vector2 targetPos;

    void Awake()
    {
        if (icon == null)
        {
            Debug.LogError("notes.cs: Icon is not assigned.");
            enabled = false;
            return;
        }

        originPos = icon.anchoredPosition;
        targetPos = originPos;
    }

    void Update()
    {
        icon.anchoredPosition =
            Vector2.Lerp(icon.anchoredPosition, targetPos, Time.deltaTime * smoothSpeed);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetPos = originPos + Vector2.up * moveUpY;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetPos = originPos;
    }
}
