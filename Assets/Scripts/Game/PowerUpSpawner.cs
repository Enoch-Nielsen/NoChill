using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject powerUp;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnPowerUp), 10f, 30f);
    }

    private void SpawnPowerUp()
    {
        float x = Random.Range(-6, 6);
        float y = Random.Range(0, -3);

        Instantiate(powerUp, new Vector3(x, y, 0.0f), Quaternion.identity);
    }
}
