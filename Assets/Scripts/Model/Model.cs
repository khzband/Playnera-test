using System.Collections.Generic;
using UnityEngine;

public class Model : IService
{
    EventBus eventBus;

    private bool _acne;
    private bool _acneAlreadyRemoved;
    private int _blushColor;
    private int _lipstickColor;
    private int _eyeshadowsColor;

    public bool acne => _acne;
    public bool acneAlreadyRemoved => _acneAlreadyRemoved;
    public int blushColor => _blushColor;
    public int lipstickColor => _lipstickColor;
    public int eyeshadowsColor => _eyeshadowsColor;


    public void Init()
    {
        eventBus = ServiceLocator.Instance.Get<EventBus>();
        
        _acne = true;
        _acneAlreadyRemoved = false;
        _blushColor = 0;
        _lipstickColor = 0;
        _eyeshadowsColor = 0;
    }

    public void SetBlushColor(int newColor)
    {
        if (newColor >= 0 && newColor < 10) 
        {
            _blushColor = newColor;
        }

        eventBus.blushColorSet?.Invoke(); // BlushController начинает загружать спрайты, InstrumentController запускает анимацию нанесения
        Debug.Log($"New blush color = {newColor}");
    }

    public void SetLipstickColor(int newColor)
    {
        if (newColor >= 0 && newColor < 7)
        {
            _lipstickColor = newColor;
        }

        eventBus.lipstickColorSet?.Invoke(); // LipstickController начинает загружать спрайты, InstrumentController запускает анимацию нанесения
        Debug.Log($"New lipstick color = {newColor}");
    }

    public void SetEyeshadowsColor(int newColor)
    {
        if (newColor >= 0 && newColor < 10)
        {
            _eyeshadowsColor = newColor;
        }

        eventBus.eyeshadowsColorSet?.Invoke(); // EyeshadowsController начинает загружать спрайты, InstrumentController запускает анимацию нанесения
        Debug.Log($"New eyeshadows color = {newColor}");
    }

    public void SetAcne(bool value)
    {
        _acne = value;
        eventBus.acneSet?.Invoke();// InstrumentController запускает анимацию нанесения
    }

    public void SetAcneAlreadyRemoved(bool value)
    {
        _acneAlreadyRemoved = value;
    }

    public void RemoveBlush()
    {
        _blushColor = 0;
        eventBus.blushRemoved?.Invoke();
    }

    public void RemoveLipstick()
    {
        _lipstickColor = 0;
        eventBus.lipstickRemoved?.Invoke();
    }

    public void RemoveEyeshadows()
    {
        _eyeshadowsColor = 0;
        eventBus.eyeshadowsRemoved?.Invoke();
    }

    public void RemoveCream()
    {
        _acne = true;
        eventBus.creamRemoved?.Invoke();
    }

}
