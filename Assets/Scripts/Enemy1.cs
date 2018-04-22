///////////////////////////////////////////////////////////////////////
//
//      Enemy1.cs
//      CompSci 40S, 2017-2018, Yaroslav Mikhaylik - HaselLoyance
//
///////////////////////////////////////////////////////////////////////

using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public float shootSpeedOrig = 5.0f;
    public uint shootRays = 2;
    public GameObject enemyBullet;

    Enemy e;
    bool shooting = true;
    float shootSpeed = 5.0f;

    void Start()
    {
        e = GetComponentInParent<Enemy>();
        shootSpeed = shootSpeedOrig * e.diffCoeff;
        if (e.diffCoeff > 2.5f)
        {
            shootRays = 4;
        }

        if (e.diffCoeff > 4.0f)
        {
            shootRays = 8;
        }

        Invoke("Shoot", 0.0f);
    }

    void Update()
    {
        if (!e.isDead && !shooting && !e.distanceLimited)
        {
            shooting = true;
            shootSpeed = shootSpeedOrig * e.diffCoeff;
            if (e.diffCoeff > 4.0f)
            {
                shootRays = 4;
            }
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

        for(int i =0; i < shootRays; i++)
        {
            Instantiate(enemyBullet, transform.position, Quaternion.Euler(new Vector3(0, 0, transform.rotation.eulerAngles.z + i * (360.0f / shootRays))))
            .GetComponent<EnemyBullet>().diffCoef = e.diffCoeff;
        }

        Invoke("Shoot", 1.0f/shootSpeed);
    }
}
