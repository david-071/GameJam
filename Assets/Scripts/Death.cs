using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Death : MonoBehaviour
{
    public GameObject player;
    public GameObject playerAlt;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadSceneAsync(1);
            Destroy(player);
            Destroy(playerAlt);
        }
    }
}
