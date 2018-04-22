///////////////////////////////////////////////////////////////////////
//
//      Enemy2.cs
//
///////////////////////////////////////////////////////////////////////

using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public float shootSpeedOrig = 15.0f;
    public GameObject enemyBullet;

    Enemy e;
    bool shooting = true;
    float shootSpeed = 5.0f;

    void Start()
    {
        e = GetComponentInParent<Enemy>();
        shootSpeed = shootSpeedOrig * e.diffCoeff;
        Invoke("Shoot", 0.0f);
    }

    void Update()
    {
        if (!e.isDead && !shooting && !e.distanceLimited)
        {
            shooting = true;
            shootSpeed = shootSpeedOrig * e.diffCoeff;
            Invoke("Shoot", 0.0f);
        }
    }

    void Shoot()
    {
        if ((e.isDead || e.distanceLimited) && shooting)
        {
            shooting = false;
            return;
        }

        Instantiate(enemyBullet, transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0.0f,360.0f))))
            .GetComponent<EnemyBullet>().diffCoef = e.diffCoeff;

        Invoke("Shoot", 1.0f / shootSpeed);
    }

}
