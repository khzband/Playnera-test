using System;
using UnityEngine;

public class EventBus : IService
{
    //Application


    // UI
    public Action instrumentSelected;
    public Action instrumentReady;
    public Action blushInstrumentUsed;

    // Model
    public Action<int> blushColorSet;
    public Action blushAnimationStarted;

}
