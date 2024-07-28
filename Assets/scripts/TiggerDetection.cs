using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetection : MonoBehaviour
{
    public float radius = 2.5f; 
    public Transform[] player; 

    private void Update()
    {
       
        if (player != null && player.Length > 0)
        {
   
            bool canInteract = Vector2.Distance(transform.position, player[0].position) < radius;

            if (canInteract)
            {
                Debug.Log("Touched a collider: a");
                InteractEffect();
            }
        }
        else
        {

            Debug.LogWarning("Player variable in TriggerDetection is not assigned or empty!");
        }
    }

    private void InteractEffect()
    {
   
        if (player != null && player.Length > 0)
        {

            MoveSetting moveSetting = player[0].GetComponent<MoveSetting>();
            if (moveSetting != null)
            {
                moveSetting.lightON();
            }
            else
            {
                Debug.LogWarning("MoveSetting component not found on player[0]!");
            }

            MoveSetting2 moveSetting2 = player[0].GetComponent<MoveSetting2>();
            if (moveSetting2 != null)
            {
                moveSetting2.lightON();
            }
            else
            {
                Debug.LogWarning("MoveSetting2 component not found on player[0]!");
            }
        }
        else
        {
            Debug.LogWarning("Player variable in TriggerDetection is not assigned or empty!");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}