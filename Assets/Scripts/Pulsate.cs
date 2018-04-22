///////////////////////////////////////////////////////////////////////
//
//      Pulsate.cs
//
///////////////////////////////////////////////////////////////////////

using UnityEngine;

public class Pulsate : MonoBehaviour
{
    public float speed = 1.0f;
    public float amplitude = 0.1f;

    Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        transform.localScale = originalScale + new Vector3(amplitude * Mathf.Sin(speed * Time.time), amplitude * Mathf.Cos(speed * Time.time), 0);
    }
}
