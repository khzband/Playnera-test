using UnityEngine;
using UnityEngine.EventSystems;

public class CreamClick : MonoBehaviour, IPointerClickHandler
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
        Debug.Log("Cream selected");

        if (!uiModel.inputBlocked && uiModel.stage == 0)
        {
            presenter.OnCreamSelected();
            Debug.Log("Cream selected");
        }
    }
}
