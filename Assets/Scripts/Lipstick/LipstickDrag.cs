using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;

public class LipstickDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private UIModel uiModel;
    private EventBus eventBus;
    private Presenter presenter;

    private BoxCollider2D lipstickCollider; // Собственный коллайдер
    public BoxCollider2D faceZoneCollider; // Коллайдер области лица

    private RectTransform rectTransform;

    private Lipstick lipstick;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uiModel = ServiceLocator.Instance.Get<UIModel>();
        eventBus = ServiceLocator.Instance.Get<EventBus>();
        presenter = ServiceLocator.Instance.Get<Presenter>();

        lipstick = GetComponent<Lipstick>();
        rectTransform = GetComponent<RectTransform>();
        lipstickCollider = GetComponent<BoxCollider2D>();
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("Lipstick drag begins");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (uiModel.instrumentBlocked) return;
        if (uiModel.instrument != lipstick.id - 1) return; // Если эта помада не выбрана, ее нельзя переместить
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (uiModel.instrument != lipstick.id - 1) return; // Если эта помада не выбрана, ее нельзя переместить
        if (lipstickCollider.IsTouching(faceZoneCollider))
        {
            presenter.OnLipstickInstrumentUsed();
        }

    }
}
