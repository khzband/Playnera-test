using UnityEngine;
using UnityEngine.EventSystems;

public class LipstickClick : MonoBehaviour, IPointerClickHandler
{
    private UIModel uiModel;
    private Presenter presenter;

    public GameObject lipstickObj;
    private Lipstick lipstick;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uiModel = ServiceLocator.Instance.Get<UIModel>();
        presenter = ServiceLocator.Instance.Get<Presenter>();

        lipstick = lipstickObj.GetComponent<Lipstick>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!uiModel.inputBlocked && uiModel.stage == 0)
        {
            int color = lipstick.id;
            presenter.OnColorSelected(color);
            Debug.Log($"Lipstick {lipstick.id} selected");
        }
    }

}
