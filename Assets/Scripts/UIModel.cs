using UnityEngine;

public class UIModel : IService
{
    EventBus eventBus;
    
    public int page;
    public int mode;
    public int color;
    public int instrument; // Номер интсрумента в соостветсвующем списке InstrumentController
    public int stage; // 0 - начальный режим, 1 - фаза GetReady

    public bool inputBlocked;
    public bool instrumentBlocked;
    public bool quickColorReset;

    public void Init()
    {
        eventBus = ServiceLocator.Instance.Get<EventBus>();

        
        page = 1; // 0 - powder, 1 - blush, 2 - lipstick, 3 - eyeshadows
        mode = 1; // 0 - powder, 1 - blush, 2 - lipstick, 3 - eyeshadows, 4 - cream
        color = 0;
        stage = 0;

        inputBlocked = false;
        instrumentBlocked = true;
        quickColorReset = false;
    }

    public void SetPage(int pageValue)
    {
        page = pageValue;
    }

    public void SetMode(int modeValue)
    {
        mode = modeValue;
    }

    public void SetColor(int newColor)
    {
        color = newColor;
    }

    public void SetStage(int newStage)
    {
        stage = newStage;
    }

    public void SetInstrument(int newInstrument) 
    { 
        instrument = newInstrument;
        inputBlocked = true;
        eventBus.instrumentSelected?.Invoke();
    }

    public void BlockInstrumentInput()
    {
        instrumentBlocked = true;
        Debug.Log("Instrument blocked");
    }

    public void UnblockInstrumentInput()
    {
        instrumentBlocked = false;
        Debug.Log("Instrument unblocked");
    }

    public void BlockInput()
    {
        inputBlocked = true;
        Debug.Log("Input blocked");
    }

    public void UnblockInput() 
    {
        inputBlocked = false;
        Debug.Log("Input unblocked");
    }

}
