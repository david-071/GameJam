using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public float leverID;
    bool isPulled = false;


    void Update()
    {
        if (!isPulled)
        {
            if (GameManager.manager.pulledLever == leverID)
            {
                Debug.Log("Lever: " + leverID + " pulled");
                transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y);
                GameManager.manager.selectedLever = 0f;
                isPulled = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isPulled)
        {
            if (other.CompareTag("Player"))
            {
                GameManager.manager.selectedLever = leverID;
                Debug.Log("Lever selected: " + leverID);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!isPulled)
        {
            if (other.CompareTag("Player"))
            {
                GameManager.manager.selectedLever = 0f;

            }
        }
    }
}
