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

        //eventBus.instrumentReady += OnInstrumentReady;
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
        // Блокируем возможность выбора другого цвета
        uiModel.BlockInput();
        
        // Устанавливаем номер выбранного цвета
        uiModel.SetColor(newColor);
        Debug.Log($"Color selected {newColor}");
        
        // Выбираем номер инструмента, который будет наносить цвет
        if (uiModel.page == 2) 
        {
            uiModel.SetInstrument(newColor-1);
        }
        else
        {
            uiModel.SetInstrument(0);
        }

        // TODO В случае с помадой нужно предусмотреть вариант смены цвета после GetReady

    }

    public void OnInstrumentReady()
    {
        // Разрешаем выбрать новый цвет или двигать инструмент
        uiModel.UnblockInput();
        uiModel.UnblockInstrumentInput();

        // Меняем фазу на GetReady
        uiModel.SetStage(1);
        Debug.Log("Get Ready phase");
    }

    private void OnBlushInstrumentUsed()
    {
        model.SetBlushColor(uiModel.color);
        uiModel.BlockInstrumentInput();
    }

    public void OnCycleCompleted()
    {
        uiModel.SetStage(0);
        Debug.Log("Initial phase");
    }

    public void OnDispose()
    {
        //eventBus.instrumentReady -= OnInstrumentReady;
    }

}
