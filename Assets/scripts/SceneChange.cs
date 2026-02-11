
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [Header("目标场景名（需加入 Build Settings）")]
    public string targetSceneName;

    public void LoadScene()
    {
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            SceneManager.LoadScene(targetSceneName,LoadSceneMode.Single);
        }
        else
        {
            Debug.LogError("没有设置目标场景名！");
        }
    }
}