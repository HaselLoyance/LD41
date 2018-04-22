///////////////////////////////////////////////////////////////////////
//
//      PressSpaceRestart.cs
//
///////////////////////////////////////////////////////////////////////

using UnityEngine;
using UnityEngine.SceneManagement;

public class PressSpaceRestart : MonoBehaviour
{
    public GameObject flash;

    Vector3 initialPos;
    bool started = false;
    Camera cam;
    private void Start()
    {
        cam=FindObjectOfType<Camera>();
        initialPos = transform.position;
    }

    private void Update()
    {
        transform.position = initialPos + new Vector3(0, 10 * Mathf.Sin(Time.time));

        if (Input.GetKeyDown(KeyCode.Space) && !started)
        {
            Vector3 pos = new Vector3(cam.transform.position.x, cam.transform.position.y, flash.transform.position.z);
            Instantiate(flash, pos, Quaternion.identity).GetComponent<FadeIn>().doTheThing = true;
            started = true;
            Invoke("Transition", 2.0f);
        }
    }

    void Transition()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
