﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomByView : MonoBehaviour
{

    void Update()
    {
        //鼠标滚轮的效果
        //Camera.main.fieldOfView 摄像机的视野
        //Camera.main.orthographicSize 摄像机的正交投影

        //zoom out
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (Camera.main.fieldOfView <= 100)
                Camera.main.fieldOfView += 2;
            if (Camera.main.orthographicSize <= 20)
                Camera.main.orthographicSize += 0.5f;
        }
        //zoom in
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (Camera.main.fieldOfView > 2)
                Camera.main.fieldOfView -= 2;
            if (Camera.main.orthographicSize >= 1)
                Camera.main.orthographicSize -= 0.5f;
        }

    }
}
