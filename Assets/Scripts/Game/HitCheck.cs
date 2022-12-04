using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCheck : MonoBehaviour
{
    [SerializeField] private Player player;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            if (player.PunchIsReady())
                player.Punch();
    }
}
