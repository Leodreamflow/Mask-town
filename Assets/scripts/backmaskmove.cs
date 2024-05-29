using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public float amplitude = 0.5f; // �����ĸ߶�
    public float frequency = 1f; // ������Ƶ��

    // ��ʼλ��
    private Vector3 startPos;

    void Start()
    {
        // �����ʼλ��
        startPos = transform.position;
    }

    void Update()
    {
        // �����µ� Y λ��
        float newY = startPos.y + Mathf.Sin(Time.time * frequency) * amplitude;

        // �����µ�λ��
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}