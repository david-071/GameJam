using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{

    public static GameManager manager;

    //Variables publicas
    [Header("VariablesPublicas")]
    public bool isGamePlaying;
    public bool isGameOver;
    public bool isFacingRight;
    public bool reflectionPlaying = false;
    public float reflectionChangeDelay = 1f;
    public bool respawnNow = false;
    public float selectedLever;
    public float pulledLever;
    public bool isRunningGlobal = false;
    public bool isJumpingGlobal = false;
    public bool isChangingGlobal = false;
    public bool isEndingChangeGlobal = false;
    float reflectionMaxDelay;
    float fadeTimer = 0f;

    private void Awake()
    {
        if (manager == null)
        {
            manager = this;
            DontDestroyOnLoad(this);
            isGamePlaying = true;
            isGameOver = false;
        }
        else if (manager != this)
        {
            Destroy(gameObject);
        }
        reflectionMaxDelay = reflectionChangeDelay;
    }

    void Update()
    {
        reflectionChangeDelay += Time.deltaTime;
        if (reflectionChangeDelay > reflectionMaxDelay)
        {
            reflectionChangeDelay = reflectionMaxDelay; 
        }

        if (isGameOver)
        {
            fadeTimer += Time.deltaTime;
            if (fadeTimer >= 0.1f)
            {
                isGameOver = false;
                fadeTimer = 0f;
            }
        }
    }

}
