using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    private Animator mAnimator;

    private void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        mAnimator.SetBool("isGamePlaying", GameManager.manager.isGamePlaying);
        mAnimator.SetBool("isGameOver", GameManager.manager.isGameOver);
    }
}
