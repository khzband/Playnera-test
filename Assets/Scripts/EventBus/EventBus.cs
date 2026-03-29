using System;
using UnityEngine;

public class EventBus : IService
{
    
    // Instruments
    public Action instrumentSelected;

    // Blush
    public Action blushColorSelected; // Сигнал для загрузки окрашенного кончика кисти
    public Action blushColorReset; // Быстрое освобождение от предыдущего цвета
    public Action blushColorSet;
    public Action brushTouchedColor;
    public Action blushAnimationStarted;

    // Lipstick
    public Action lipstickColorSet;
    public Action lipstickAnimationStarted;

    // Eyebrush
    public Action eyeshadowsColorSelected; // Сигнал для загрузки окрашенного кончика кисти
    public Action eyeshadowsColorReset; // Быстрое освобождение от предыдущего цвета
    public Action eyeshadowsColorSet;
    public Action eyebrushTouchedColor;
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
