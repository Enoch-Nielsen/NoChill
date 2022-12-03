using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject snowBall = null;
    [SerializeField] private GameObject icicle = null;
    [SerializeField] private GameObject player = null;
    private int attackNumber = 0;
    private int totalAttacks = 2;
    
    //Boundries
    private float xMin = 0;
    private float xMax = 10;
    private float yMin = 0;
    private float yMax = 10;

    //transform Variables
    private float xPos = 0;
    private float yPos = 0;
    private Vector3 projectileSpawnPoint = new Vector3(0, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        GetNewTransform();
        attackNumber = GetAttackNumber();
        StartCoroutine(holdAttack());
        //StartAttack();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartAttack()
    {
        attackNumber = GetAttackNumber();
        if(attackNumber == 1)
        {
            Attack1();
            return;
        }

        if (attackNumber == 2)
        {
            Attack2();
            return;
        }

        if (attackNumber == 3)
        {
            //Attack3);
            return;
        }

        if (attackNumber == 4)
        {
            //Attack4();
            return;
        }
    }

    private void Attack1()
    {
        /*for(int i = 1; i < 4; i++)
        {
            GetNewTransform();
            Instantiate(snowBall, projectileSpawnPoint, snowBall.transform.rotation);
            
        }*/
        StartCoroutine(StartAttack1());

        StartCoroutine(holdAttack());
    }

    private void Attack2()
    {
        Attack1();
    }

    private IEnumerator StartAttack1()
    {
        for (int i = 0; i < 50; i++)
        {
            GetNewTransform();
            Instantiate(snowBall, projectileSpawnPoint, snowBall.transform.rotation);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator holdAttack()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("Hamburger");
        StartAttack();
    }

    private int GetAttackNumber()
    {
        return Random.Range(1, totalAttacks);
    }

    private void GetNewTransform()
    {
        xPos = Random.Range(xMin, xMax);
        yPos = Random.Range(yMin, yMax);
        Vector3 randomSpawnPosition = new Vector3(xPos, yPos, 0.0f);
        projectileSpawnPoint = randomSpawnPosition;
    }
}
