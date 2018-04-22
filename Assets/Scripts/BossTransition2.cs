///////////////////////////////////////////////////////////////////////
//
//      BossTransition2.cs
//
///////////////////////////////////////////////////////////////////////

using UnityEngine;

public class BossTransition2 : MonoBehaviour
{
    SpriteRenderer sr;
    BoxCollider2D col;
    SpriteMask sm;

    public bool transition = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
        sm = GetComponentInChildren<SpriteMask>();
    }

    void Update()
    {
        if (transition && sr.color.a >= 1.0f - Mathf.Epsilon)
        {
            col.enabled = false;
            sm.enabled = false;
        }

        if (transition && sr.color.a > 0.0f)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - Time.deltaTime * 1.5f);
        }
    }

    public void StartTransition()
    {
        transition = true;
    }

}
