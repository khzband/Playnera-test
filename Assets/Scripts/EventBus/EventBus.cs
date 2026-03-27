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
    //public Action blushInstrumentUsed;
    public Action blushAnimationStarted;

    // Lipstick
    public Action lipstickColorSet;
    public Action lipstickAnimationStarted;

}
