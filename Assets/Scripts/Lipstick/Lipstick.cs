using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lipstick : MonoBehaviour, IInstrument
{
    EventBus eventBus;
    Presenter presenter;

    Vector3 startZone;
    public RectTransform readyZone;
    public RectTransform lipsZone;

    public int id;

    private float speed = 700f;
    private RectTransform rectTransform; // RectTransform помады

    void Start()
    {
        eventBus = ServiceLocator.Instance.Get<EventBus>();
        presenter = ServiceLocator.Instance.Get<Presenter>();

        rectTransform = GetComponent<RectTransform>();

        startZone = rectTransform.position; // Запоминаем стартовую позицию
    }

    // Вызов начальной анимации, поступает с InstrumentController
    public void GetReady()
    {
        Debug.Log($"Lipstick {id} started");
        StartCoroutine(GetReadySequence());
    }

    IEnumerator GetReadySequence()
    {
        // Перемещение в зону готовности
        yield return StartCoroutine(Utils.MoveRoutine(rectTransform, readyZone.position, speed));


        Debug.Log("Sequence completed");


        //eventBus.instrumentReady?.Invoke();
        presenter.OnInstrumentReady();
    }

    // Вызов анимации нанесения, поступает с InstrumentController
    public void ApplyInstrument()
    {
        //StartCoroutine(ApplyBlushSequence());
    }
}
