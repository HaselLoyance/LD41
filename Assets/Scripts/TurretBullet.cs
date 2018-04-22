///////////////////////////////////////////////////////////////////////
//
//      TurretBullet.cs
//
///////////////////////////////////////////////////////////////////////

using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    public float angleChange = 1.0f;
    CarBullet cb;
    Rigidbody2D rb;

    float initialAngle = 0.0f;
    float t = 0.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cb = GetComponent<CarBullet>();
        initialAngle = transform.rotation.eulerAngles.z;
    }

    void Update()
    {
        rb.velocity = Utils.GetVelocityVector(cb.speed, transform.rotation.eulerAngles.z);

        if (t < 0.4f)
        {
            Quaternion q = transform.rotation;
            q.eulerAngles += new Vector3(0, 0, angleChange * Time.deltaTime * 20.0f);
            transform.rotation = q;
        }

        t += Time.deltaTime;
    }
}
