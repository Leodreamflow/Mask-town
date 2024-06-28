using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Newsinfro : MonoBehaviour
{

    [SerializeField] Camera mainCamera;
    [SerializeField] float zPos;

    private void Start()
    {
        
    }

    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = zPos;

        Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);
        transform.position = worldPos;
    }



    //public void InfoDisplay(string _note)
    //{
    //    GetComponentInChildren<TextMeshPro>().text = _note;
    //}
}

