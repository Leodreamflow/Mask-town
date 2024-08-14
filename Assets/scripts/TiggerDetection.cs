using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetection : MonoBehaviour
{
    //Distance
    public float radius = 2.5f;
    public Transform[] players;

    private bool[] canInteract;


    //Np
    public float NPvalue;
    public NewsFeedback[] Feedbacks;


    //Comments
    public GameObject[] Comments;


    private void Start()
    {
        if (players != null)
        {
            canInteract = new bool[players.Length];
        }
        else
        {
            Debug.LogWarning("Players array is not assigned!");
        }
    }

    private void Update()
    {
        if (players != null && players.Length > 0)
        {
            for (int i = 0; i < players.Length; i++)
            {
                canInteract[i] = Vector2.Distance(transform.position, players[i].position) < radius;

                if (canInteract[i])
                {
                    Debug.Log($"Player {i} is within the radius.");
                    InteractEffect(i);
                    NPManager(i);
                    CommentsManager(i);
                }
            }
        }
        else
        {
            Debug.LogWarning("Players array is not assigned or empty!");
        }
    }

    private void InteractEffect(int playerIndex)
    {
        if (players != null && players.Length > 0)
        {
            MoveSetting moveSetting = players[playerIndex].GetComponent<MoveSetting>();
            if (moveSetting != null)
            {
                moveSetting.lightON();
            }
            else
            {
                Debug.LogWarning($"MoveSetting component not found on player[{playerIndex}]!");
            }

            MoveSetting2 moveSetting2 = players[playerIndex].GetComponent<MoveSetting2>();
            if (moveSetting2 != null)
            {
                moveSetting2.lightON();
            }
            else
            {
                Debug.LogWarning($"MoveSetting2 component not found on player[{playerIndex}]!");
            }
        }
        else
        {
            Debug.LogWarning("Players array is not assigned or empty!");
        }
    }

    private void NPManager(int maskIndex)
    {
        NPvalue += Feedbacks[maskIndex].NPMask;

    }

    private void CommentsManager(int commentIndex)
    {
        if (Comments[commentIndex] == null) return;

        Comments[commentIndex].SetActive(true);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
