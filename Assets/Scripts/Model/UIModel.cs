using UnityEngine;

public class UIModel : IService
{
    EventBus eventBus;

    private int _page;
    private int _mode;
    private int _color;
    private int _instrument; // Номер интсрумента в соостветсвующем списке InstrumentController
    private int _stage; // 0 - начальный режим, 1 - фаза GetReady

    private bool _inputBlocked;
    private bool _instrumentBlocked;
    private bool _quickColorReset;


    public int page => _page;
    public int mode => _mode;
    public int color => _color;
    public int instrument => _instrument; // Номер инструмента в соостветсвующем списке InstrumentController
    public int stage => _stage; // 0 - начальный режим, 1 - фаза GetReady

    public bool inputBlocked => _inputBlocked;
    public bool instrumentBlocked => _instrumentBlocked;
    public bool quickColorReset => _quickColorReset;

    public void Init()
    {
        eventBus = ServiceLocator.Instance.Get<EventBus>();

        
        _page = 1; // 0 - powder, 1 - blush, 2 - lipstick, 3 - eyeshadows
        _mode = 1; // 0 - powder, 1 - blush, 2 - lipstick, 3 - eyeshadows, 4 - cream
        _color = 0;
        _stage = 0;

        _inputBlocked = false;
        _instrumentBlocked = true;
        _quickColorReset = false;
    }

    public void SetPage(int pageValue)
    {
        if (pageValue >= 0 && pageValue < 4) 
        {
            _page = pageValue;
        }

        
    }

    public void SetMode(int modeValue)
    {
        if (modeValue >= 0 && modeValue < 5)
        {
            _mode = modeValue;
        }
        
    }

    public void SetColor(int newColor)
    {
        if (newColor >= 0 && newColor < 10)
        {
            _color = newColor;
        }
        
    }

    public void SetQuickColorReset(bool value)
    {
        _quickColorReset = value;
    }

    public void SetStage(int newStage)
    {
        if (newStage >= 0 && newStage < 2)
        {
            _stage = newStage;
        }
        
    }

    public void SetInstrument(int newInstrument) 
    {
        if (newInstrument >= 0 && newInstrument < 6)
        {
            _instrument = newInstrument;
            _inputBlocked = true;
            eventBus.instrumentSelected?.Invoke();
        }
        
    }

    public void BlockInstrumentInput()
    {
        _instrumentBlocked = true;
        Debug.Log("Instrument blocked");
    }

    public void UnblockInstrumentInput()
    {
        _instrumentBlocked = false;
        Debug.Log("Instrument unblocked");
    }

    public void BlockInput()
    {
        _inputBlocked = true;
        Debug.Log("Input blocked");
    }

    public void UnblockInput() 
    {
        _inputBlocked = false;
        Debug.Log("Input unblocked");
    }

}
