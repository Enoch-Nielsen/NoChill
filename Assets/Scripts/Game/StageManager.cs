using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private AudioClip stage1, stage2, stage3, chill;

    [SerializeField] private AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        SelectStage(1);
    }

    public void SelectStage(int stage)
    {
        if (stage > 4)
            return;
        
        audioManager.StopAllSounds();
        
        if (stage == 1)
            audioManager.AddSoundToQueue(stage1, true, 0.2f);
        
        if (stage == 2)
            audioManager.AddSoundToQueue(stage2, true, 0.2f);
        
        if (stage == 3)
            audioManager.AddSoundToQueue(stage3, true, 0.2f);
        
        if (stage == 4)
            audioManager.AddSoundToQueue(chill, true, 0.3f);
    }
}
