using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyebrush : MonoBehaviour, IInstrument
{
    EventBus eventBus;
    UIModel uiModel;
    Presenter presenter;

    public List<Transform> eyeshadowsCells; // Список ячеек с румянами
    public RectTransform readyZone;
    public RectTransform eyesZone;

    Vector3 startZone;

    private RectTransform rectTransform; // RectTransform кисти

    private float speed = 700f;


    void Start()
    {
        eventBus = ServiceLocator.Instance.Get<EventBus>();
        uiModel = ServiceLocator.Instance.Get<UIModel>();
        presenter = ServiceLocator.Instance.Get<Presenter>();

        rectTransform = GetComponent<RectTransform>();

        startZone = rectTransform.position; // Запоминаем стартовую позицию
    }

    // Вызов начальной анимации, поступает с InstrumentController
    public void GetReady()
    {
        StartCoroutine(GetReadySequence(uiModel.color));
    }


    /// <summary>
    /// Анимация нанесения цвета на кисть и перемещение в зону готовности
    /// </summary>
    IEnumerator GetReadySequence(int color)
    {
        Vector3 offset = new Vector3(50, 0, 0);
        float moveTime = 0.2f;

        // Перемещаем к цвету
        yield return StartCoroutine(Utils.MoveRoutine(rectTransform, eyeshadowsCells[color - 1].GetComponent<RectTransform>().position, speed));

        // Анимация нанесения цвета на кисть
        eventBus.eyebrushTouchedColor?.Invoke();
        for (int i = 0; i < 2; i++)
        {
            yield return StartCoroutine(Utils.MoveByTimeRoutine(rectTransform, eyeshadowsCells[color - 1].GetComponent<RectTransform>().position + offset, moveTime));
            yield return StartCoroutine(Utils.MoveByTimeRoutine(rectTransform, eyeshadowsCells[color - 1].GetComponent<RectTransform>().position, moveTime));
        }

        yield return StartCoroutine(Utils.MoveByTimeRoutine(rectTransform, eyeshadowsCells[color - 1].GetComponent<RectTransform>().position + offset, moveTime));


        // Перемещение в зону готовности
        yield return StartCoroutine(Utils.MoveRoutine(rectTransform, readyZone.position, speed));


        presenter.OnInstrumentReady();
    }

    // Вызов анимации нанесения, поступает с InstrumentController
    public void ApplyInstrument()
    {
        StartCoroutine(ApplyEyeshadowsSequence());
    }

    /// <summary>
    /// Анимация использования на кукле и возврат в начальное местоположение
    /// </summary>
    IEnumerator ApplyEyeshadowsSequence()
    {
        Vector3 offsetL = new Vector3(-100, 0, 0);
        Vector3 offsetR = new Vector3(100, 0, 0);
        float moveTime = 0.3f;

        // Перемещаем к начальной точке
        yield return StartCoroutine(Utils.MoveRoutine(rectTransform, eyesZone.position + offsetL, speed));


        // Анимация нанесения цвета на лицо
        eventBus.eyeshadowsAnimationStarted?.Invoke();
        for (int i = 0; i < 2; i++)
        {
            yield return StartCoroutine(Utils.MoveByTimeRoutine(rectTransform, eyesZone.position + offsetR, moveTime));
            yield return StartCoroutine(Utils.MoveByTimeRoutine(rectTransform, eyesZone.position + offsetL, moveTime));
        }
        yield return StartCoroutine(Utils.MoveByTimeRoutine(rectTransform, eyesZone.position + offsetR, moveTime));

        // Возвращаем кисть на место
        yield return StartCoroutine(Utils.MoveRoutine(rectTransform, startZone, speed));

        presenter.OnCycleCompleted();
    }

}
