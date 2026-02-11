using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AutoSwitchSpriteBackground : MonoBehaviour
{
    [Header("Backgrounds")]
    [SerializeField] private Sprite[] backgrounds;

    [Header("Timing")]
    [SerializeField] private float intervalSeconds = 3f;
    [SerializeField] private bool randomOrder = false;

    private SpriteRenderer sr;
    private int index = 0;
    private float timer = 0f;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        if (backgrounds == null || backgrounds.Length == 0)
        {
            Debug.LogWarning($"{name}: No backgrounds assigned.");
            enabled = false;
            return;
        }

        index = 0;
        sr.sprite = backgrounds[index];
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer < intervalSeconds) return;

        timer = 0f;
        SwitchOnce();
    }

    private void SwitchOnce()
    {
        if (backgrounds.Length == 1) return;

        if (randomOrder)
        {
            int next = index;
            // 避免连续重复
            while (next == index)
                next = Random.Range(0, backgrounds.Length);
            index = next;
        }
        else
        {
            index = (index + 1) % backgrounds.Length;
        }

        sr.sprite = backgrounds[index];
    }

    // 可选：给按钮/事件调用，手动切一次
    public void NextNow()
    {
        timer = 0f;
        SwitchOnce();
    }
}