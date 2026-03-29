using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class EyebrushTip : MonoBehaviour
{
    EventBus eventBus;
    UIModel uiModel;

    private Image tipImage;

    float takeColorTime = 1.0f;
    //float releaseColorTime = 1.5f;
    float resetColorTime = 0.2f;

    private AsyncOperationHandle tipHandle;

    private string folder = "Eyebrush tips/";
    private List<string> tipImages = new List<string>()
    {
        "eyebrush_tip_color_1.png",
        "eyebrush_tip_color_2.png",
        "eyebrush_tip_color_3.png",
        "eyebrush_tip_color_4.png",
        "eyebrush_tip_color_5.png",
        "eyebrush_tip_color_6.png",
        "eyebrush_tip_color_7.png",
        "eyebrush_tip_color_8.png",
        "eyebrush_tip_color_9.png"

    };


    void Start()
    {
        eventBus = ServiceLocator.Instance.Get<EventBus>();
        uiModel = ServiceLocator.Instance.Get<UIModel>();

        tipImage = GetComponent<Image>();

        eventBus.eyeshadowsColorSelected += OnEyeshadowsColorSelected;
        eventBus.eyebrushTouchedColor += OnEyebrushTouchedColor;
        //eventBus.eyeshadowsAnimationStarted += OnEyeshadowsAnimationStarted;
        eventBus.eyeshadowsColorReset += OnEyeshadowsColorReset;
    }

    private void OnEyeshadowsColorSelected()
    {
        LoadAsync(uiModel.color, tipImage);
    }

    private void OnEyebrushTouchedColor()
    {
        StartCoroutine(Utils.FadeInRoutine(takeColorTime, tipImage));
    }

    /*
    private void OnEyeshadowsAnimationStarted()
    {
        StartCoroutine(Utils.FadeOutRoutine(releaseColorTime, tipImage));
    }
    */

    private void OnEyeshadowsColorReset()
    {
        StartCoroutine(Utils.FadeOutRoutine(resetColorTime, tipImage));
    }

    async void LoadAsync(int index, Image holder)
    {
        if (tipHandle.IsValid())
        {
            Addressables.Release(tipHandle);
            Debug.Log("Handle released");
        }

        tipHandle = Addressables.LoadAssetAsync<Sprite>(folder + tipImages[index - 1]);
        Sprite eyeshadowsSprite = (Sprite)await tipHandle.Task;
        if (eyeshadowsSprite != null)
        {
            holder.sprite = eyeshadowsSprite;
        }
        else
        {
            Debug.Log("Failed to load sprite");
        }
    }

    void OnDestroy()
    {
        Debug.Log("OnDestroy invoked");
        eventBus.eyeshadowsColorSelected -= OnEyeshadowsColorSelected;
        eventBus.eyebrushTouchedColor -= OnEyebrushTouchedColor;
        //eventBus.eyeshadowsAnimationStarted -= OnEyeshadowsAnimationStarted;
        eventBus.eyeshadowsColorReset -= OnEyeshadowsColorReset;
    }
}
