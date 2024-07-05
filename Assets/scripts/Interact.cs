using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;

    private Vector3 orginalPos;

    private void Start()
    {
        orginalPos = transform.position;
    }

    void OnMouseDown()
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;
    }

    void OnMouseUp()
    {
        isDragging = false;
        transform.position = orginalPos;
    }

    void Update()
    {
        if (isDragging)
        {

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            transform.position = mousePosition + offset;
        }
    }
}