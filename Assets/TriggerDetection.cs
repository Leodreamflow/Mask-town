using UnityEngine;

public class TriggerDetection : MonoBehaviour
{

    public float radius = 2.5f;
    public Transform[] player;


    bool canInteract = false;

    private void Start()
    {

    }


    private void Update()
    {
        // to fix room trasition bug

        canInteract = Vector2.Distance(transform.position, player[0].position) < radius;

        if (canInteract)
        {
            Debug.Log("Touched a collider: a");
            InteractEffect();
        }
    }

    private void InteractEffect()
    {
        //animation
        player[0].GetComponent<MoveSetting>().lightON();
        //comment

    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}