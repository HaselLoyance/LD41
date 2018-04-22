///////////////////////////////////////////////////////////////////////
//
//      SlowRotation.cs
//
///////////////////////////////////////////////////////////////////////

using UnityEngine;

public class SlowRotation : MonoBehaviour
{
    public float speed = 0.0f;

    void Update()
    {
        Quaternion q = transform.rotation;
        q.eulerAngles += new Vector3(0, 0, speed * Time.deltaTime);
        transform.rotation = q;
    }
}
