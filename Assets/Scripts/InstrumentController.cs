using System.Collections.Generic;
using UnityEngine;

public class InstrumentController : MonoBehaviour, IService
{
    EventBus eventBus;
    UIModel uiModel;

    public List<GameObject> powderInstruments;
    public List<GameObject> blushInstruments;
    public List<GameObject> lipstickInstruments;
    public List<GameObject> eyeshadowsInstruments;

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

    }

    

    private void OnInstrumentSelected()
    {
        switch (uiModel.page)
        {
            case 0:
                 break;
                
                
            case 1:
                instrumentObj = blushInstruments[uiModel.instrument];
                break;
                
                
            case 2: 
                instrumentObj = lipstickInstruments[uiModel.instrument];
                break;
                
                
            case 3:
                instrumentObj = eyeshadowsInstruments[uiModel.instrument];
                break;
                
        }

        currentInstrument = instrumentObj.GetComponent<IInstrument>();
        currentInstrument.GetReady(uiModel.color);
    }

    private void OnDisable()
    {
        eventBus.instrumentSelected -= OnInstrumentSelected;
    }
}
