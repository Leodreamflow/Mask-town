using UnityEngine;

public class BackButton : MonoBehaviour
{
    public void GoBack()
    {
        if (SceneBackStack.Instance == null)
        {
            Debug.LogError("SceneBackStack.Instance is null. 场景里没有 SceneBackStackGO 或没运行起来。");
            return;
        }

        SceneBackStack.Instance.GoBack();
    }
}