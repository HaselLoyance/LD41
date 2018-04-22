///////////////////////////////////////////////////////////////////////
//
//      GarboCollectDestruct.cs
//
///////////////////////////////////////////////////////////////////////

using UnityEngine;

public class GarboCollectDestruct : MonoBehaviour
{
    public float time = 10.0f;
    float t = 0.0f;

    void Update()
    {
        t += Time.deltaTime;

        if (t > time)
        {
            Destroy(gameObject);
        }
    }
}
