using UnityEngine;

public class FlyRandomMove : MonoBehaviour
{
    [Header("References")]
    public RectTransform fly;

    [Header("Movement Area (anchored)")]
    public Vector2 areaSize = new Vector2(300f, 200f); // 活动范围（以初始位置为中心）

    [Header("Movement")]
    public float moveSpeed = 80f;            // 移动速度（px/s）
    public float jitterStrength = 12f;       // 抖动强度
    public float jitterSpeed = 6f;           // 抖动频率
    public float changeTargetInterval = 1.2f; // 换目标点间隔（秒）

    [Header("Rotation")]
    public float rotationSpeed = 180f;        // 转向速度（度/秒）
    public float rotationJitter = 25f;        // 每次换向随机角度范围
    public float changeRotationInterval = 0.8f; // 换旋转目标间隔（秒）

    private Vector2 originPos;
    private Vector2 targetPos;
    private float targetTimer;

    private float targetRotation;
    private float rotationTimer;

    void Awake()
    {
        if (fly == null)
            fly = GetComponent<RectTransform>();

        originPos = fly.anchoredPosition;
        PickNewTarget();

        targetRotation = Random.Range(0f, 360f);
    }

    void Update()
    {
        // ---- target position ----
        targetTimer += Time.deltaTime;
        if (targetTimer >= changeTargetInterval)
        {
            PickNewTarget();
            targetTimer = 0f;
        }

        // 主移动：向目标点靠近
        fly.anchoredPosition = Vector2.MoveTowards(
            fly.anchoredPosition,
            targetPos,
            moveSpeed * Time.deltaTime
        );

        // 电子/生物抖动：用 Perlin 噪声（更自然）
        float nx = Mathf.PerlinNoise(Time.time * jitterSpeed, 0.123f) - 0.5f;
        float ny = Mathf.PerlinNoise(0.456f, Time.time * jitterSpeed) - 0.5f;
        Vector2 jitter = new Vector2(nx, ny) * jitterStrength;

        fly.anchoredPosition += jitter * Time.deltaTime;

        // ---- rotation ----
        rotationTimer += Time.deltaTime;
        if (rotationTimer >= changeRotationInterval)
        {
            targetRotation += Random.Range(-rotationJitter, rotationJitter);
            rotationTimer = 0f;
        }

        float currentZ = fly.localEulerAngles.z;
        float newZ = Mathf.MoveTowardsAngle(currentZ, targetRotation, rotationSpeed * Time.deltaTime);
        fly.localEulerAngles = new Vector3(0f, 0f, newZ);
    }

    void PickNewTarget()
    {
        targetPos = originPos + new Vector2(
            Random.Range(-areaSize.x, areaSize.x),
            Random.Range(-areaSize.y, areaSize.y)
        );
    }
}