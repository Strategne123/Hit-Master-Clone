using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class TapHandler : MonoBehaviour,IPointerClickHandler
{
    public static TapAction TapEvent;
    private RectTransform _senderRect;
    public delegate void TapAction(RectTransform rect, Vector3 position);

    private void Awake()
    {
        _senderRect = GetComponent<RectTransform>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        TapEvent?.Invoke(_senderRect,eventData.position);
    }
}
