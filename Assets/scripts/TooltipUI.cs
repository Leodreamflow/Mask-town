using UnityEngine;
using TMPro;

public class TooltipUI : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public Canvas canvas;                 // 你的主 Canvas
    public RectTransform tooltipRect;     // Tooltip 根对象 RectTransform
    public TextMeshProUGUI tooltipText;   // TMP 文本（Text (TMP)）

    [Header("Follow Mouse")]
    public Vector2 offset = new Vector2(16f, -16f);

    private Camera uiCamera;

    void Awake()
    {
        // 保险：开局强制隐藏（同时你也要在Hierarchy里把它设为Inactive）


        if (tooltipRect == null) tooltipRect = GetComponent<RectTransform>();

        if (canvas != null && canvas.renderMode != RenderMode.ScreenSpaceOverlay)
            uiCamera = canvas.worldCamera;
        else
            uiCamera = null;
    }

    void Update()
    {
        // 只有显示时才会执行 Update（因为隐藏时对象 inactive，Update 不跑）
        FollowMouse();
    }

    void FollowMouse()
    {
        if (canvas == null) return;

        RectTransform canvasRect = canvas.transform as RectTransform;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            Input.mousePosition,
            uiCamera,
            out Vector2 localPoint
        );

        tooltipRect.anchoredPosition = localPoint + offset;
    }

    public void Show(string message)
    {
        if (tooltipText != null) tooltipText.text = message;
        gameObject.SetActive(true);
        FollowMouse(); // 立刻定位一次，避免第一帧跳动
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}