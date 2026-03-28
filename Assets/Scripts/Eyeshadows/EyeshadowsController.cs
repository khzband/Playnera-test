using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class EyeshadowsController : MonoBehaviour
{
    EventBus eventBus;
    Model model;

    public Image eyeshadowsHolder1;
    public Image eyeshadowsHolder2;
    int activeHolder;

    float animationTime = 1.5f;

    private AsyncOperationHandle eyeshadowsHandle;

    private string eyeshadowsFolder = "Eyeshadows/";
    private List<string> eyeshadowsImages = new List<string>()
    {
        "eyeshadow_color_01.png",
        "eyeshadow_color_02.png",
        "eyeshadow_color_03.png",
        "eyeshadow_color_04.png",
        "eyeshadow_color_05.png",
        "eyeshadow_color_06.png",
        "eyeshadow_color_07.png",
        "eyeshadow_color_08.png",
        "eyeshadow_color_09.png"

    };

    void Start()
    {
        eventBus = ServiceLocator.Instance.Get<EventBus>();
        model = ServiceLocator.Instance.Get<Model>();

        eventBus.eyeshadowsColorSet += LoadEyeshadowsSprite;
        eventBus.eyeshadowsAnimationStarted += EyeshadowsAnimation;

        //Addressables.DownloadDependenciesAsync(eyeshadowsFolder + eyeshadowsImages[0]);
        activeHolder = 1;
    }

    private void LoadEyeshadowsSprite()
    {
        int color = model.eyeshadowsColor;
        if (color > 0)
        {
            if (activeHolder == 1)
            {
                LoadEyeshadowsAsync(color - 1, eyeshadowsHolder1);
            }
            else
            {
                LoadEyeshadowsAsync(color - 1, eyeshadowsHolder2);
            }

        }
        else
        {
            if (activeHolder == 1)
            {
                eyeshadowsHolder1.enabled = false;
            }
            else
            {
                eyeshadowsHolder2.enabled = false;
            }

        }
    }


    async void LoadEyeshadowsAsync(int index, Image holder)
    {
        if (eyeshadowsHandle.IsValid())
        {
            Addressables.Release(eyeshadowsHandle);
            Debug.Log("Handle released");
        }

        eyeshadowsHandle = Addressables.LoadAssetAsync<Sprite>(eyeshadowsFolder + eyeshadowsImages[index]);
        Sprite eyeshadowsSprite = (Sprite)await eyeshadowsHandle.Task;
        if (eyeshadowsSprite != null)
        {
            holder.sprite = eyeshadowsSprite;
        }
        else
        {
            Debug.Log("Failed to load sprite");
        }
    }

    private void EyeshadowsAnimation()
    {
        if (activeHolder == 1)
        {
            StartCoroutine(StartFadeInRoutine(animationTime, eyeshadowsHolder1));
            if (eyeshadowsHolder2.sprite != null)
            {
                StartCoroutine(Utils.FadeOutRoutine(animationTime, eyeshadowsHolder2));
            }

        }
        else
        {
            StartCoroutine(StartFadeInRoutine(animationTime, eyeshadowsHolder2));
            StartCoroutine(Utils.FadeOutRoutine(animationTime, eyeshadowsHolder1));
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
        if (activeHolder == 1)
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
        eventBus.eyeshadowsColorSet -= LoadEyeshadowsSprite;
        eventBus.eyeshadowsAnimationStarted -= EyeshadowsAnimation;
    }

}
