///////////////////////////////////////////////////////////////////////
//
//      TrackTurret.cs
//
///////////////////////////////////////////////////////////////////////

using UnityEngine;

public class TrackTurret : MonoBehaviour
{
    public GameObject carBullet3;
    Camera c;
    Car car;
    bool bomb = false;
    float bombT = 0.0f;

    void Start()
    {
        c = FindObjectOfType<Camera>();
        car = transform.parent.GetComponent<Car>();
    }

    void Update()
    {
        if (!bomb)
        {
            Vector3 pos = c.WorldToScreenPoint(transform.position);
            Vector3 mPos = Input.mousePosition;

            mPos.x = mPos.x - pos.x;
            mPos.y = mPos.y - pos.y;
            float angle = Mathf.Atan2(mPos.y, mPos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        else
        {
            Quaternion q = transform.rotation;
            q.eulerAngles += new Vector3(0, 0, 360.0f * Time.deltaTime);
            transform.rotation = q;

            bombT += Time.deltaTime;

            if (bombT >= 1.0f)
            {
                bomb = false;
                bombT = 0.0f;
                car.lockTrackTurret = false;
                CancelInvoke("hujak");
            }
        }
    }

    public void Bomb()
    {
        bomb = true;
        InvokeRepeating("hujak", 0.0f, 1.0f / 80.0f);
    }

    public void hujak()
    {
        Instantiate(carBullet3, transform.position, transform.rotation);
    }
}
