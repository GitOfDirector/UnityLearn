using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDrag3D : MonoBehaviour
{
    public float smoothSpeed = 2f;

    Vector3 mouse3dPos;

    IEnumerator OnMouseDown()
    {
        Debug.Log("On Mouse Down");

        Vector3 cubeScreenPos = Camera.main.WorldToScreenPoint(transform.position);

         mouse3dPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cubeScreenPos.z);

        //2.只有3维坐标情况下才能来计算鼠标位置与物理的距离，offset即是距离
        //将鼠标屏幕坐标转为三维坐标，再算出物体位置与鼠标之间的距离
         //Vector3 offset = transform.position - Camera.main.ScreenToWorldPoint(mouse3dPos);
         Vector3 offset = Vector3.zero;

        while (Input.GetMouseButton(0))
        {
            mouse3dPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cubeScreenPos.z);

            Vector3 curTragetPos = Camera.main.ScreenToWorldPoint(mouse3dPos) + offset;

            //transform.position = curTragetPos;
            transform.position = Vector3.Lerp(transform.position, curTragetPos, Time.deltaTime *smoothSpeed);

            yield return new WaitForFixedUpdate();

        }

    }
}
