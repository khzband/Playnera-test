using System;
using UnityEngine;

public class EventBus : IService
{
    //Application


    // Instruments
    public Action instrumentSelected;
    public Action instrumentReady;

    // Blush
    public Action<int> blushColorSet;
    public Action<int> brushTouchedColor;
    public Action blushInstrumentUsed;
    public Action blushAnimationStarted;

}
