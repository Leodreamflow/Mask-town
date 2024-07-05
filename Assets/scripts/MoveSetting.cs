using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSetting : MonoBehaviour
{
    public float movespeed;
    public float waitsecond;

    public Transform startposition;
    public Transform endposition;
    public Transform mittelposition;

    private float count = 0;
    private Animator npcanimator;

    private bool ischoosed = false;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = startposition.position;
        npcanimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!ischoosed)
        {
            if (transform.position.x >= endposition.position.x)
            {
                transform.position = startposition.position;
                count = 0;
            }
            else if (transform.position.x >= mittelposition.position.x)
            {
                if (count < waitsecond)
                {
                    count += Time.deltaTime;
                    npcanimator.SetTrigger("isStanding");
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, endposition.position, movespeed * Time.deltaTime);
                    npcanimator.SetTrigger("isWalking");
                }
            }
            else
            {
                // 否则，继续向终点移动
                transform.position = Vector3.MoveTowards(transform.position, endposition.position, movespeed * Time.deltaTime);
                npcanimator.SetTrigger("isWalking");
            }
        }
        
    }

    public void lightON()
    {
        ischoosed = true;
        npcanimator.SetTrigger("isLighting");
    }

    public void ContinueAnimation()
    {
        ischoosed = false;
    }
}