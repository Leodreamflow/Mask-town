using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TabManager : MonoBehaviour
{
    [Header("一一对应，顺序必须一致")]
    [SerializeField] private Button[] buttons;
    [SerializeField] private GameObject[] pages;

    [Header("启动默认")]
    [SerializeField] private int defaultIndex = 0;

    private void Awake()
    {
        // 基本安全检查
        if (buttons == null || pages == null || buttons.Length == 0 || pages.Length == 0)
        {
            Debug.LogError("[TabManager] buttons/pages 没有设置。");
            return;
        }
        if (buttons.Length != pages.Length)
        {
            Debug.LogError("[TabManager] buttons 和 pages 数量不一致！");
        }
    }

    private void Start()
    {
        SelectTab(defaultIndex);
    }

    /// <summary>
    /// 方式A：OnClick 传 Button（最不容易填错）
    /// </summary>
    public void SelectTabByButton(Button clicked)
    {
        int index = System.Array.IndexOf(buttons, clicked);
        if (index < 0)
        {
            Debug.LogError("[TabManager] 这个 Button 不在 buttons 数组里：" + clicked.name);
            return;
        }
        SelectTab(index);
    }

    /// <summary>
    /// 方式B：OnClick 传 int（也保留给你用）
    /// </summary>
    public void SelectTab(int index)
    {
        if (buttons == null || pages == null) return;
        if (index < 0 || index >= buttons.Length) return;

        int count = Mathf.Min(buttons.Length, pages.Length);

        for (int i = 0; i < count; i++)
        {
            bool isCurrent = (i == index);

            // 1) 显示 / 隐藏页面
            if (pages[i] != null)
                pages[i].SetActive(isCurrent);

            // 2) 让“选中按钮”变亮（Selected）
            if (isCurrent && EventSystem.current != null && buttons[i] != null)
                EventSystem.current.SetSelectedGameObject(buttons[i].gameObject);
        }
    }
}