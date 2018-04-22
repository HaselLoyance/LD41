///////////////////////////////////////////////////////////////////////
//
//      EndTrigger.cs
//
///////////////////////////////////////////////////////////////////////

using UnityEngine;
using UnityEngine.UI;

public class EndTrigger : MonoBehaviour
{
    public GameObject credits;
    public GameObject flash;

    Car c;
    Camera cam;

    void Start()
    {
        cam = FindObjectOfType<Camera>();
        c = FindObjectOfType<Car>();
        GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Car")
        {
            c.lockControls = true;
            c.rb.velocity = new Vector2(350.0f, 0.0f);
            c.transform.rotation = Quaternion.identity;

            Invoke("CREDITS", 8.0f);
        }
    }

    void CREDITS()
    {
        GameManager.Instance.sm.FadeOutLevelMusic();
        GameManager.Instance.sm.PlayLevelMusic("sTitle");

        foreach (ParticleSystem ps in c.GetComponentsInChildren<ParticleSystem>())
        {
            ps.startColor = Color.black;
        }

        Destroy(FindObjectOfType<BGColorizer>().gameObject);
        Destroy(FindObjectOfType<BGPiece>().gameObject);
        cam.backgroundColor = Color.white;
        c.GetComponent<SpriteRenderer>().color = Color.black;
        FindObjectOfType<Canvas>().enabled = false;
        c.rb.velocity = new Vector2(280.0f, 0.0f);
        GameObject go = Instantiate(credits);
        GameManager.Instance.CalcScore();
        go.transform.GetChild(go.transform.childCount - 1).GetComponent<Text>().text = "Score: " + GameManager.Instance.SCORE;
        go.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
        go.GetComponent<Canvas>().worldCamera = cam;

        Invoke("EnoughCredits", 20);
    }

    void EnoughCredits()
    {
        GameObject go = Instantiate(flash);
        go.transform.parent = cam.transform;
        go.transform.position = cam.transform.TransformPoint(new Vector3(0, 0, 10));
    }
}
