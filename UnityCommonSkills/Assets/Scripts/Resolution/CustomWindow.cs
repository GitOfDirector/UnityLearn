using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomWindow : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler
{
    public Vector2 minSize = new Vector2(100, 100);
    public Vector2 maxSize = new Vector2(1920, 1080);

    [SerializeField]
    Transform m_Corner1;
    [SerializeField]
    Transform m_Corner2;
    [SerializeField]
    Transform m_Corner3;
    [SerializeField]
    Transform m_Corner4;
    [SerializeField]
    Texture2D m_ResizeTexCorner45;
    [SerializeField]
    Texture2D m_ResizeTexCorner135;
    [SerializeField]
    Texture2D m_ResizeTexHor;
    [SerializeField]
    Texture2D m_ResizeTexVer;
    [SerializeField]
    Texture2D m_DefaultTex;

    //是否选择控件
    private bool isSelectContorl = false;
    //距离边界直线的最小有效距离
    private float minDisToLine = 25.0f;
    //是否处于Panel之内
    private bool isInPanelArea = false;
    //鼠标坐标
    private Vector3 mousePos;
    //距各个边线的距离
    private List<DisToLineData> disToLineData = new List<DisToLineData>();
    //操作类型
    private OperationType opt = OperationType.T;
    //鼠标相对位置
    private LocationRelativeToWindow mouseLocation = LocationRelativeToWindow.Inside;
    //鼠标相对位置
    private LocationRelativeToWindow oldMouseLocation = LocationRelativeToWindow.Unknown;

    //变换
    private RectTransform panelRectTransform;
    //父物体变换
    private RectTransform parentRectTransform;
    //旧的鼠标坐标
    private Vector2 originalLocalPointerPosition;
    //旧的大小
    private Vector2 originalSizeDelta;
    //旧的坐标
    private Vector3 originalPanelLocalPosition;


    void Awake()
    {
        panelRectTransform = transform.GetComponent<RectTransform>();
        parentRectTransform = transform.parent as RectTransform;
    }

    void Start()
    {
        disToLineData.Add(new DisToLineData { rank = 0, distance = 404, lineSite = BoundarySite.UP });
        disToLineData.Add(new DisToLineData { rank = 1, distance = 303, lineSite = BoundarySite.RIGHT });
        disToLineData.Add(new DisToLineData { rank = 2, distance = 202, lineSite = BoundarySite.DOWN });
        disToLineData.Add(new DisToLineData { rank = 3, distance = 101, lineSite = BoundarySite.LEFT });

        maxSize = new Vector2(Screen.width, Screen.height);

        //EvaluateMouseLocation(disToLineData);
    }

    void Update()
    {
        #region 操作类型
        if (Input.GetKeyUp(KeyCode.Q))
        {
            opt = OperationType.Q;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            opt = OperationType.W;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            opt = OperationType.E;
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            opt = OperationType.R;
        }
        if (Input.GetKeyUp(KeyCode.T))
        {
            opt = OperationType.T;
        }
        #endregion

        //OnMouseMove();
    }

    #region 鼠标位置

    void OnMouseMove()
    {
        mousePos = Input.mousePosition;

        //默认从左上角开始计算（暂不考虑旋转）
        float disTo_Up_Line = CalculateDisPointToLine(mousePos, m_Corner1.position, m_Corner2.position);
        float disTo_Right_Line = CalculateDisPointToLine(mousePos, m_Corner2.position, m_Corner3.position);
        float disTo_Down_Line = CalculateDisPointToLine(mousePos, m_Corner3.position, m_Corner4.position);
        float disTo_Left_Line = CalculateDisPointToLine(mousePos, m_Corner4.position, m_Corner1.position);

        disToLineData[0].distance = disTo_Up_Line;
        disToLineData[1].distance = disTo_Right_Line;
        disToLineData[2].distance = disTo_Down_Line;
        disToLineData[3].distance = disTo_Left_Line;

        //判断鼠标相对于控件边界的位置
        mouseLocation = EvaluateMouseLocation(disToLineData);

        Debug.Log("鼠标相对位置：" + mouseLocation);
        //更改鼠标样式
        if (mouseLocation != oldMouseLocation)
        {
            oldMouseLocation = mouseLocation;
            switch (mouseLocation)
            {
                case LocationRelativeToWindow.Up:
                case LocationRelativeToWindow.Down:
                    Cursor.SetCursor(m_ResizeTexVer, Vector2.zero, CursorMode.Auto);
                    break;
                case LocationRelativeToWindow.Right:
                case LocationRelativeToWindow.Left:
                    Cursor.SetCursor(m_ResizeTexHor, Vector2.zero, CursorMode.Auto);
                    break;
                case LocationRelativeToWindow.RightUp:
                case LocationRelativeToWindow.LeftDown:
                    Cursor.SetCursor(m_ResizeTexCorner45, Vector2.zero, CursorMode.Auto);
                    break;
                case LocationRelativeToWindow.RightDown:
                case LocationRelativeToWindow.LeftUp:
                    Cursor.SetCursor(m_ResizeTexCorner135, Vector2.zero, CursorMode.Auto);
                    break;
                case LocationRelativeToWindow.Inside:
                    Cursor.SetCursor(m_DefaultTex, Vector2.zero, CursorMode.Auto);
                    break;
                case LocationRelativeToWindow.Outside:
                default:
                    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                    break;
            }
        }


    }

    /// <summary>
    /// 评估鼠标的位置
    /// </summary>
    private LocationRelativeToWindow EvaluateMouseLocation(List<DisToLineData> disToLineData)
    {
        CustomSort(disToLineData);

        Debug.Log("最小距离：" + disToLineData[0].distance);
        if (disToLineData[0].distance > minDisToLine)//如果最小距离仍然大于最小判断间隔
        {
            if (isInPanelArea)
            {
                return LocationRelativeToWindow.Inside;
            }
            else
            {
                return LocationRelativeToWindow.Outside;
            }            
        }
        else
        {
            //更好的方式是用if-else
            switch (disToLineData[0].lineSite)
            {
                case BoundarySite.UP:
                    if (disToLineData[1].distance > minDisToLine)
                    {
                        return LocationRelativeToWindow.Up;
                    }
                    else
                    {
                        if (disToLineData[1].lineSite == BoundarySite.RIGHT)
                        {
                            return LocationRelativeToWindow.RightUp;
                        }
                        else
                        {
                            return LocationRelativeToWindow.RightDown;
                        }
                    }
                case BoundarySite.RIGHT:
                    if (disToLineData[1].distance > minDisToLine)
                    {
                        return LocationRelativeToWindow.Right;
                    }
                    else
                    {
                        if (disToLineData[1].lineSite == BoundarySite.UP)
                        {
                            return LocationRelativeToWindow.RightUp;
                        }
                        else
                        {
                            return LocationRelativeToWindow.RightDown;
                        }
                    }
                case BoundarySite.DOWN:
                    if (disToLineData[1].distance > minDisToLine)
                    {
                        return LocationRelativeToWindow.Down;
                    }
                    else
                    {
                        if (disToLineData[1].lineSite == BoundarySite.RIGHT)
                        {
                            return LocationRelativeToWindow.RightDown;
                        }
                        else
                        {
                            return LocationRelativeToWindow.LeftDown;
                        }
                    }
                case BoundarySite.LEFT:
                    if (disToLineData[1].distance > minDisToLine)
                    {
                        return LocationRelativeToWindow.Left;
                    }
                    else
                    {
                        if (disToLineData[1].lineSite == BoundarySite.UP)
                        {
                            return LocationRelativeToWindow.LeftUp;
                        }
                        else
                        {
                            return LocationRelativeToWindow.LeftDown;
                        }
                    }
                default:
                    return LocationRelativeToWindow.Outside;
            }
        }

    }

    /// <summary>
    /// 对距离进行排序
    /// </summary>
    private void CustomSort(List<DisToLineData> disToLineData)
    {
        disToLineData.Sort(DisCompare);
        UpdateRank(disToLineData);

        //foreach (var item in disToLineData)
        //{
        //    Debug.Log(item.rank + "==" + item.distance + "==" + item.lineSite);
        //}
    }

    private int DisCompare(DisToLineData x, DisToLineData y)
    {
        if (x.distance > y.distance)
        {
            return 1;
        }
        else if (x.distance < y.distance)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }

    private void UpdateRank(List<DisToLineData> disToLineData)
    {
        for (int i = 0; i < disToLineData.Count; i++)
        {
            disToLineData[i].rank = i;
        }
    }

    /// <summary>
    /// 计算点到直线的距离
    /// </summary>
    private float CalculateDisPointToLine(Vector3 mousePos, Vector3 vecStart, Vector3 vecEnd)
    {
        float disToLine = 0;

        Vector3 vectorS_E = vecEnd - vecStart;
        Vector3 vectorS_Mouse = mousePos - vecStart;

        //float dot = Vector3.Dot(vector1_2, vector1_Mouse);
        Vector3 cross = Vector3.Cross(vecStart, vectorS_Mouse);

        float vectorS_ELen = Vector3.Magnitude(vectorS_E);

        if (vectorS_ELen <= Mathf.Epsilon)//如果边长过小，则跳过
        {
            disToLine = 0;
        }
        else
        {
            float s = Vector3.Magnitude(cross);
            disToLine = s / vectorS_ELen;
        }

        return disToLine;
    }

    #endregion


    /// <summary>
    /// 选择控件
    /// </summary>
    /// <param name="eventData"></param>
    void ISelectHandler.OnSelect(BaseEventData eventData)
    {
        isSelectContorl = true;
    }

    /// <summary>
    /// 取消选择
    /// </summary>
    /// <param name="eventData"></param>
    void IDeselectHandler.OnDeselect(BaseEventData eventData)
    {
        isSelectContorl = false;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        originalPanelLocalPosition = panelRectTransform.localPosition;
        originalSizeDelta = panelRectTransform.sizeDelta;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(panelRectTransform, eventData.position, eventData.pressEventCamera, out originalLocalPointerPosition);
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        //Q---拖动屏幕
        //W---改变位置
        //E---改变旋转
        //T---1，改变缩放
        //      2，改变位置

        //Debug.Log("当前操作类型：" + opt);
        switch (opt)
        {
            case OperationType.Q:
                break;
            case OperationType.W:
                break;
            case OperationType.E:
                break;
            case OperationType.R:
                break;
            case OperationType.T:
                switch (mouseLocation)
                {
                    case LocationRelativeToWindow.Up:
                    case LocationRelativeToWindow.Right:
                    case LocationRelativeToWindow.Down:
                    case LocationRelativeToWindow.Left:
                    case LocationRelativeToWindow.RightUp:
                    case LocationRelativeToWindow.RightDown:
                    case LocationRelativeToWindow.LeftUp:
                    case LocationRelativeToWindow.LeftDown:
                        ResizePanel(eventData);
                        break;
                    case LocationRelativeToWindow.Inside:
                        DragPanel(eventData);
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 调整Panel大小
    /// </summary>
    void ResizePanel(PointerEventData data)
    {
        if (panelRectTransform == null)
            return;

        Vector2 localPointerPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(panelRectTransform, data.position, data.pressEventCamera, out localPointerPosition);
        Vector3 offsetToOriginal = localPointerPosition - originalLocalPointerPosition;

        Vector2 sizeDelta = originalSizeDelta + new Vector2(offsetToOriginal.x, -offsetToOriginal.y);
        sizeDelta = new Vector2(
            Mathf.Clamp(sizeDelta.x, minSize.x, maxSize.x),
            Mathf.Clamp(sizeDelta.y, minSize.y, maxSize.y)
        );

        panelRectTransform.sizeDelta = sizeDelta;
    }

    /// <summary>
    /// 拖拽更改Panel位置
    /// </summary>
    void DragPanel(PointerEventData data)
    {
        if (panelRectTransform == null || parentRectTransform == null)
            return;

        //Debug.Log("Here Drag...");
        Vector2 localPointerPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, data.position, data.pressEventCamera, out localPointerPosition))
        {
            Vector3 offsetToOriginal = localPointerPosition - originalLocalPointerPosition;
            panelRectTransform.localPosition = originalPanelLocalPosition + offsetToOriginal;
        }

        ClampToWindow();
    }

    /// <summary>
    /// 将拖拽区域限制在父物体之内
    /// </summary>
    void ClampToWindow()
    {
        Vector3 pos = panelRectTransform.localPosition;

        Vector3 minPosition = parentRectTransform.rect.min - panelRectTransform.rect.min;
        Vector3 maxPosition = parentRectTransform.rect.max - panelRectTransform.rect.max;

        pos.x = Mathf.Clamp(panelRectTransform.localPosition.x, minPosition.x, maxPosition.x);
        pos.y = Mathf.Clamp(panelRectTransform.localPosition.y, minPosition.y, maxPosition.y);

        panelRectTransform.localPosition = pos;
    }


    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        isInPanelArea = true;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        isInPanelArea = false;
    }

}

/// <summary>
/// 到直线的距离数据
/// </summary>
public class DisToLineData
{
    public int rank = 0;
    public float distance = 0.0f;
    public BoundarySite lineSite = BoundarySite.UP;
}

/// <summary>
/// 边线的位置
/// </summary>
public enum BoundarySite
{
    UP,
    RIGHT,
    DOWN,
    LEFT,
}

/// <summary>
/// 相对于边界的位置
/// </summary>
public enum LocationRelativeToWindow
{
    Up,
    Right,
    Down,
    Left,
    RightUp,
    RightDown,
    LeftUp,
    LeftDown,
    Inside,
    Outside,
    Unknown,
}

/// <summary>
/// 操作类型
/// </summary>
public enum OperationType
{
    Q,
    W,
    E,
    R,
    T,
}
