using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Lipstick : Instrument
{
    EventBus eventBus;
    Presenter presenter;

    Vector3 startZone;
    public RectTransform readyZone;
    public RectTransform lipsZone;

    public Image bookImage;
    private Image lipstickImage;

    public int id;

    private float speed = 900f;
    private Vector3 rotateAngles = new Vector3(0, 0, 10);
    private float rotateDuration = 0.2f;

    private RectTransform rectTransform; // RectTransform помады

    void Start()
    {
        eventBus = ServiceLocator.Instance.Get<EventBus>();
        presenter = ServiceLocator.Instance.Get<Presenter>();

        rectTransform = GetComponent<RectTransform>();
        lipstickImage = GetComponent<Image>();

        startZone = rectTransform.position; // Запоминаем стартовую позицию
    }

    // Вызов начальной анимации, поступает с InstrumentController
    public override void GetReady()
    {
        lipstickImage.enabled = true;
        bookImage.enabled = false;
        Debug.Log($"Lipstick {id} started");
        StartCoroutine(GetReadySequence());

    }

    IEnumerator GetReadySequence()
    {
        // Поворачиваем
        yield return StartCoroutine(Utils.RotateOverTimeRoutine(rectTransform, rotateAngles, rotateDuration));

        // Перемещение в зону готовности
        yield return StartCoroutine(Utils.MoveRoutine(rectTransform, readyZone.position, speed));

        presenter.OnInstrumentReady();
    }

    // Вызов анимации нанесения, поступает с InstrumentController
    public override void ApplyInstrument()
    {
        StartCoroutine(ApplyLipstickSequence());
        
    }

    IEnumerator ApplyLipstickSequence()
    {
        Vector3 offsetL = new Vector3(-50, 0, 0);
        Vector3 offsetR = new Vector3(50, 0, 0);
        float moveTime = 0.3f;

        // Перемещаем к начальной точке
        yield return StartCoroutine(Utils.MoveRoutine(rectTransform, lipsZone.position + offsetL, speed));


        // Анимация нанесения цвета на лицо
        eventBus.lipstickAnimationStarted?.Invoke();
        for (int i = 0; i < 2; i++)
        {
            yield return StartCoroutine(Utils.MoveByTimeRoutine(rectTransform, lipsZone.position + offsetR, moveTime));
            yield return StartCoroutine(Utils.MoveByTimeRoutine(rectTransform, lipsZone.position + offsetL, moveTime));
        }
        yield return StartCoroutine(Utils.MoveByTimeRoutine(rectTransform, lipsZone.position + offsetR, moveTime));

        // Возвращаем кисть на место
        yield return StartCoroutine(Utils.MoveRoutine(rectTransform, startZone, speed));

        // Поворачиваем в начальное положение
        yield return StartCoroutine(Utils.RotateOverTimeRoutine(rectTransform, Vector3.zero, rotateDuration));

        bookImage.enabled = true;
        lipstickImage.enabled = false;

        presenter.OnCycleCompleted();
    }

}
