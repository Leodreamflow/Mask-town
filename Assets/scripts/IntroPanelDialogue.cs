using System.Collections;
using UnityEngine;
using TMPro;

public class IntroPanelDialogue : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private RectTransform panel;   // 要滑动的面板
    [SerializeField] private TMP_Text textLabel;    // 面板里的文字
    [SerializeField] private GameObject clickBlocker; // 可选：覆盖全屏的透明Button/Panel，用来接收点击

    [Header("Slide Settings")]
    [SerializeField] private Vector2 hiddenPos = new Vector2(0, 800);   // 面板隐藏位置（屏幕上方）
    [SerializeField] private Vector2 shownPos = new Vector2(0, 0);     // 面板显示位置
    [SerializeField] private float slideDuration = 0.35f;

    [Header("Dialogue")]
    [TextArea(2, 6)]
    [SerializeField] private string[] lines;

    private int index = 0;
    private bool isAnimating = false;
    private bool isActive = false;

    private void Awake()
    {
        if (panel == null) panel = GetComponent<RectTransform>();
    }

    private void Start()
    {
        // 开局：先放到隐藏位置，再滑下来
        panel.anchoredPosition = hiddenPos;
        if (textLabel != null) textLabel.text = "";
        if (clickBlocker != null) clickBlocker.SetActive(true);

        StartCoroutine(ShowSequence());
    }

    // 把这个函数绑到 Button 的 OnClick()（比如全屏透明按钮）
    public void OnClickNext()
    {
        if (!isActive || isAnimating) return;

        // 还有文字：显示下一句
        if (lines != null && index < lines.Length)
        {
            textLabel.text = lines[index];
            index++;

            // 已经显示完最后一句：再点一次就收回（你也可以改成自动收回）
            if (index >= lines.Length)
            {
                // 提示用户“再点一次关闭”，可选
                // textLabel.text += "\n\n(Click to close)";
            }

            return;
        }

        // 文字放完后，再点：收回
        StartCoroutine(HideSequence());
    }

    private IEnumerator ShowSequence()
    {
        isAnimating = true;
        yield return Slide(panel, hiddenPos, shownPos, slideDuration);
        isAnimating = false;

        isActive = true;

        // 自动显示第一句（如果你想“点击才出第一句”，删掉下面两行）
        index = 0;
        if (lines != null && lines.Length > 0)
        {
            textLabel.text = lines[index];
            index++;
        }
    }

    private IEnumerator HideSequence()
    {
        isActive = false;
        isAnimating = true;

        yield return Slide(panel, shownPos, hiddenPos, slideDuration);

        isAnimating = false;
        if (clickBlocker != null) clickBlocker.SetActive(false);
        // 如果你想隐藏整个面板对象：
        // gameObject.SetActive(false);
    }

    private IEnumerator Slide(RectTransform rt, Vector2 from, Vector2 to, float duration)
    {
        float t = 0f;
        rt.anchoredPosition = from;

        while (t < duration)
        {
            t += Time.deltaTime;
            float k = duration <= 0f ? 1f : Mathf.Clamp01(t / duration);
            // 更顺滑的缓动（可选）
            k = k * k * (3f - 2f * k); // SmoothStep
            rt.anchoredPosition = Vector2.LerpUnclamped(from, to, k);
            yield return null;
        }

        rt.anchoredPosition = to;
    }
}