using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    private GameObject player = null;
    [SerializeField] private bool isIcicle = false;
    public int attackNumber = 1;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if(attackNumber == 1)
        {
            transform.LookAt(player.transform);
        }

        StartCoroutine(SelfDestruct());
    }

    // Update is called once per frame
    void Update()
    {
        if(attackNumber == 1)
        {
            transform.Translate((Vector3.forward).normalized * Time.deltaTime * speed);
        }

        if(attackNumber == 2)
        {
            transform.Translate((Vector3.down).normalized * Time.deltaTime * speed);
        }

    }

    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }
}
