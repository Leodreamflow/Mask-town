using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenubackgroundMove : MonoBehaviour
{
    public float amplitude = 0.5f;
    public float frequency = 1f;


    private Vector3 startPos;

    void Start()
    {

        startPos = transform.position;
    }

    void Update()
    {

        float newX = startPos.x + Mathf.Sin(Time.time * frequency) * amplitude;


        transform.position = new Vector3(newX, startPos.y, startPos.z);
    }
}
