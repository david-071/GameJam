using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.TimeZoneInfo;

public class Death : MonoBehaviour
{
    public GameObject player;
    public Transform playerTransform;
    public GameObject playerAlt;
    public float respawnDelay = 1f;
    private float respawnDelayMax;
    private bool hasDied = false;
    private float transitionTime;

    private void Start()
    {
        respawnDelay = 1f;
        respawnDelayMax = respawnDelay;
    }

    private void Update()
    {
        playerTransform = player.transform;
        if (hasDied)
        {
            respawnDelay += Time.deltaTime;
            if (respawnDelay >= respawnDelayMax)
            {
                Debug.Log(respawnDelay + "/" + respawnDelayMax);
                GameManager.manager.respawnNow = true;


                //tp reflejo
                GameManager.manager.reflectionPlaying = false;
                GameManager.manager.reflectionChangeDelay = 0f;
                Debug.Log("Reflection playing: " + GameManager.manager.reflectionPlaying);
                GameManager.manager.isChangingGlobal = false;
                GameManager.manager.isEndingChangeGlobal = true;
                hasDied = false;
                respawnDelay = 0f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.manager.isGameOver = true;
            respawnDelay = 0f;
            hasDied = true;
        }
    }
}
