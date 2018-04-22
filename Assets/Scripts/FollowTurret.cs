///////////////////////////////////////////////////////////////////////
//
//      FollowTurret.cs
//
///////////////////////////////////////////////////////////////////////

using UnityEngine;

public class FollowTurret : MonoBehaviour
{
    private bool _enabled = false;
    SpriteRenderer sr;
    Car c;

    public GameObject turretBullet;

    public bool turretEnabled
    {
        get
        {
            return _enabled;
        }

        set
        {
            _enabled = value;
        }
    }

    void Start()
    {
        c = transform.parent.GetComponent<Car>();
        sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }

    void Update()
    {
        if (!turretEnabled && sr.color.a < Mathf.Epsilon)
        {
            return;
        }

        if (turretEnabled && sr.color.a <1.0f)
        {
            sr.color = new Color(1.0f, 1.0f, 1.0f, sr.color.a + Time.deltaTime);
        }
        
        if (!turretEnabled && sr.color.a > Mathf.Epsilon)
        {
            sr.color = new Color(1.0f, 1.0f, 1.0f, sr.color.a - Time.deltaTime);
        }
    }

    public void DoShooty()
    {
        Vector3 rot = transform.rotation.eulerAngles;
        rot.z += 60.0f;
        GameObject bul = Instantiate(turretBullet, transform.position, Quaternion.Euler(rot));
        bul.GetComponent<TurretBullet>().angleChange = -9.0f;

        rot.z -= 60 * 2;
        bul = Instantiate(turretBullet, transform.position, Quaternion.Euler(rot));
        bul.GetComponent<TurretBullet>().angleChange = 9.0f;
    }
}
