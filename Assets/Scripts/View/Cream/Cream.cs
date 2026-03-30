using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cream : MonoBehaviour, IInstrument
{
    EventBus eventBus;
    UIModel uiModel;
    Presenter presenter;

    public RectTransform readyZone;
    public RectTransform creamZone;

    Vector3 startZone;

    public Image shelfImage;
    private Image creamImage;

    private RectTransform rectTransform; // RectTransform кисти

    private float speed = 900f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eventBus = ServiceLocator.Instance.Get<EventBus>();
        uiModel = ServiceLocator.Instance.Get<UIModel>();
        presenter = ServiceLocator.Instance.Get<Presenter>();

        rectTransform = GetComponent<RectTransform>();
        creamImage = GetComponent<Image>();

        startZone = shelfImage.rectTransform.position; // Запоминаем стартовую позицию
        rectTransform.position = startZone;
    }

    // Вызов начальной анимации, поступает с InstrumentController
    public void GetReady()
    {
        StartCoroutine(GetReadySequence());
    }

    /// <summary>
    /// Анимация перемещения в зону готовности
    /// </summary>
    IEnumerator GetReadySequence()
    {
        creamImage.enabled = true;
        shelfImage.enabled = false;

        // Перемещение в зону готовности
        yield return StartCoroutine(Utils.MoveRoutine(rectTransform, readyZone.position, speed));

        presenter.OnInstrumentReady();
    }

    // Вызов анимации нанесения, поступает с InstrumentController
    public void ApplyInstrument()
    {
        StartCoroutine(ApplyCreamSequence());
    }

    /// <summary>
    /// Анимация использования на кукле и возврат в начальное местоположение
    /// </summary>
    IEnumerator ApplyCreamSequence()
    {
        
        // Перемещаем в зону нанесения
        yield return StartCoroutine(Utils.MoveRoutine(rectTransform, creamZone.position, speed));

        eventBus.acneAnimationStarted?.Invoke();
        yield return new WaitForSeconds(0.5f);

        // Возвращаем крем на место
        yield return StartCoroutine(Utils.MoveRoutine(rectTransform, startZone, speed));

        shelfImage.enabled = true;
        creamImage.enabled = false;
        presenter.OnAcneRemoved();
        presenter.OnCycleCompleted();
    }

}
