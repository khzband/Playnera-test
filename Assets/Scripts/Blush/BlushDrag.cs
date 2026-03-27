using UnityEngine;
using UnityEngine.EventSystems;

public class BlushDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private UIModel uiModel;
    private EventBus eventBus;
    private Presenter presenter;

    public BoxCollider2D brushCollider; // Коллайдер кисти
    public BoxCollider2D faceZoneCollider; // Коллайдер области лица

    private RectTransform rectTransform;

    void Start()
    {
        uiModel = ServiceLocator.Instance.Get<UIModel>();
        eventBus = ServiceLocator.Instance.Get<EventBus>();
        presenter = ServiceLocator.Instance.Get<Presenter>();

        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("Brush drag begins");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (uiModel.instrumentBlocked) return;
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        if (brushCollider.IsTouching(faceZoneCollider))
        {
            //eventBus.blushInstrumentUsed?.Invoke();
            presenter.OnBlushInstrumentUsed();
        }
        
    }
}
