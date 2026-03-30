using System.Collections.Generic;
using UnityEngine;

public class PagesManager : MonoBehaviour, IService
{
    private UIModel uiModel;
    private Presenter presenter;
    private int currentPage;
    public List<GameObject> pageObjects;


    public void Init()
    {
        // Получаем нужные нам сервисы
        presenter = ServiceLocator.Instance.Get<Presenter>();
        uiModel = ServiceLocator.Instance.Get<UIModel>();
        
        
        pageObjects[uiModel.page].SetActive(true);
        currentPage = uiModel.page;
    }

    public void ShowPage(int page)
    {
        if(uiModel.stage == 0 && !uiModel.inputBlocked)
        {
            // Прячем предыдущую страницу
            pageObjects[currentPage].GetComponent<PageScript>().Hide();

            // Открываем новую страницу и запоминаем её индекс
            pageObjects[page].SetActive(true);
            currentPage = page;

            // Отправляем выбранную страницу
            presenter.OnPageSelected(page);
        }
        
        
    }

    public void ColorSelected(int color)
    {
        if (!uiModel.inputBlocked)
        {
            presenter.OnColorSelected(color);
        }
        
    }

}
