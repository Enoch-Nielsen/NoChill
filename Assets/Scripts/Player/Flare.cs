using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flare : MonoBehaviour
{
    private void Start()
    {
        GameObject[] snowballs = GameObject.FindGameObjectsWithTag("Snowball");
        
        GameObject[] icicles = GameObject.FindGameObjectsWithTag("Icicle");

        if (snowballs == null || icicles == null)
            return;
        
        foreach (var s in snowballs)
            Destroy(s.gameObject);   
        
        foreach (var s in icicles)
            Destroy(s.gameObject);   
    }
}
