using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    [Header("Player Move")]
    [SerializeField] private float playerSpeed;
    [SerializeField] private Vector2 playerMove, playerMoveGoal;
    [SerializeField] private float moveLerpSpeed, currentLerpSpeed;
    [SerializeField] private float currentSpeed, speedGoal, speedLerp, speedDodgeLerp;

    [Header("Player Dodge")] 
    [SerializeField] private float dodgeSpeed;
    [SerializeField] private float dodgeMoveLerp;
    [SerializeField] private float dodgeMaxTime, dodgeTimer;
    [SerializeField] private float dodgeDelay, dodgeDelayTimer;

    private void Start()
    {
        currentSpeed = playerSpeed;
        speedGoal = playerSpeed;
        dodgeTimer = dodgeMaxTime;
        dodgeDelayTimer = 0.0f;
    }

    private void Update()
    {
        if (dodgeTimer < dodgeMaxTime)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, speedGoal, speedDodgeLerp * Time.deltaTime);
            currentLerpSpeed = dodgeMoveLerp;
            dodgeTimer += Time.deltaTime;
        }
        else
        {
            speedGoal = playerSpeed;
            currentSpeed = Mathf.Lerp(currentSpeed, speedGoal, speedLerp * Time.deltaTime);
            currentLerpSpeed = moveLerpSpeed;

            if (dodgeDelayTimer < dodgeDelay)
            {
                dodgeDelayTimer += Time.deltaTime;
            }
        }
        
        playerMove = Vector2.Lerp(playerMove, playerMoveGoal, Time.deltaTime * currentLerpSpeed);
        
        transform.Translate(playerMove * currentSpeed * Time.deltaTime);
    }

    private void OnMove(InputValue value)
    {
        Vector2 temp = value.Get<Vector2>();

        if (temp.x != 0 && temp.y != 0)
        {
            temp *= 0.75f;
        }
        
        playerMoveGoal = temp;
    }

    private void OnDodge()
    {
        if (dodgeDelayTimer >= dodgeDelay)
        {
            speedGoal = dodgeSpeed;
            
            dodgeTimer = 0.0f;
            dodgeDelayTimer = 0.0f;
        }
    }
}