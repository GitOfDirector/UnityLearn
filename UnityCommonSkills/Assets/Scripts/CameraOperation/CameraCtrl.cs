using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraCtrl : MonoBehaviour
{
    #region 常量

    // Default unity names for mouse axes
    const string MOUSEHorizontalAxisName = "Mouse X";
    const string MOUSEVerticalAxisName = "Mouse Y";
    const string MOUSEScrollAxisName = "Mouse ScrollWheel";

    #endregion

    [Tooltip("绕X轴旋转速度")]
    public float XSensitivity = 2f;
    [Tooltip("绕Y轴旋转速度")]
    public float YSensitivity = 2f;
    [Tooltip("视野缩放速度")]
    public float ZSensitivity = 5f;
    [Tooltip("相机移动速度")]
    public float moveSpeed = 2f;

    public bool clampVerticalRotation = true;
    public float MinimumX = -90F;
    public float MaximumX = 90F;

    public bool smooth;
    public float smoothTime = 5f;

    private Quaternion m_CameraYRot;
    private Quaternion m_CameraXRot;

    [Tooltip("控制位移")]
    [SerializeField]
    private Transform m_TranslateTran;
    [Tooltip("控制X轴旋转")]
    [SerializeField]
    private Transform m_RotateXTran;
    [Tooltip("控制Y轴旋转")]
    [SerializeField]
    private Transform m_RotateYTran;

    void Start()
    {
        m_CameraXRot = m_RotateXTran.localRotation;
        m_CameraYRot = m_RotateYTran.localRotation;
    }

    void Update()
    {
        float xValue = Input.GetAxis(MOUSEHorizontalAxisName);
        float yValue = Input.GetAxis(MOUSEVerticalAxisName);
        float scrollValue = Input.GetAxis(MOUSEScrollAxisName);

        #region 拖动

        //拖动屏幕
        //if (Input.GetMouseButton(2))
        //{
        //    float translateY = xValue * XSensitivity;
        //    float translateX = yValue * YSensitivity;

        //    transform.Translate(translateX, translateY, 0);
        //}

        #endregion

        #region 移动

        //前移相机
        if (Input.GetKey(KeyCode.W) && Input.GetMouseButton(1))
        {
            m_TranslateTran.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);
        }

        //左移相机
        if (Input.GetKey(KeyCode.A) && Input.GetMouseButton(1))
        {
            m_TranslateTran.Translate(Vector3.left * Time.deltaTime * moveSpeed, Space.World);
        }

        //后撤相机
        if (Input.GetKey(KeyCode.S) && Input.GetMouseButton(1))
        {
            m_TranslateTran.Translate(Vector3.back * Time.deltaTime * moveSpeed, Space.World);
        }

        //右移相机
        if (Input.GetKey(KeyCode.D) && Input.GetMouseButton(1))
        {
            m_TranslateTran.Translate(Vector3.right * Time.deltaTime * moveSpeed, Space.World);
        }

        #endregion

        #region 旋转

        //旋转相机---以相机为轴点
        if (Input.GetMouseButton(1))
        {
            SetRotation(xValue, yValue);
        }

        //旋转相机---以屏幕中心为轴点
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButton(0))
        {

        }

        #endregion

        #region 缩放

        //拉近或者拉远相机
        float depthTranslation = scrollValue * ZSensitivity;
        transform.Translate(0, 0, depthTranslation);

        #endregion


    }

    /// <summary>
    /// 设置旋转
    /// </summary>
    /// <param name="xValue"></param>
    /// <param name="yValue"></param>
    public void SetRotation(float xValue, float yValue)
    {
        float yRot = xValue * XSensitivity;
        float xRot = yValue * YSensitivity;

        m_CameraYRot *= Quaternion.Euler(0f, yRot, 0f);
        m_CameraXRot *= Quaternion.Euler(-xRot, 0f, 0f);

        if (clampVerticalRotation)
            m_CameraXRot = ClampRotationAroundXAxis(m_CameraXRot);

        if (smooth)
        {
            m_RotateXTran.localRotation = Quaternion.Slerp(m_RotateXTran.localRotation, m_CameraXRot,
                smoothTime * Time.deltaTime);
            m_RotateYTran.localRotation = Quaternion.Slerp(m_RotateYTran.localRotation, m_CameraYRot,
                smoothTime * Time.deltaTime);
        }
        else
        {
            m_RotateXTran.localRotation = m_CameraYRot;
            m_RotateYTran.localRotation = m_CameraXRot;
        }

    }

    /// <summary>
    /// 限制旋转角度
    /// </summary>
    /// <param name="q"></param>
    /// <returns></returns>
    Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }

}
