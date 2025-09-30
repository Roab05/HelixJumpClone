using UnityEngine;
using UnityEngine.EventSystems;

public class SensitivitySlider : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    public static SensitivitySlider Instance { get; private set; }
    private bool isHandleHeld = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    public bool IsHandleHeld()
    {
        return isHandleHeld;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isHandleHeld = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isHandleHeld = false;
    }
}
