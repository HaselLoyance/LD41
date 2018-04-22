///////////////////////////////////////////////////////////////////////
//
//      BlinkOnContact.cs
//
///////////////////////////////////////////////////////////////////////

using UnityEngine;

public class BlinkOnContact : MonoBehaviour
{
    public SpriteRenderer sr;
    Color startColor;

    void Start()
    {
        startColor = sr.color;
    }

    void Update()
    {
        if (sr.color.a > Mathf.Epsilon)
        {
            sr.color = new Color(startColor.r, startColor.g, startColor.b, sr.color.a - Time.deltaTime * 5);
        }
    }

    public void Blink()
    {
        sr.color = Color.white;
    }
}
