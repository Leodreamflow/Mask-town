using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{

    public GameObject menu;

    public void TogglePanelVisibility()
    {
        // 切换Panel的激活状态
        if (menu != null)
        {
            menu.SetActive(!menu.activeSelf);
        }
    }
}
