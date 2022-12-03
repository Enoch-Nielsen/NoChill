using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    private GameObject player = null;
    [SerializeField] float rotationModifyer = 60;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //Vector3 vectorToPlayer = player.transform.position - transform.position;
        //float angle = Mathf.Atan2(vectorToPlayer.y, vectorToPlayer.x) * Mathf.Rad2Deg - rotationModifyer;
        //Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, q, Time.deltaTime * 1);
        //Quaternion.LookRotation(Vector3.forward, vectorToPlayer);

        transform.LookAt(player.transform);
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate((Vector3.forward).normalized * Time.deltaTime * speed);
        
    }
}
