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
    float removeTime = 0.5f;

    private AsyncOperationHandle lipstickHandle1;
    private AsyncOperationHandle lipstickHandle2;

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
        eventBus.lipstickRemoved += RemoveLipstick;

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
                LoadLipstickAsync(color - 1, lipstickHolder1, lipstickHandle1);
            }
            else
            {
                LoadLipstickAsync(color - 1, lipstickHolder2, lipstickHandle2);
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

    async void LoadLipstickAsync(int index, Image holder, AsyncOperationHandle handle)
    {
        if (handle.IsValid())
        {
            Addressables.Release(handle);
            Debug.Log("Handle released");
        }

        handle = Addressables.LoadAssetAsync<Sprite>(lipstickFolder + lipstickImages[index]);
        Sprite lipstickSprite = (Sprite)await handle.Task;
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

    private void RemoveLipstick()
    {
        // Здесь номера холдеров наоборот, потому что процедура в конце ставит номер холдера для следующего использования 
        if (activeHolder == 1)
        {
            StartCoroutine(RemoveRoutine(removeTime, lipstickHolder2));
        }
        else
        {
            StartCoroutine(RemoveRoutine(removeTime, lipstickHolder1));
        }
    }

    IEnumerator RemoveRoutine(float time, Image holder)
    {
        yield return StartCoroutine(Utils.FadeOutRoutine(time, holder));

        activeHolder = 1;
        lipstickHolder2.sprite = null;
    }

    private void OnDestroy()
    {
        eventBus.lipstickColorSet -= LoadLipstickSprite;
        eventBus.lipstickAnimationStarted -= LipstickAnimation;
        eventBus.lipstickRemoved -= RemoveLipstick;
    }
}
