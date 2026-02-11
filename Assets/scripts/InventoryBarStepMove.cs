using UnityEngine;




public class InventoryBarStepMove : MonoBehaviour
{
    [Header("要移动的整条道具栏 Root")]
    [SerializeField] private Transform barRoot;

    [Header("每次点击移动的距离（世界坐标单位）")]
    [SerializeField] private float step = 1.5f;

    [Header("移动范围（世界坐标）")]
    [SerializeField] private float minX = -6f;
    [SerializeField] private float maxX = 0f;

    [Header("是否平滑移动")]
    [SerializeField] private bool smooth = true;

    [SerializeField] private float smoothSpeed = 12f;

    private float targetX;

    void Awake()
    {
        if (barRoot == null) barRoot = transform;
        targetX = barRoot.position.x;
    }

    void Update()
    {
        if (!smooth) return;

        Vector3 p = barRoot.position;
        p.x = Mathf.Lerp(p.x, targetX, Time.deltaTime * smoothSpeed);
        barRoot.position = p;
    }

    public void MoveLeft()
    {
        SetTargetX(targetX - step);
    }

    public void MoveRight()
    {
        SetTargetX(targetX + step);
    }

    private void SetTargetX(float x)
    {
        targetX = Mathf.Clamp(x, minX, maxX);

        if (!smooth)
        {
            Vector3 p = barRoot.position;
            p.x = targetX;
            barRoot.position = p;
        }
    }
}