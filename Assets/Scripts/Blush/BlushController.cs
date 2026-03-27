using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class BlushController : MonoBehaviour
{
    EventBus eventBus;

    public Image blushHolder1;
    public Image blushHolder2;
    int activeHolder;

    float animationTime = 1.5f;

    private AsyncOperationHandle blushHandle;

    private string blushFolder = "Blush/";
    private List<string> blushImages = new List<string>()
    {
        "blush_color_01.png",
        "blush_color_02.png",
        "blush_color_03.png",
        "blush_color_04.png",
        "blush_color_05.png",
        "blush_color_06.png",
        "blush_color_07.png",
        "blush_color_08.png",
        "blush_color_09.png"

    };

    void Start()
    {
        eventBus = ServiceLocator.Instance.Get<EventBus>();

        eventBus.blushColorSet += ApplyBlushColor;
        eventBus.blushAnimationStarted += BlushAnimation;

        //LoadBlushAsync(0);
        Addressables.DownloadDependenciesAsync(blushFolder + blushImages[0]);
        activeHolder = 1;
    }

    private void ApplyBlushColor(int color)
    {
        if (color > 0)
        {
            if (activeHolder == 1) 
            {
                LoadBlushAsync(color - 1, blushHolder1);
            }
            else
            {
                LoadBlushAsync(color - 1, blushHolder2);
            }
            
        }
        else
        {
            if (activeHolder == 1)
            {
                blushHolder1.enabled = false;
            }
            else
            {
                blushHolder2.enabled = false;
            }
                
        }
    }

    async void LoadBlushAsync(int index, Image holder)
    {
        if (blushHandle.IsValid())
        {
            Addressables.Release(blushHandle);
            Debug.Log("Handle released");
        }

        blushHandle = Addressables.LoadAssetAsync<Sprite>(blushFolder + blushImages[index]);
        Sprite blushSprite = (Sprite)await blushHandle.Task;
        if (blushSprite != null)
        {
            holder.sprite = blushSprite;
        }
        else
        {
            Debug.Log("Failed to load sprite");
        }
    }

    private void BlushAnimation()
    {
        if(activeHolder == 1)
        {
            StartCoroutine(FadeInRoutine(animationTime, blushHolder1));
            if(blushHolder2.sprite != null)
            {
                StartCoroutine(FadeOutRoutine(animationTime, blushHolder2));
            }
            
        }
        else
        {
            StartCoroutine(FadeInRoutine(animationTime, blushHolder2));
            StartCoroutine(FadeOutRoutine(animationTime, blushHolder1));
        }
        
    }

    IEnumerator FadeInRoutine(float duration, Image holder)
    {
        //holder.color = new Color(1, 1, 1, 0);
        //holder.enabled = true;
        Color color = holder.color;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            // Вычисляем альфу от 0 до 1
            color.a = Mathf.Clamp01(elapsedTime / duration);
            holder.color = color;

            yield return null; // Ждем следующего кадра
        }

        // Гарантируем полную непрозрачность в конце
        color.a = 1f;
        holder.color = color;

        // Переключаем холдеры
        SwitchHolders();
    }

    IEnumerator FadeOutRoutine(float duration, Image holder)
    {
        Color color = holder.color;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            // Вычисляем альфу от 0 до 1
            color.a = Mathf.Clamp01(1f - elapsedTime / duration);
            holder.color = color;

            yield return null; // Ждем следующего кадра
        }

        // Гарантируем полную прозрачность в конце
        color.a = 0f;
        holder.color = color;
    }

    private void SwitchHolders()
    {
        if(activeHolder == 1)
        {
            activeHolder = 2;
        }
        else
        {
            activeHolder = 1;
        }
    }

    private void OnDisable()
    {
        eventBus.blushColorSet -= ApplyBlushColor;
    }
}
