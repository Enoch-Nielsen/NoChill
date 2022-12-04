using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    [SerializeField] private AudioClip stage1, stage2, stage3;

    [SerializeField] private AudioManager audioManager;

    [SerializeField] private Animator snowmanAnimator;
    [SerializeField] private CameraShake cameraShake;

    [SerializeField] private int currentStage;

    [SerializeField] private ProjectileSpawner pSpawner;

    private void Start()
    {
        audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        SelectStage(1);
    }

    public void SelectStage(int stage)
    {
        if (stage > 4)
            return;

        currentStage = stage;
        
        if (stage == 1)
        {
            Invoke(nameof(SwitchTheme), 0.25f);
            snowmanAnimator.SetInteger("Stage", 1);
        }
        
        if (stage == 2)
        {
            Invoke(nameof(SwitchTheme), 3f);

            snowmanAnimator.SetInteger("Stage", 2);
        }
        
        if (stage == 3)
        {
            Invoke(nameof(SwitchTheme), 3f);

            snowmanAnimator.SetInteger("Stage", 3);
        }

        if (stage == 4)
        {
            SceneManager.LoadScene("ChillScene");
        }
        
        pSpawner.IncreaseMoves();
        
        Invoke(nameof(StartShake), 2.75f);
    }

    private void StartShake()
    {
        cameraShake.Shake(1.0f);
    }

    private void SwitchTheme()
    {
        audioManager.StopAllSounds();
        
        if (currentStage == 1)
            audioManager.AddSoundToQueue(stage1, true, 0.1f);
        
        if (currentStage == 2)
            audioManager.AddSoundToQueue(stage2, true, 0.1f);
        
        if (currentStage == 3)
            audioManager.AddSoundToQueue(stage3, true, 0.1f);
    }
}
