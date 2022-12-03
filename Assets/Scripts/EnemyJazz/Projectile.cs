using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    private GameObject player = null;
    [SerializeField] private bool isIcicle = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        transform.LookAt(player.transform);
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate((Vector3.forward).normalized * Time.deltaTime * speed);
        
    }
}
