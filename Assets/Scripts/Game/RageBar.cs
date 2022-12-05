using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RageBar : MonoBehaviour
{
    [SerializeField] private Image rBar;
    [SerializeField] private Player player;

    private void Update()
    {
        rBar.fillAmount = player.GetRageFill();
    }
}
