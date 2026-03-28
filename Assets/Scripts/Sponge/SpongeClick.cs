using UnityEngine;
using UnityEngine.EventSystems;

public class SpongeClick : MonoBehaviour, IPointerClickHandler
{
    private UIModel uiModel;
    private Presenter presenter;


    void Start()
    {
        uiModel = ServiceLocator.Instance.Get<UIModel>();
        presenter = ServiceLocator.Instance.Get<Presenter>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!uiModel.inputBlocked && uiModel.stage == 0)
        {
            presenter.OnSpongeClicked();
            Debug.Log("Sponge clicked");
        }
    }
}
