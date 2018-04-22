///////////////////////////////////////////////////////////////////////
//
//      WhiteFlash.cs
//
///////////////////////////////////////////////////////////////////////

using UnityEngine;

public class WhiteFlash : MonoBehaviour
{
    SpriteRenderer sr;

     void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        Invoke("DIE", 6.0f);
    }

    private void Update()
    {
        sr.color = new Color(1.0f, 1.0f, 1.0f, sr.color.a + Time.deltaTime / 5.0f);
    }

    void DIE()
    {
        Application.Quit();
    }
}
