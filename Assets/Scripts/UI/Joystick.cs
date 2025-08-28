using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private RectTransform _handle;
    private RectTransform background;

    private Vector2 input = Vector2.zero;
    private float radius;

    public float Horizontal => input.x;
    public float Vertical => input.y;
    public Vector2 Direction => new Vector2(Horizontal, Vertical);

    public void Awake()
    {
        background = GetComponent<RectTransform>();
        radius = background.sizeDelta.x / 2f;
    }

    private void ResetInput()
    {
        input = Vector2.zero;
        _handle.anchoredPosition = Vector2.zero;
    }

    private void HandlePause(bool isPaused)
    {
        if (isPaused)
            ResetInput();
    }

    private void OnEnable()
    {
        UIManager.OnPauseStateChanged += HandlePause;
    }

    private void OnDisable()
    {
        ResetInput();
        UIManager.OnPauseStateChanged -= HandlePause;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(background, eventData.position, eventData.pressEventCamera, out localPoint);

        input = Vector2.ClampMagnitude(localPoint / radius, 1f);

        _handle.anchoredPosition = input * radius;
    }

    public void OnPointerDown(PointerEventData eventData) => OnDrag(eventData);

    public void OnPointerUp(PointerEventData eventData) => ResetInput();
}
