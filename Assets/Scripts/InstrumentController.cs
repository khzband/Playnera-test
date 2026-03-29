using System.Collections.Generic;
using UnityEngine;

public class InstrumentController : MonoBehaviour, IService
{
    EventBus eventBus;
    UIModel uiModel;

    //public GameObject powderInstrument;
    public GameObject blushInstrument;
    public List<GameObject> lipstickInstruments;
    public GameObject eyeshadowsInstrument;
    public GameObject creamInstrument;

    //List <List<GameObject>> instrList;

    /*
    private static readonly Dictionary<int, List<IInstrument>> instrDirectory = new Dictionary<int, List<IInstrument>>()
    {
        {0, powderInstruments },
        {1, blushInstruments },
        {2, lipstickInstruments },
        {3, eyeshadowsInstruments }
    };
    */

    private GameObject instrumentObj;
    private IInstrument currentInstrument;

    public void Init()
    {
        eventBus = ServiceLocator.Instance.Get<EventBus>();
        uiModel = ServiceLocator.Instance.Get<UIModel>();

        eventBus.instrumentSelected += OnInstrumentSelected;
        eventBus.blushColorSet += OnInstrumentUsed;
        eventBus.lipstickColorSet += OnInstrumentUsed;
        eventBus.eyeshadowsColorSet += OnInstrumentUsed;
        eventBus.acneSet += OnInstrumentUsed;
    }

    

    private void OnInstrumentSelected()
    {
        switch (uiModel.mode)
        {
            case 0:
                 break;
                
                
            case 1:
                instrumentObj = blushInstrument;
                eventBus.blushColorSelected?.Invoke();
                if (uiModel.quickColorReset)
                {
                    eventBus.blushColorReset?.Invoke();
                }
                break;
                
                
            case 2: 
                instrumentObj = lipstickInstruments[uiModel.instrument];
                break;
                
                
            case 3:
                instrumentObj = eyeshadowsInstrument;
                eventBus.eyeshadowsColorSelected?.Invoke();
                if (uiModel.quickColorReset)
                {
                    eventBus.eyeshadowsColorReset?.Invoke();
                }
                break;
                
            case 4:
                instrumentObj = creamInstrument;
                break;


        }

        currentInstrument = instrumentObj.GetComponent<IInstrument>();
        currentInstrument.GetReady();
    }

    private void OnInstrumentUsed()
    {
        currentInstrument.ApplyInstrument();
    }

    private void OnDestroy()
    {
        eventBus.instrumentSelected -= OnInstrumentSelected;
        eventBus.blushColorSet -= OnInstrumentUsed;
        eventBus.lipstickColorSet -= OnInstrumentUsed;
        eventBus.eyeshadowsColorSet -= OnInstrumentUsed;
        eventBus.acneSet -= OnInstrumentUsed;
    }
}
