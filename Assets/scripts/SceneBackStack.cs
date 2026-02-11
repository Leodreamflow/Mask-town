using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneBackStack : MonoBehaviour
{
    public static SceneBackStack Instance { get; private set; }

    private readonly Stack<string> stack = new Stack<string>();
    private string current;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        current = SceneManager.GetActiveScene().name;
        stack.Push(current);

        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    private void OnDestroy()
    {
        if (Instance == this)
            SceneManager.activeSceneChanged -= OnActiveSceneChanged;
    }

    private void OnActiveSceneChanged(Scene oldScene, Scene newScene)
    {
        // oldScene 是刚离开的那个，newScene 是刚进入的那个
        if (newScene.name == current) return;

        // 如果连续加载同名场景，避免重复压栈
        if (stack.Count == 0 || stack.Peek() != newScene.name)
            stack.Push(newScene.name);

        current = newScene.name;

        Debug.Log($"[SceneBackStack] stack = {string.Join(" -> ", stack)}");
    }

    public bool CanGoBack => stack.Count > 1;

    public void GoBack()
    {
        if (stack.Count <= 1)
        {
            Debug.LogWarning("[SceneBackStack] No previous scene to go back.");
            return;
        }

        // pop 当前
        stack.Pop();
        string target = stack.Peek(); // 上一个

        Debug.Log("[SceneBackStack] GoBack to: " + target);
        SceneManager.LoadScene(target, LoadSceneMode.Single);
    }
}