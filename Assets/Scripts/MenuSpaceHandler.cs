///////////////////////////////////////////////////////////////////////
//
//      MenuSpaceHandler.cs
//
///////////////////////////////////////////////////////////////////////

using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSpaceHandler : MonoBehaviour
{
    public GameObject flash;
    
    Vector3 initialPos;
    bool started = false;

    private void Start()
    {
        initialPos = transform.position;
    }

    private void Update()
    {
        transform.position = initialPos + new Vector3(0, 10 * Mathf.Sin(Time.time));

        if(Input.GetKeyDown(KeyCode.Space) && !started)
        {
            Instantiate(flash).GetComponent<FadeIn>().doTheThing = true;
            started = true;
            GameManager.Instance.sm.PlaySound("Agree");
            Invoke("Transition", 2.0f);
        }
    }

    void Transition()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
