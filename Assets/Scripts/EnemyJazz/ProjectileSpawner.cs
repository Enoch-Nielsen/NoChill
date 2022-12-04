using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject snowBall = null;
    [SerializeField] private GameObject icicle = null;
    public GameObject player = null;
    private int attackNumber = 0;
    private int totalAttacks = 9;
    
    //Boundries
    private float xMin = 11;
    private float xMax = 16;

    private float yMin = -8;
    private float yMax = 8;

    //transform Variables
    private float xPos1 = 0;
    private float xPos2 = 0;
    private float yPos = 0;
    private Vector3 projectileSpawnPoint = new Vector3(0, 0, 0);

    //Other Important things
    private int side = 1;
    // Start is called before the first frame update
    void Start()
    {
        GetNewTransform();
        attackNumber = GetAttackNumber();
        StartCoroutine(HoldAttack());
        StartCoroutine(AttackR());
        //StartAttack();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartAttack()
    {
        attackNumber = GetAttackNumber();
        if (attackNumber == 1)
        {
            StartCoroutine(Attack1());
            return;
        }

        if (attackNumber == 2)
        {
            StartCoroutine(Attack2());
            return;
        }

        if (attackNumber == 3)
        {
            StartCoroutine(Attack3());
            return;
        }

        if (attackNumber == 4)
        {
            StartCoroutine(Attack4());
            return;
        }

        if (attackNumber == 5)
        {
            StartCoroutine(Attack5());
            return;
        }

        if (attackNumber == 6)
        {
            StartCoroutine(Attack6());
            return;
        }

        if (attackNumber == 7)
        {
            StartCoroutine(Attack7One());
            return;
        }

        if (attackNumber == 8)
        {
            StartCoroutine(Attack8());
        }

        if (attackNumber == 9)
        {
            StartCoroutine(Attack9());
            return;
        }
    }


    private IEnumerator Attack1()
    {
        for (int i = 0; i < 40; i++)
        {
            GetNewTransform();
            snowBall.GetComponent<Projectile>().attackNumber = attackNumber;
            Instantiate(snowBall, projectileSpawnPoint, snowBall.transform.rotation);
            yield return new WaitForSeconds(0.2f);
        }

        StartCoroutine(HoldAttack());
    }

    private IEnumerator Attack2()
    {
        yield return new WaitForSeconds(1.0f);
        projectileSpawnPoint.x = -10;
        projectileSpawnPoint.y = 13;
        for (int i = 0; i < 55; i++)
        {
            snowBall.GetComponent<Projectile>().attackNumber = attackNumber;
            Instantiate(snowBall, projectileSpawnPoint, snowBall.transform.rotation);
            projectileSpawnPoint.x += 2;
            if(projectileSpawnPoint.x > 10)
            {
                projectileSpawnPoint.x = -10;
                projectileSpawnPoint.y -= 2;
            }
        }
        StartCoroutine(HoldAttack());
    }
    
    private IEnumerator Attack3()
    {
        yield return new WaitForSeconds(0.5f);
        projectileSpawnPoint.x = -11;
        projectileSpawnPoint.y = Random.Range(-0.5f, 5.5f);
        icicle.GetComponent<Projectile>().attackNumber = attackNumber;
        for(int i = 0; i < 5; i++)
        {
            Instantiate(icicle, projectileSpawnPoint, icicle.transform.rotation);
            projectileSpawnPoint.y -= 1;

        }
        StartCoroutine(HoldAttack());
    }

    private IEnumerator Attack4()
    {
        yield return new WaitForSeconds(0.5f);
        projectileSpawnPoint.x = -11;
        projectileSpawnPoint.y = 4.5f;
        snowBall.GetComponent<Projectile>().attackNumber = attackNumber;
        for(int i = 0; i < 5; i++)
        {
            Instantiate(snowBall, projectileSpawnPoint, snowBall.transform.rotation);
            projectileSpawnPoint.y -= 2;
        }
        StartCoroutine(HoldAttack());
    }

    private IEnumerator Attack5()
    {
        icicle.GetComponent<Projectile>().attackNumber = attackNumber;
        for(int i = 0; i < 15; i++)
        {
            yield return new WaitForSeconds(0.4f);
            GetNewTransformTwo();
            Instantiate(icicle, projectileSpawnPoint, icicle.transform.rotation);
        }
        StartCoroutine(HoldAttack());
    }

    private IEnumerator AttackR()
    {
        float waitTime = Random.Range(0.1f, 10f);
        yield return new WaitForSeconds(waitTime);
        float randomProjectile = Random.Range(-2, 3);
        if(randomProjectile < 0)
        {
            icicle.GetComponent<Projectile>().attackNumber = 1;
            Instantiate(icicle, projectileSpawnPoint, icicle.transform.rotation);
        }else if(randomProjectile > 0)
        {
            snowBall.GetComponent<Projectile>().attackNumber = 1;
            Instantiate(snowBall, projectileSpawnPoint, snowBall.transform.rotation);
        }

        StartCoroutine(AttackR());
    }

    private IEnumerator Attack6()
    {
        int whichOne = 1;
        snowBall.GetComponent<Projectile>().speed /= 1.5f;
        icicle.GetComponent<Projectile>().speed /= 1.5f;
        snowBall.GetComponent<Projectile>().destroyTime *= 3;
        icicle.GetComponent<Projectile>().destroyTime *= 3;
        for (int i = 0; i < 25; i++)
        {
            GetNewTransform();
            snowBall.GetComponent<Projectile>().attackNumber = attackNumber;
            icicle.GetComponent<Projectile>().attackNumber = attackNumber;




            if (whichOne > 0)
            {
                Instantiate(snowBall, projectileSpawnPoint, snowBall.transform.rotation);
            }
            else
            {
                Instantiate(icicle, projectileSpawnPoint, snowBall.transform.rotation);
            }
            whichOne *= -1;
        }
        snowBall.GetComponent<Projectile>().speed *= 1.5f;
        icicle.GetComponent<Projectile>().speed *= 1.5f;
        snowBall.GetComponent<Projectile>().destroyTime /= 3;
        icicle.GetComponent<Projectile>().destroyTime /= 3;
        yield return new WaitForSeconds(1.0f);

        StartCoroutine(HoldAttack());
    }

    private IEnumerator Attack7One()
    {
        projectileSpawnPoint.y = 4.5f;
        projectileSpawnPoint.x = -11;
        for (int i = 0; i < 8; i++)
        {
            icicle.GetComponent<Projectile>().attackNumber = attackNumber;
            yield return new WaitForSeconds(0.2f);
            Instantiate(icicle, projectileSpawnPoint, icicle.transform.rotation);
            projectileSpawnPoint.y--;
        }

        StartCoroutine(Attack7Two());
    }

    private IEnumerator Attack7Two()
    {
        yield return new WaitForSeconds(0.5f);
        projectileSpawnPoint.y = 2.5f;
        projectileSpawnPoint.x = -11;
        for (int i = 0; i < 8; i++)
        {
            icicle.GetComponent<Projectile>().attackNumber = attackNumber;
            yield return new WaitForSeconds(0.2f);
            Instantiate(icicle, projectileSpawnPoint, icicle.transform.rotation);
            projectileSpawnPoint.y--;
        }
        StartCoroutine(HoldAttack());
    }

    private IEnumerator Attack8()
    {
        icicle.GetComponent<Projectile>().attackNumber = attackNumber;
        StartCoroutine(Attack7One());
        yield return new WaitForSeconds(0.1f);
    }

    private IEnumerator Attack9()
    {
        projectileSpawnPoint.y = 6;
        projectileSpawnPoint.x = -3;
        snowBall.GetComponent<Projectile>().attackNumber = attackNumber;

        for (int i = 0; i < 7; i++)
        {
            Instantiate(snowBall, projectileSpawnPoint, snowBall.transform.rotation);
            projectileSpawnPoint.x++;
        }
        yield return new WaitForSeconds(1);
        projectileSpawnPoint.x = -11;
        for(int e = 0; e < 7; e++)
        {
            Instantiate(snowBall, projectileSpawnPoint, snowBall.transform.rotation);
            projectileSpawnPoint.x++;
        }
        projectileSpawnPoint.x = 11;
        for(int a = 0; a < 7; a++)
        {
            Instantiate(snowBall, projectileSpawnPoint, snowBall.transform.rotation);
            projectileSpawnPoint.x--;
        }

        StartCoroutine(HoldAttack());
    }

    private IEnumerator HoldAttack()
    {
        yield return new WaitForSeconds(1.5f);
        StartAttack();
    }

    private int GetAttackNumber()
    {
        return Random.Range(1, totalAttacks + 1);
    }

    private void GetNewTransform()
    {
        float xPos = 0;
        side *= -1;
        xPos1 = Random.Range(xMin, xMax);
        yPos = Random.Range(yMin, yMax);
        xPos2 = Random.Range(-xMin, -xMax);
        if(side > 0)
        {
            xPos = xPos1;
        }else if (side < 0)
        {
            xPos = xPos2;
        }
        Vector3 randomSpawnPosition = new Vector3(xPos, yPos, 0.0f);
        projectileSpawnPoint = randomSpawnPosition;
    }

    private void GetNewTransformTwo()
    {
        float yPos = Random.Range(6, 9) * side;
        side *= -1;
        xPos1 = Random.Range(-11, 12);
        projectileSpawnPoint = new Vector3(xPos1, yPos, 0.0f);
    }

    public void IncreaseMoves(int newMoveCount)
    {
        totalAttacks = newMoveCount;
    }
}
