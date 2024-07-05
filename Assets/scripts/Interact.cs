using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;

    void OnMouseDown()
    {
        // 计算鼠标点击点与物体中心的偏移量，转换到世界坐标系
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    void Update()
    {
        if (isDragging)
        {
            // 获取鼠标在世界坐标系中的位置
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // 应用父对象的逆变换，以便考虑父对象的位置和旋转
            transform.position = mousePosition + offset;
        }
    }
}