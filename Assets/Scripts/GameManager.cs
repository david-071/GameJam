using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{

    public static GameManager manager;

    //Variables publicas
    [Header("VariablesPublicas")]
    public bool reflectionPlaying = false;
    public float reflectionChangeDelay = 2f;
    float reflectionMaxDelay; 

    private void Awake()
    {
        if (manager == null)
        {
            manager = this;
            DontDestroyOnLoad(this);
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
    }

}
