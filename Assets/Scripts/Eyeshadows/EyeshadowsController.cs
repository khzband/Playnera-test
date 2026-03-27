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

        //eventBus.eyeshadowsColorSet += LoadEyeshadowsSprite;
        //eventBus.eyeshadowsAnimationStarted += EyeshadowsAnimation;

        Addressables.DownloadDependenciesAsync(eyeshadowsFolder + eyeshadowsImages[0]);
        activeHolder = 1;
    }

    
}
