///////////////////////////////////////////////////////////////////////
//
//      GameBeginFlash.cs
//
///////////////////////////////////////////////////////////////////////

using UnityEngine;

public class GameBeginFlash : MonoBehaviour
{
    Car c;
    private void Start()
    {
        c = FindObjectOfType<Car>();

        Invoke("Unlock", 1.0f);
    }

    void Unlock()
    {
        c.lockControls = false;
    }
}
