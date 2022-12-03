using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private PlayerMove playerMove;
    
    [SerializeField] private float rage, maxRage;
    [SerializeField] private float rageSpeed, slowedRageSpeed;
    
    [SerializeField] private bool isSlowed;
    [SerializeField] private float slowTimer, slowTimerMax;

    public bool isInvincible { get; private set;}
    [SerializeField] private float invincibilityTimer, invincibilityMax;

    public bool punchReady { get; private set; }

    private void Update()
    {
        if (rage < maxRage)
            rage += (isSlowed ? rageSpeed : slowedRageSpeed) * Time.deltaTime;
        else
            punchReady = true;

        if (slowTimer < slowTimerMax)
            slowTimer += Time.deltaTime;
        else
            isSlowed = false;
        
        if (invincibilityTimer < invincibilityMax)
            invincibilityTimer += Time.deltaTime;
        else
            isInvincible = false;

        if (punchReady)
            SetInvincible(100.0f);
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
    }

    /**
     * will damage the player by the damageValue, slow will decrease rage increments temporarily, does nothing if the player
     * is invincible.
     */
    public void Damage(float damageValue, bool doesSlow)
    {
        if (isInvincible)
            return;
        
        rage -= damageValue;

        if (rage < 0)
        {
            rage = 0;
        }

        isSlowed = doesSlow;

        slowTimer = 0.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Snowball"))
        {
            Damage(7, false);
        }

        if (other.CompareTag("Icicle"))
        {
            Damage(10, true);
        }
    }
}
