using System;
using UnityEngine;

public class EventBus : IService
{
    
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

    // Cream
    public Action acneSet;
    public Action acneAnimationStarted;

    // Remove makeup
    public Action blushRemoved;
    public Action lipstickRemoved;
    public Action eyeshadowsRemoved;
    public Action creamRemoved;



}
