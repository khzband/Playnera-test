using UnityEngine;
using UnityEngine.EventSystems;

public class CreamDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private UIModel uiModel;
    private Presenter presenter;

    private BoxCollider2D creamCollider; // Собственный коллайдер кисти
    public BoxCollider2D faceZoneCollider; // Коллайдер области лица

    private RectTransform rectTransform;

    void Start()
    {
        uiModel = ServiceLocator.Instance.Get<UIModel>();
        presenter = ServiceLocator.Instance.Get<Presenter>();

        rectTransform = GetComponent<RectTransform>();
        creamCollider = GetComponent<BoxCollider2D>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("Trying to drag cream");
    }

    public void OnDrag(PointerEventData eventData)
    {
        
        if (uiModel.instrumentBlocked && uiModel.mode != 4) return;
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        if (creamCollider.IsTouching(faceZoneCollider))
        {
            presenter.OnCreamInstrumentUsed();
        }

    }
}
