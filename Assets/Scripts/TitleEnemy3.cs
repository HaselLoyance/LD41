///////////////////////////////////////////////////////////////////////
//
//      TitleEnemy3.cs
//
///////////////////////////////////////////////////////////////////////

using UnityEngine;

public class TitleEnemy3 : MonoBehaviour
{
    public GameObject enemyBullet;
    float shootSpeedOrig = 5;
    TitleCar c;

    void Start()
    {
        c = FindObjectOfType<TitleCar>();
        CalcAngle();
        Invoke("Shoot", 0.0f);
    }

    void CalcAngle()
    {
        Vector3 pos = transform.position;
        Vector3 mPos = c.transform.position;

        mPos.x = mPos.x - pos.x;
        mPos.y = mPos.y - pos.y;
        float angle = Mathf.Atan2(mPos.y, mPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Update()
    {
        CalcAngle();
    }

    void Shoot()
    {
        Instantiate(enemyBullet, transform.position, Quaternion.Euler(new Vector3(0, 0, transform.rotation.eulerAngles.z)));

        Invoke("Shoot", 1.0f / shootSpeedOrig);
    }
}
