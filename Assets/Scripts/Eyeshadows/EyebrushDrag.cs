using UnityEngine;
using UnityEngine.EventSystems;

public class EyebrushDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private UIModel uiModel;
    private Presenter presenter;

    private BoxCollider2D eyebrushCollider; // Собственный коллайдер кисти
    public BoxCollider2D faceZoneCollider; // Коллайдер области лица

    private RectTransform rectTransform;

    void Start()
    {
        uiModel = ServiceLocator.Instance.Get<UIModel>();
        presenter = ServiceLocator.Instance.Get<Presenter>();

        rectTransform = GetComponent<RectTransform>();
        eyebrushCollider = GetComponent<BoxCollider2D>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("Eyebrush drag begins");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (uiModel.instrumentBlocked) return;
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        if (eyebrushCollider.IsTouching(faceZoneCollider))
        {
            presenter.OnEyeshadowsInstrumentUsed();
            Debug.Log("Contact!");
        }

    }
}
