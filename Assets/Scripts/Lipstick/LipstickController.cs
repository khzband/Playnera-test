using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class LipstickController : MonoBehaviour
{
    EventBus eventBus;
    Model model;

    public Image lipstickHolder1;
    public Image lipstickHolder2;
    int activeHolder;

    float animationTime = 1.5f;

    private AsyncOperationHandle lipstickHandle;

    private string lipstickFolder = "Lipstick/";
    private List<string> lipstickImages = new List<string>()
    {
        "lipstick_01.png",
        "lipstick_02.png",
        "lipstick_03.png",
        "lipstick_04.png",
        "lipstick_05.png",
        "lipstick_06.png"

    };


    void Start()
    {
        eventBus = ServiceLocator.Instance.Get<EventBus>();
        model = ServiceLocator.Instance.Get<Model>();

        eventBus.lipstickColorSet += LoadLipstickSprite;
        eventBus.lipstickAnimationStarted += LipstickAnimation;

        //Addressables.DownloadDependenciesAsync(lipstickFolder + lipstickImages[0]);
        activeHolder = 1;
    }

    private void LoadLipstickSprite()
    {
        int color = model.lipstickColor;
        if (color > 0)
        {
            if (activeHolder == 1)
            {
                LoadLipstickAsync(color - 1, lipstickHolder1);
            }
            else
            {
                LoadLipstickAsync(color - 1, lipstickHolder2);
            }

        }
        else
        {
            if (activeHolder == 1)
            {
                lipstickHolder1.enabled = false;
            }
            else
            {
                lipstickHolder2.enabled = false;
            }

        }
    }

    async void LoadLipstickAsync(int index, Image holder)
    {
        if (lipstickHandle.IsValid())
        {
            Addressables.Release(lipstickHandle);
            Debug.Log("Handle released");
        }

        lipstickHandle = Addressables.LoadAssetAsync<Sprite>(lipstickFolder + lipstickImages[index]);
        Sprite lipstickSprite = (Sprite)await lipstickHandle.Task;
        if (lipstickSprite != null)
        {
            holder.sprite = lipstickSprite;
        }
        else
        {
            Debug.Log("Failed to load sprite");
        }
    }

    private void LipstickAnimation()
    {
        if (activeHolder == 1)
        {
            StartCoroutine(StartFadeInRoutine(animationTime, lipstickHolder1));
            if (lipstickHolder2.sprite != null)
            {
                StartCoroutine(Utils.FadeOutRoutine(animationTime, lipstickHolder2));
            }

        }
        else
        {
            StartCoroutine(StartFadeInRoutine(animationTime, lipstickHolder2));
            StartCoroutine(Utils.FadeOutRoutine(animationTime, lipstickHolder1));
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
        eventBus.lipstickColorSet -= LoadLipstickSprite;
        eventBus.lipstickAnimationStarted -= LipstickAnimation;
    }
}
