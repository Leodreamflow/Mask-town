using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public float amplitude = 0.5f; // 浮动的高度
    public float frequency = 1f; // 浮动的频率

    // 初始位置
    private Vector3 startPos;

    void Start()
    {
        // 保存初始位置
        startPos = transform.position;
    }

    void Update()
    {
        // 计算新的 Y 位置
        float newY = startPos.y + Mathf.Sin(Time.time * frequency) * amplitude;

        // 设置新的位置
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}