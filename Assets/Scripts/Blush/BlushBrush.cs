using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlushBrush : MonoBehaviour, IInstrument
{
    EventBus eventBus;
    UIModel uiModel;
    Presenter presenter;

    public List<Transform> blushCells; // Список ячеек с румянами
    //public RectTransform startZone;
    public RectTransform readyZone;
    public RectTransform blushZone;

    Vector3 startZone;

    private RectTransform rectTransform; // RectTransform кисти

    private float speed = 900f;
    private Vector3 rotateAngles = new Vector3(0, 0, 10);
    private float rotateDuration = 0.2f;

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

        // Поворачиваем
        yield return StartCoroutine(Utils.RotateOverTimeRoutine(rectTransform, rotateAngles, rotateDuration));

        // Перемещаем к цвету
        yield return StartCoroutine(Utils.MoveRoutine(rectTransform, blushCells[color - 1].GetComponent<RectTransform>().position, speed));

        // Анимация нанесения цвета на кисть
        eventBus.brushTouchedColor?.Invoke();
        for (int i = 0; i < 2; i++)
        {
            yield return StartCoroutine(Utils.MoveByTimeRoutine(rectTransform, blushCells[color - 1].GetComponent<RectTransform>().position + offset, moveTime));
            yield return StartCoroutine(Utils.MoveByTimeRoutine(rectTransform, blushCells[color - 1].GetComponent<RectTransform>().position, moveTime));
        }

        yield return StartCoroutine(Utils.MoveByTimeRoutine(rectTransform, blushCells[color - 1].GetComponent<RectTransform>().position + offset, moveTime));

        
        // Перемещение в зону готовности
        yield return StartCoroutine(Utils.MoveRoutine(rectTransform, readyZone.position, speed));
        

        presenter.OnInstrumentReady();
    }

    // Вызов анимации нанесения, поступает с InstrumentController
    public void ApplyInstrument()
    {
        StartCoroutine(ApplyBlushSequence());
    }

    /// <summary>
    /// Анимация использования на кукле и возврат в начальное местоположение
    /// </summary>
    IEnumerator ApplyBlushSequence()
    {
        Vector3 offsetL = new Vector3(-100, 0, 0);
        Vector3 offsetR = new Vector3(100, 0, 0);
        float moveTime = 0.3f;

        
        // Перемещаем к начальной точке
        yield return StartCoroutine(Utils.MoveRoutine(rectTransform, blushZone.position + offsetL, speed));

        
        // Анимация нанесения цвета на лицо
        eventBus.blushAnimationStarted?.Invoke();
        for (int i = 0; i < 2; i++)
        {
            yield return StartCoroutine(Utils.MoveByTimeRoutine(rectTransform, blushZone.position + offsetR, moveTime));
            yield return StartCoroutine(Utils.MoveByTimeRoutine(rectTransform, blushZone.position + offsetL, moveTime));
        }
        yield return StartCoroutine(Utils.MoveByTimeRoutine(rectTransform, blushZone.position + offsetR, moveTime));

        // Возвращаем кисть на место
        yield return StartCoroutine(Utils.MoveRoutine(rectTransform, startZone, speed));

        // Поворачиваем в начальное положение
        yield return StartCoroutine(Utils.RotateOverTimeRoutine(rectTransform, Vector3.zero, rotateDuration));

        presenter.OnCycleCompleted();
    }

    
}
