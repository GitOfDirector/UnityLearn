using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ResizePanel : MonoBehaviour, IPointerDownHandler, IDragHandler
{

    public LocationRelativeToWindow location = LocationRelativeToWindow.Unknown;

    public Vector2 minSize = new Vector2(100, 100);
    public Vector2 maxSize = new Vector2(400, 400);

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
        //Vector2 oldPivot = panelRectTransform.pivot;
        //Vector2 pivotOffset = Vector2.up - oldPivot;

        //float newPosX = panelRectTransform.anchoredPosition.x + pivotOffset.x * panelRectTransform.rect.width;
        //float newPosY = panelRectTransform.anchoredPosition.y + pivotOffset.y * panelRectTransform.rect.height;

        //panelRectTransform.anchoredPosition = new Vector2(newPosX, newPosY);
        //panelRectTransform.pivot = Vector2.up;

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
            case LocationRelativeToWindow.Down:
                sizeDelta = originalSizeDelta + new Vector2(0, -offsetToOriginal.y);
                break;
            case LocationRelativeToWindow.Right:
            case LocationRelativeToWindow.Left:
                sizeDelta = originalSizeDelta + new Vector2(offsetToOriginal.x, 0);
                break;
            case LocationRelativeToWindow.RightUp:
            case LocationRelativeToWindow.RightDown:
            case LocationRelativeToWindow.LeftUp:
            case LocationRelativeToWindow.LeftDown:
                sizeDelta = originalSizeDelta + new Vector2(offsetToOriginal.x, -offsetToOriginal.y);
                break;
            case LocationRelativeToWindow.Inside:
            case LocationRelativeToWindow.Outside:
            case LocationRelativeToWindow.Unknown:
            default:
                break;
        }

        sizeDelta = new Vector2(
            Mathf.Clamp(sizeDelta.x, minSize.x, maxSize.x),
            Mathf.Clamp(sizeDelta.y, minSize.y, maxSize.y)
        );

        panelRectTransform.sizeDelta = sizeDelta;
    }
}