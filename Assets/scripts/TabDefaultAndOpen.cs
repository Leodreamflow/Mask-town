using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class TabDefaultAndOpen : MonoBehaviour
{
    [Header("Default")]
    [SerializeField] private Button defaultButton;

    private GameObject lastSelected;

    void Start()
    {
        StartCoroutine(Init());
    }

    IEnumerator Init()
    {
        yield return null; // 等 UI 初始化完（非常关键）

        // 1. 设为 Selected
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(defaultButton.gameObject);

        // 2. 手动触发它的点击逻辑（打开内容）
        defaultButton.onClick.Invoke();

        lastSelected = defaultButton.gameObject;
    }

    void LateUpdate()
    {
        if (EventSystem.current == null) return;

        var current = EventSystem.current.currentSelectedGameObject;

        // 有新的选中 → 记录
        if (current != null)
        {
            lastSelected = current;
        }
        // 选中被清空 → 恢复
        else if (lastSelected != null)
        {
            EventSystem.current.SetSelectedGameObject(lastSelected);
        }
    }
}