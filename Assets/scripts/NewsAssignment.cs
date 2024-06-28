using UnityEngine;
using TMPro;

public class NewsAssignment : MonoBehaviour
{
    public NewsSpecial newsart;

    public GameObject Textinfro;

    private void OnMouseEnter()
    {
        //show text
        Textinfro.SetActive(true);
        Textinfro.GetComponent<TextMeshPro>().text = newsart.info;
    }

    private void OnMouseExit()
    {
        //Exit text
        Textinfro.SetActive(false);
        Textinfro.GetComponent<TextMeshPro>().text = newsart.info;
    }
}
