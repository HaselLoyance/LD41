///////////////////////////////////////////////////////////////////////
//
//      Enemy3.cs
//
///////////////////////////////////////////////////////////////////////

using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    public float shootSpeedOrig = 0.1f;
    public GameObject enemyBullet;

    Enemy e;
    bool shooting = true;
    float shootSpeed = 1.0f;

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

        if (!e.isDead && shooting && !e.distanceLimited)
        {
            Vector3 pos = transform.position;
            Vector3 mPos = e.c.transform.position;

            mPos.x = mPos.x - pos.x;
            mPos.y = mPos.y - pos.y;
            float angle = Mathf.Atan2(mPos.y, mPos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    void Shoot()
    {
        if ((e.isDead || e.distanceLimited) && shooting)
        {
            shooting = false;
            return;
        }

        Instantiate(enemyBullet, transform.position, Quaternion.Euler(new Vector3(0, 0, transform.rotation.eulerAngles.z)))
            .GetComponent<EnemyBullet>().diffCoef = e.diffCoeff;

        Invoke("Shoot", 1.0f / shootSpeed);
    }
}
