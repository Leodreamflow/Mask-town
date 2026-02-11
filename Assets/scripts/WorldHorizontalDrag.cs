using UnityEngine;

public class WorldHorizontalDrag : MonoBehaviour
{
    [Header("要移动的目标（通常是InventoryBarRoot）")]
    [SerializeField] private Transform target;

    [Header("X轴移动范围（世界坐标）")]
    [SerializeField] private float minX = -5f;
    [SerializeField] private float maxX = 0f;

    [Header("拖动灵敏度（越大拖得越快）")]
    [SerializeField] private float dragSpeed = 1f;

    private Camera cam;
    private bool dragging = false;

    private Vector3 lastWorld;

    void Awake()
    {
        cam = Camera.main;
        if (target == null) target = transform;
    }

    void Update()
    {
        // 鼠标 或 触摸：统一成一个“是否按下/移动/抬起”
        if (PointerDown())
        {
            if (HitThisCollider())
            {
                dragging = true;
                lastWorld = PointerWorldPos();
            }
        }

        if (dragging && PointerHeld())
        {
            Vector3 nowWorld = PointerWorldPos();
            float dx = (nowWorld.x - lastWorld.x) * dragSpeed;

            Vector3 p = target.position;
            p.x = Mathf.Clamp(p.x + dx, minX, maxX);
            target.position = p;

            lastWorld = nowWorld;
        }

        if (PointerUp())
        {
            dragging = false;
        }
    }

    // --------- Pointer helpers ---------

    bool PointerDown()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        return Input.GetMouseButtonDown(0);
#else
        return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
#endif
    }

    bool PointerHeld()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        return Input.GetMouseButton(0);
#else
        if (Input.touchCount == 0) return false;
        var ph = Input.GetTouch(0).phase;
        return ph == TouchPhase.Moved || ph == TouchPhase.Stationary;
#endif
    }

    bool PointerUp()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        return Input.GetMouseButtonUp(0);
#else
        return Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled);
#endif
    }

    Vector3 PointerWorldPos()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        Vector3 s = Input.mousePosition;
#else
        Vector3 s = Input.GetTouch(0).position;
#endif
        // 2D：给一个到相机的距离，把屏幕点转世界点
        s.z = Mathf.Abs(cam.transform.position.z);
        return cam.ScreenToWorldPoint(s);
    }

    bool HitThisCollider()
    {
        Vector3 world = PointerWorldPos();
        Vector2 p2 = new Vector2(world.x, world.y);

        // 只检测当前物体的Collider2D
        var col = GetComponent<Collider2D>();
        return col != null && col.OverlapPoint(p2);
    }
}