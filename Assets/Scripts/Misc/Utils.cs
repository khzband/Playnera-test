using UnityEngine;
using System.Collections;

public class Utils
{
    /// <summary>
    /// Рутина перемещения объекта в заданную точку с постоянной скоростью
    /// </summary>
    public static IEnumerator MoveRoutine(RectTransform objectToMove, Vector3 targetPos, float speed)
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

    /// <summary>
    /// Рутина перемещения объекта в заданную точку за заданное время
    /// </summary>
    public static IEnumerator MoveByTimeRoutine(RectTransform objectToMove, Vector3 targetPos, float time)
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
}
