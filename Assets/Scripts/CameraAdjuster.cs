///////////////////////////////////////////////////////////////////////
//
//      CameraAdjuster.cs
//
///////////////////////////////////////////////////////////////////////

using UnityEngine;

public class CameraAdjuster : MonoBehaviour
{
    public bool disableTrackerInThisLevel = false;
    
    Car car = null;
    Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        
        cam.orthographicSize = GameManager.TILE_SIZE_UNITS * GameManager.N_TILES_VER / 2;
    }

    void Update()
    {
        if (disableTrackerInThisLevel)
        {
            return;
        }
        
        if (car == null && GameObject.FindGameObjectWithTag("Car") != null)
        {
            car = GameObject.FindGameObjectWithTag("Car").GetComponent<Car>();
        }

        if (car != null && !car.isDead)
        {            
            Vector3 newPos = car.transform.position;
            newPos.z = transform.position.z;
            transform.position = newPos;
        }
    }
}
