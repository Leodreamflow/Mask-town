using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;

    void OnMouseDown()
    {
        // ��������������������ĵ�ƫ������ת������������ϵ
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
            // ��ȡ�������������ϵ�е�λ��
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Ӧ�ø��������任���Ա㿼�Ǹ������λ�ú���ת
            transform.position = mousePosition + offset;
        }
    }
}