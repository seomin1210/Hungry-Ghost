using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField]
    private RectTransform _joystick;
    [SerializeField]
    private RectTransform _handler;
    private RectTransform _rectTransform;

    private float _handlerRange = 150f;

    private Vector2 _inputVector;
    private bool _isInput;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();

        _joystick.gameObject.SetActive(false);
        _handler.gameObject.SetActive(false);
    }

    private void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isInput = true;
        _joystick.gameObject.SetActive(true);
        _handler.gameObject.SetActive(true);

        var inputDir = eventData.position - _rectTransform.anchoredPosition;
        _joystick.anchoredPosition = inputDir;
    }

    public void OnDrag(PointerEventData eventData)
    {
        var inputDir = eventData.position - _joystick.anchoredPosition;
        var clampedDir = inputDir.magnitude < _handlerRange ? inputDir : inputDir.normalized * _handlerRange;

        _handler.anchoredPosition = clampedDir;
        _inputVector = clampedDir.normalized;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isInput = false;
        _handler.anchoredPosition = Vector2.zero;
        _inputVector = Vector2.zero;    

        _joystick.gameObject.SetActive(false);
        _handler.gameObject.SetActive(false);
    }
}
