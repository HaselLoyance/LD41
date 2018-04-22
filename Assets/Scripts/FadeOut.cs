///////////////////////////////////////////////////////////////////////
//
//      FadeOut.cs
//      CompSci 40S, 2017-2018, Yaroslav Mikhaylik - HaselLoyance
//
///////////////////////////////////////////////////////////////////////

using UnityEngine;

public class FadeOut : MonoBehaviour
{
    public float speed = 5.0f;
    public bool doTheThing = false;
    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (doTheThing)
            sr.color = new Color(1.0f, 1.0f, 1.0f, sr.color.a - Time.deltaTime * speed);
    }
}
