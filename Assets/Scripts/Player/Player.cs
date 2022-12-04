using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private PlayerMove playerMove;
    [SerializeField] private Animator animator;
    [SerializeField] private StageManager stageManager;
    [SerializeField] private GameObject flarePower, rageFx;
    [SerializeField] private ProjectileSpawner spawner;

    [SerializeField] private int currentStage = 1;
        
    [SerializeField] private float impactForce;
    
    [SerializeField] private float rage, maxRage, startRage;
    [SerializeField] private float rageSpeed, slowedRageSpeed;
    
    [SerializeField] private bool isSlowed;
    [SerializeField] private float slowTimer, slowTimerMax;

    [SerializeField] private bool isInvincible;
    [SerializeField] private float invincibilityTimer, invincibilityMax;

    [SerializeField] private bool punchReady;

    [SerializeField] private GameObject punchTrail, punchFX;

    [SerializeField] private bool powerUpReady;

    [SerializeField] private bool audioTriggered;

    [SerializeField] private bool isDead;
    
    [Header("Audio")] 
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private AudioClip hurt, bossPunch, rageHitMax, raging, flare, powerUp;

    private void Start()
    {
        isInvincible = false;

        audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();

        cameraShake = GameObject.FindWithTag("CameraShake").GetComponent<CameraShake>();

        rage = startRage;
    }

    private void Update()
    {
        if (isDead)
            return;
        
        if (rage <= 0)
            Die();
        
        if (rage < maxRage)
            rage += (isSlowed ? slowedRageSpeed : rageSpeed) * Time.deltaTime;
        else
        {
            punchReady = true;

            if (!audioTriggered)
            {
                audioManager.StopAllSounds();
                audioManager.AddSoundToQueue(rageHitMax, false, 0.55f);
                Invoke(nameof(StartRageSound), 0.75f);

                audioTriggered = true;
            }
        }
        
        if (slowTimer < slowTimerMax)
            slowTimer += Time.deltaTime;
        else
            isSlowed = false;
        
        if (invincibilityTimer < invincibilityMax)
            invincibilityTimer += Time.deltaTime;
        else
            isInvincible = false;

        if (punchReady)
            SetInvincible(2.0f);
    }

    // for rage text value.
    public int GetRageAsInt()
    {
        return Mathf.RoundToInt(rage);
    }

    // for rage bar.
    public float GetRageFill()
    {
        return rage / maxRage;
    }

    // for visual timer as text.
    public float GetDodgeDelayAsSeconds()
    {
        return playerMove.dodgeDelay - playerMove.dodgeDelayTimer;
    }

    // for visual timer as a bar or circle.
    public float GetDodgeDelayAsFill()
    {
        return playerMove.dodgeDelayTimer / playerMove.dodgeDelay;
    }

    // Makes the player invincible for the given time.
    public void SetInvincible(float time)
    {
        invincibilityMax = time;
        invincibilityTimer = 0.0f;

        isInvincible = true;
        
        //Debug.Log("Invincible for: " + time);
    }

    /**
     * will damage the player by the damageValue, slow will decrease rage increments temporarily, does nothing if the player
     * is invincible.
     */
    public void Damage(float damageValue, bool doesSlow, Vector2 hitPosition)
    {
        if (isInvincible)
        {
            return;
        }
        
        rage -= damageValue;

        if (rage < 0)
        {
            rage = 0;
        }

        isSlowed = doesSlow;

        slowTimer = 0.0f;
        
        SetInvincible(0.65f);

        audioManager.AddSoundToQueue(hurt, false, 0.35f);

        Vector2 impact = new Vector2(transform.position.x, transform.position.y) - hitPosition * -1.0f;
        
        playerMove.AddForce(impact * impactForce);

        cameraShake.Shake(0.35f);
    }

    public bool IsInvincible()
    {
        return isInvincible;
    }

    public bool PunchIsReady()
    {
        return punchReady;
    }

    public void Punch()
    {
        SetInvincible(5.0f);
        
        animator.SetBool("Punch", true);
        
        playerMove.SetPlayerCanMove(false);

        punchReady = false;        
        rage = startRage;

        punchTrail.SetActive(true);

        currentStage++;
        
        Invoke(nameof(SpawnPunchFX), 0.35f);
        Invoke(nameof(EndPunch), 1.35f);
        Invoke(nameof(ShakeDelay), 0.05f);
        Invoke(nameof(NextPhaseTrigger), 0.1f);

        audioTriggered = false;
    }

    private void NextPhaseTrigger()
    {
        stageManager.SelectStage(currentStage);
    }

    private void ShakeDelay()
    {
        cameraShake.Shake(1.0f);
    }

    private void SpawnPunchFX()
    {
        Instantiate(punchFX, transform.position + new Vector3(-0.3f, 0.0f, 0.0f), Quaternion.identity);
        audioManager.AddSoundToQueue(bossPunch, false, 0.65f);
    }

    private void EndPunch()
    {
        punchTrail.SetActive(false);
        animator.SetBool("Punch", false);
        
        playerMove.SetPlayerCanMove(true);

        rage = startRage;

        rageFx.SetActive(false);
    }

    private void StartRageSound()
    {
        audioManager.AddSoundToQueue(raging, true, 0.35f);
        rageFx.SetActive(true);
    }

    public void SetPowerUpReady()
    {
        powerUpReady = true;
        audioManager.AddSoundToQueue(powerUp, false, 0.45f);
    }

    private void OnPowerUp()
    {
        if (!powerUpReady)
            return;
        
        Instantiate(flarePower, transform.position, Quaternion.identity);
        powerUpReady = false;
        
        audioManager.AddSoundToQueue(flare, false, 0.35f);
    }

    private void Die()
    {
        if (isDead)
            return;

        animator.SetBool("Dead", true);
        
        playerMove.SetPlayerCanMove(false);

        isDead = true;

        spawner.gameObject.SetActive(false);
        
        Invoke(nameof(MoveToChill), 5.0f);
    }

    private void MoveToChill()
    {
        SceneManager.LoadScene("ChillScene");
    }
}
