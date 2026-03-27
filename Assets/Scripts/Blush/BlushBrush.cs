using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlushBrush : MonoBehaviour, IInstrument
{
    EventBus eventBus;
    UIModel uiModel;
    Presenter presenter;

    public List<Transform> blushCells; // Список ячеек с румянами
    public RectTransform startZone;
    public RectTransform readyZone;
    public RectTransform blushZone;

    private RectTransform rectTransform; // RectTransform кисти

    //private float speed = 5f;
    private float speed = 700f;

    void Start()
    {
        eventBus = ServiceLocator.Instance.Get<EventBus>();
        uiModel = ServiceLocator.Instance.Get<UIModel>();
        presenter = ServiceLocator.Instance.Get<Presenter>();

        rectTransform = GetComponent<RectTransform>();
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
        yield return StartCoroutine(Utils.MoveRoutine(rectTransform, blushCells[color - 1].GetComponent<RectTransform>().position, speed));

        // Анимация нанесения цвета на кисть
        eventBus.brushTouchedColor?.Invoke(color);
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
        yield return StartCoroutine(Utils.MoveRoutine(rectTransform, startZone.position, speed));

        presenter.OnCycleCompleted();
    }

    /*
    /// <summary>
    /// Рутина перемещения объекта в заданную точку
    /// </summary>
    IEnumerator MoveRoutine(RectTransform objectToMove, Vector3 targetPos, float speed)
    {
        //float cappedDeltaTime = Mathf.Min(Time.deltaTime, 0.03f);
        while (Vector3.Distance(objectToMove.position, targetPos) > 0.01f)
        {
            // Перемещаем объект к цели на фиксированный шаг
            objectToMove.position = Vector3.MoveTowards(objectToMove.position, targetPos, speed * Time.deltaTime);
            yield return null;
        }

        // В конце принудительно ставим в точную позицию цели
        objectToMove.position = targetPos;
    }

    IEnumerator MoveByTimeRoutine(RectTransform objectToMove, Vector3 targetPos, float time)
    {
        Vector3 startPos = objectToMove.position;
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;

            // Вычисляем коэффициент прогресса (от 0 до 1)
            float t = elapsedTime / time;

            // Линейно интерполируем позицию
            // Обновляем targetObject.position в цикле на случай, если цель движется
            objectToMove.position = Vector3.Lerp(startPos, targetPos, t);

            yield return null; // Ждем следующий кадр
        }

        // Финальная корректировка для точности
        objectToMove.position = targetPos;
    }
    */


}
