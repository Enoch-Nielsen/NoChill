using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] public float speed = 1;
    private GameObject player = null;
    [SerializeField] private bool isIcicle = false;
    public int attackNumber = 1;
    public int leftRight = 1;
    private Quaternion xRotation;
    public float destroyTime = 3;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if(attackNumber == 1 || attackNumber == 5 || attackNumber == 6)
        {
            transform.LookAt(player.transform);
        }

        StartCoroutine(SelfDestruct());
    }

    // Update is called once per frame
    void Update()
    {
        if(attackNumber == 1 || attackNumber == 5 || attackNumber == 6)
        {
            transform.Translate((Vector3.forward).normalized * Time.deltaTime * speed);
        }

        if(attackNumber == 2)
        {
            transform.Translate((Vector3.down).normalized * Time.deltaTime * speed);
        }

        if(attackNumber == 3)
        {
            transform.Translate(Vector3.forward.normalized * Time.deltaTime * speed);
        }

        if(attackNumber == 4)
        {
            transform.Translate(Vector3.forward.normalized * Time.deltaTime * speed);
        }

    }

    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(this.gameObject);
    }

    public Transform GetTransform()
    {
        return this.transform;
    }
}
