using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Removement : MonoBehaviour
{
    public float movespeed;
    public float waitsecond;

    public Transform startposition;
    public Transform endposition;
    public Transform mittelposition;

    private float count = 0;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = startposition.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.y <= endposition.position.y)
        {
            transform.position = startposition.position;
            count = 0;
        }
        else if (transform.position.y <= mittelposition.position.y)
        {
            if (count < waitsecond)
            {
                count += Time.deltaTime;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, endposition.position, movespeed * Time.deltaTime);
            }
        }

        else
        {
            // 否则，继续向终点移动
            transform.position = Vector3.MoveTowards(transform.position, endposition.position, movespeed * Time.deltaTime);
        }
    }
}
