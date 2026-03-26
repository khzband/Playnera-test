using UnityEngine;

public class Presenter : IService
{
    EventBus eventBus;
    UIModel uiModel;
    Model model;

    public void Init()
    {
        eventBus = ServiceLocator.Instance.Get<EventBus>();
        uiModel = ServiceLocator.Instance.Get<UIModel>();
        model = ServiceLocator.Instance.Get<Model>();

        eventBus.instrumentReady += OnInstrumentReady;
        eventBus.blushInstrumentUsed += OnBlushInstrumentUsed;
    }

    // В UI выбран режим
    public void OnPageSelected(int page)
    {
        uiModel.SetPage(page);
        uiModel.SetMode(page);

        Debug.Log($"Page {page}, Mode {page}");
    }

    public void OnColorSelected(int newColor)
    {
        // Блокируем возможность выбора цвета
        uiModel.BlockInput();
        
        // Устанавливаем номер выбранного цвета
        uiModel.SetColor(newColor);
        Debug.Log($"Color selected {newColor}");
        
        // Выбираем номер инструмента, который будет наносить цвет
        if (uiModel.page == 2) 
        {
            uiModel.SetInstrument(newColor);
        }
        else
        {
            uiModel.SetInstrument(0);
        }
    }

    private void OnInstrumentReady()
    {
        // Разрешаем выбрать новый цвет или двигать инструмент
        uiModel.UnblockInput();
        uiModel.UnblockInstrumentInput();
    }

    private void OnBlushInstrumentUsed()
    {
        model.SetBlushColor(uiModel.color);
        uiModel.BlockInstrumentInput();
    }

    /*
    private void OnInstrumentUsed()
    {
        switch (uiModel.mode)
        {
            case 0:
                break;

            case 1:
                model.SetBlushColor(uiModel.color);
                break;

            case 2:
                //model.SetLipstickColor(uiModel.color);
                break;

            case 3:
                //model.SetEyeshadowsColor(uiModel.color);
                break;

            case 4:
                //model.SetAcne(uiModel.color);
                break;

        }
    }
    */

    public void OnDispose()
    {
        eventBus.instrumentReady -= OnInstrumentReady;
    }

}
