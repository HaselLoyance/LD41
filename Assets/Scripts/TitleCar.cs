///////////////////////////////////////////////////////////////////////
//
//      TitleCar.cs
//
///////////////////////////////////////////////////////////////////////

using UnityEngine;

public class TitleCar : MonoBehaviour
{
    float linVel = 200.0f;
    float rotVel = 50.0f;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = Utils.GetVelocityVector(linVel, transform.rotation.eulerAngles.z);
        
        Quaternion q = transform.rotation;
        q.eulerAngles += new Vector3(0, 0, rotVel * Time.deltaTime);
        transform.rotation = q;
    }
}
