using System.Collections.Generic;
using UnityEngine;

public class Model : IService
{
    EventBus eventBus;
    
    public bool acne;
    public int blushColor;
    public int lipstickColor;
    public int eyeshadowsColor;

    Dictionary<string, int> modelState = new Dictionary<string, int>()
    {
        {"powder", 0 },
        {"blush", 0 },
        {"lipstick", 0 },
        {"eyeshadows", 0 },
        {"acne", 1 }
    };

    public void Init()
    {
        eventBus = ServiceLocator.Instance.Get<EventBus>();
        
        acne = true;
        blushColor = 0;
        lipstickColor = 0;
        eyeshadowsColor = 0;
    }

    public void SetBlushColor(int newColor)
    {
        blushColor = newColor;
        eventBus.blushColorSet?.Invoke(newColor);
        Debug.Log($"New blush color = {newColor}");
    }



}
