using Unity.VisualScripting;
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
        //eventBus.blushInstrumentUsed += OnBlushInstrumentUsed;
        //eventBus.lipstickInstrumentUsed += OnLipstickInstrumentUsed;
    }

    // В UI выбрана страница
    public void OnPageSelected(int page)
    {
        uiModel.SetPage(page);
        //uiModel.SetMode(page);

        Debug.Log($"Page {page}");
    }

    // В UI выбран цвет
    public void OnColorSelected(int newColor)
    {
        // Если выбран крем, то в фазе GetReady нельзя выбрать цвет в книге
        if (uiModel.stage == 1 && uiModel.mode == 4) return;

        // Проверка на смену цвета в фазе GetReady
        if(uiModel.stage == 1)
        {
            uiModel.quickColorReset = true;
        }

        // Устанавливаем режим
        uiModel.SetMode(uiModel.page);

        // Устанавливаем номер выбранного цвета
        uiModel.SetColor(newColor);
        Debug.Log($"Color selected {newColor}");
        
        // Выбираем номер инструмента, который будет наносить цвет
        if (uiModel.mode == 2) 
        {
            uiModel.SetInstrument(newColor-1);
        }
        else
        {
            uiModel.SetInstrument(0);
        }


    }

    public void OnCreamSelected()
    {
        // Устанавливаем режим крема
        uiModel.SetMode(4);

        // У этого инструмента только один цвет
        uiModel.SetColor(0);

        // и только один инструмент
        uiModel.SetInstrument(0);
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

    public void OnBlushInstrumentUsed()
    {
        model.SetBlushColor(uiModel.color);
        uiModel.BlockInstrumentInput();
    }

    public void OnLipstickInstrumentUsed()
    {
        model.SetLipstickColor(uiModel.color);
        uiModel.BlockInstrumentInput();
    }

    public void OnEyeshadowsInstrumentUsed()
    {
        model.SetEyeshadowsColor(uiModel.color);
        uiModel.BlockInstrumentInput();
    }

    public void OnCreamInstrumentUsed()
    {
        model.SetAcne(false);
        uiModel.BlockInstrumentInput();
    }

    public void OnAcneRemoved()
    {
        model.acneAlreadyRemoved = true;
    }

    public void OnSpongeClicked()
    {
        if(model.blushColor > 0)
        {
            model.RemoveBlush();
        }

        if (model.lipstickColor > 0)
        {
            model.RemoveLipstick();
        }

        if (model.eyeshadowsColor > 0)
        {
            model.RemoveEyeshadows();
        }

        if (!model.acne)
        {
            model.RemoveCream();
            model.acneAlreadyRemoved = false;
        }
    }


    public void OnCycleCompleted()
    {
        uiModel.SetStage(0);
        uiModel.quickColorReset = false;
        Debug.Log("Initial phase");
    }

    public void OnDestroy()
    {
        //eventBus.instrumentReady -= OnInstrumentReady;
    }

}
