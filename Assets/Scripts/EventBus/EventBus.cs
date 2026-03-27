using System;
using UnityEngine;

public class EventBus : IService
{
    //Application


    // Instruments
    public Action instrumentSelected;

    // Blush
    public Action blushColorSet;
    public Action<int> brushTouchedColor;
    public Action blushAnimationStarted;

    // Lipstick
    public Action lipstickColorSet;
    public Action lipstickAnimationStarted;

    // Eyebrush
    public Action eyeshadowsColorSet;
    public Action<int> eyebrushTouchedColor;
    public Action eyeshadowsAnimationStarted;

}
