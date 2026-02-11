using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [TextArea] public string message;
    public TooltipUI tooltip; // 拖拽场景里的 Tooltip(带TooltipUI脚本的那个)

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tooltip != null)
            tooltip.Show(message);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tooltip != null)
            tooltip.Hide();
    }
}