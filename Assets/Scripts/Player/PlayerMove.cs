using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [Header("Objects")] 
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private Animator animator;
    [SerializeField] private Player player;
    [SerializeField] private GameObject trail;
    [SerializeField] private GameObject trailBlaze;

    [Header("Player Move")] 
    [SerializeField] private bool canMove = true;
    [SerializeField] private float playerSpeed;
    [SerializeField] private Vector2 playerMove, playerMoveGoal;
    [SerializeField] private float moveLerpSpeed, currentLerpSpeed;
    [SerializeField] private float currentSpeed, speedGoal, speedLerp, speedDodgeLerp;

    [Header("Player Velocity")] 
    [SerializeField] private Vector2 outsideForce;
    [SerializeField] private float outsideForceLerp;
    
    [Header("Player Dodge")] 
    [SerializeField] private float dodgeSpeed;
    [SerializeField] private float dodgeMoveLerp;
    [SerializeField] private float dodgeMaxTime, dodgeTimer;

    [Header("Audio")] 
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private AudioClip dodge;
    
    public float dodgeDelay { get; private set; }
    public float dodgeDelayTimer { get; private set; }

    public bool isDodge { get; private set; }
    public bool isMoving { get; private set; }
    
    private void Start()
    {
        currentSpeed = playerSpeed;
        speedGoal = playerSpeed;
        dodgeTimer = dodgeMaxTime;
        dodgeDelayTimer = 0.0f;

        dodgeDelay = 1f;
        
        audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
    }

    private void Update()
    {
        if (!canMove)
            return;
        
        if (dodgeTimer < dodgeMaxTime)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, speedGoal, speedDodgeLerp * Time.deltaTime);
            currentLerpSpeed = dodgeMoveLerp;
            dodgeTimer += Time.deltaTime;

            isDodge = true;
            
            trail.SetActive(true);
        }
        else
        {
            speedGoal = playerSpeed;
            currentSpeed = Mathf.Lerp(currentSpeed, speedGoal, speedLerp * Time.deltaTime);
            currentLerpSpeed = moveLerpSpeed;

            trail.SetActive(false);
            
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
    }

    private void FixedUpdate()
    {
        if (!canMove)
        {
            rb2d.velocity = Vector2.zero;
            return;
        }

            
        outsideForce = Vector2.Lerp(outsideForce, Vector2.zero, outsideForceLerp * Time.deltaTime);
        rb2d.velocity = ((playerMove * currentSpeed) + (outsideForce)) * 200.0f * Time.fixedDeltaTime;

        Vector2 vel = RoundAnimVelocity();
        
        animator.SetFloat("X", vel.x);
        animator.SetFloat("Y", vel.y);
        
        //Debug.Log(vel);
    }

    private Vector2 RoundAnimVelocity()
    {
        Vector2 temp = new Vector2(rb2d.velocity.x, rb2d.velocity.y);

        if (Mathf.Abs(temp.y) > Mathf.Abs(temp.x))
        {
            temp.x = 0.0f;
            temp.y = 1.0f * temp.y > 0.0f ? 1.0f : -1.0f;
        }
        else
        {
            temp.x = 1.0f * temp.x > 0.0f ? 1.0f : -1.0f;
            temp.y = 0.0f;
        }

        return temp;
    }

    public void AddForce(Vector2 force)
    {
        outsideForce = force;
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
        if (rb2d.velocity.magnitude < 1.0f)
            return;

        if (dodgeDelayTimer >= dodgeDelay)
        {
            speedGoal = dodgeSpeed;
            
            dodgeTimer = 0.0f;
            dodgeDelayTimer = 0.0f;
            
            Instantiate(trailBlaze, transform.position, Quaternion.identity);
        
            audioManager.AddSoundToQueue(dodge, false, 0.35f);

            player.SetInvincible(0.55f);
        }
    }

    public void SetPlayerCanMove(bool canMove)
    {
        this.canMove = canMove;
    }
}