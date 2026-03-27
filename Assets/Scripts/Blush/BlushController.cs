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
    Model model;

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
        model = ServiceLocator.Instance.Get<Model>();

        eventBus.blushColorSet += LoadBlushSprite;
        eventBus.blushAnimationStarted += BlushAnimation;

        Addressables.DownloadDependenciesAsync(blushFolder + blushImages[0]);
        activeHolder = 1;
    }

    private void LoadBlushSprite()
    {
        int color = model.blushColor;
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
            StartCoroutine(StartFadeInRoutine(animationTime, blushHolder1));
            if(blushHolder2.sprite != null)
            {
                StartCoroutine(Utils.FadeOutRoutine(animationTime, blushHolder2));
            }
            
        }
        else
        {
            StartCoroutine(StartFadeInRoutine(animationTime, blushHolder2));
            StartCoroutine(Utils.FadeOutRoutine(animationTime, blushHolder1));
        }
        
    }

    IEnumerator StartFadeInRoutine(float duration, Image holder)
    {
        yield return StartCoroutine(Utils.FadeInRoutine(duration, holder));
        // Переключаем холдеры
        SwitchHolders();
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
        eventBus.blushColorSet -= LoadBlushSprite;
    }
}
