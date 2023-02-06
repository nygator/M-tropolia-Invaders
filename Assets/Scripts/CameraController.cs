using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera cam;
    float size;

    void Awake()
    {
        cam = Camera.main;
        size = cam.orthographicSize;
    }

    void Update()
    {
        float screenRatio = (float)Screen.width / Screen.height;
        float targetRatio = 1; // 1/1

        if (screenRatio >= targetRatio)
        {
            //cam.orthographicSize = size / 2; EDIT: ei kuulu jakaa 2:lla
            cam.orthographicSize = size;
        }
        else
        {
            float scale = targetRatio / screenRatio;
            //cam.orthographicSize = size / 2 * scale; EDIT: ei kuulu jakaa 2:lla
            cam.orthographicSize = size * scale;
        }
    }
}