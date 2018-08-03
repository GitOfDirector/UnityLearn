using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ResizePanel : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public Transform pivotTran;
    public Vector2 pivotPlace = Vector2.zero;
    public LocationRelativeToWindow location = LocationRelativeToWindow.Unknown;

    public Vector2 minSize = new Vector2(100, 100);
    public Vector2 maxSize = new Vector2(400, 400);
    public bool isSubtractX = false;
    public bool isSubtractY = false;
    public bool isClampWidth = true;
    public bool isClampHeight = true;

    private RectTransform panelRectTransform;
    private Vector2 originalLocalPointerPosition;
    private Vector2 originalSizeDelta;

    void Awake()
    {
        panelRectTransform = transform.parent.parent.GetComponent<RectTransform>();
    }

    void Start() { }

    public void OnPointerDown(PointerEventData data)
    {
        ResolutionTool.Instance.KeepUIRelativePlace(panelRectTransform, pivotPlace);

        originalSizeDelta = panelRectTransform.sizeDelta;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(panelRectTransform, data.position, data.pressEventCamera, out originalLocalPointerPosition);
    }

    public void OnDrag(PointerEventData data)
    {
        if (panelRectTransform == null)
            return;

        Vector2 localPointerPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(panelRectTransform, data.position, data.pressEventCamera, out localPointerPosition);
        Vector3 offsetToOriginal = localPointerPosition - originalLocalPointerPosition;

        Vector2 sizeDelta = Vector2.zero;

        switch (location)
        {
            case LocationRelativeToWindow.Up:
                sizeDelta = originalSizeDelta + new Vector2(0, offsetToOriginal.y);
                break;
            case LocationRelativeToWindow.Down:
                sizeDelta = originalSizeDelta + new Vector2(0, -offsetToOriginal.y);
                break;
            case LocationRelativeToWindow.Right:
                sizeDelta = originalSizeDelta + new Vector2(offsetToOriginal.x, 0);
                break;
            case LocationRelativeToWindow.Left:
                sizeDelta = originalSizeDelta + new Vector2(-offsetToOriginal.x, 0);
                break;
            case LocationRelativeToWindow.RightUp:
                sizeDelta = originalSizeDelta + new Vector2(offsetToOriginal.x, offsetToOriginal.y);
                break;
            case LocationRelativeToWindow.RightDown:
                sizeDelta = originalSizeDelta + new Vector2(offsetToOriginal.x, -offsetToOriginal.y);
                break;
            case LocationRelativeToWindow.LeftUp:
                sizeDelta = originalSizeDelta + new Vector2(-offsetToOriginal.x, offsetToOriginal.y);
                break;
            case LocationRelativeToWindow.LeftDown:
                sizeDelta = originalSizeDelta + new Vector2(-offsetToOriginal.x, -offsetToOriginal.y);
                break;
            case LocationRelativeToWindow.Inside:
            case LocationRelativeToWindow.Outside:
            case LocationRelativeToWindow.Unknown:
            default:
                break;
        }

        Vector2 pivotScreenPos = RectTransformUtility.WorldToScreenPoint(null, pivotTran.position);

        if (isClampWidth)
        {
            maxSize.x = isSubtractX ? Screen.width - pivotScreenPos.x : pivotScreenPos.x;
        }
        else
        {
            maxSize.x = Screen.width;
        }

        if (isClampHeight)
        {
            maxSize.y = isSubtractY ? Screen.height - pivotScreenPos.y : pivotScreenPos.y;
        }
        else
        {
            maxSize.y = Screen.height;
        }

        sizeDelta = new Vector2(
            Mathf.Clamp(sizeDelta.x, minSize.x, maxSize.x),
            Mathf.Clamp(sizeDelta.y, minSize.y, maxSize.y)
        );

        panelRectTransform.sizeDelta = sizeDelta;
    }
}