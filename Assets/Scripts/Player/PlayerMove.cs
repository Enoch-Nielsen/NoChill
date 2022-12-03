using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{

    [Header("Objects")] 
    [SerializeField] private Rigidbody2D rb2d;
    
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

    public bool isDodge { get; private set; }
    public bool isMoving { get; private set; }


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

            isDodge = true;
        }
        else
        {
            speedGoal = playerSpeed;
            currentSpeed = Mathf.Lerp(currentSpeed, speedGoal, speedLerp * Time.deltaTime);
            currentLerpSpeed = moveLerpSpeed;
            
            isDodge = false;
            isMoving = playerMove.magnitude > 0.15f;
            
            if (dodgeDelayTimer < dodgeDelay)
            {
                dodgeDelayTimer += Time.deltaTime;
            }
        }
        
        playerMove = Vector2.Lerp(playerMove, playerMoveGoal, Time.deltaTime * currentLerpSpeed);

        if (Mathf.Approximately(playerMove.x, 0.0f))
            playerMove.x = 0;

        if (Mathf.Approximately(playerMove.y, 0.0f))
        {
            playerMove.y = 0;
        }
        

        //rb2d.velocity = playerMove * currentSpeed * Time.deltaTime;
        //transform.Translate(playerMove * currentSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        rb2d.velocity = playerMove * currentSpeed * 200.0f * Time.fixedDeltaTime;
    }

    private void OnMove(InputValue value)
    {
        Vector2 temp = value.Get<Vector2>();

        if (Mathf.Abs(temp.x) > 0.15f && Mathf.Abs(temp.y) > 0.15f)
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