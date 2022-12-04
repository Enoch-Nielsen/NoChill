using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float shakeTimer, shakeMaxTimer;

    private void Update()
    {
        if (shakeTimer < shakeMaxTimer)
            shakeTimer += Time.deltaTime;
        else
            animator.SetBool("Shake", false);
    }

    public void Shake(float duration)
    {
        shakeTimer = 0.0f;

        shakeMaxTimer = duration;

        animator.SetBool("Shake", true);
    }
}
