using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class BrushTip : MonoBehaviour
{
    EventBus eventBus;
    UIModel uiModel;

    private Image tipImage;

    float takeColorTime = 1.0f;
    float releaseColorTime = 1.5f;
    float resetColorTime = 0.2f;

    private AsyncOperationHandle tipHandle;

    private string folder = "Brush tips/";
    private List<string> tipImages = new List<string>()
    {
        "blush_tip_color_1.png",
        "blush_tip_color_2.png",
        "blush_tip_color_3.png",
        "blush_tip_color_4.png",
        "blush_tip_color_5.png",
        "blush_tip_color_6.png",
        "blush_tip_color_7.png",
        "blush_tip_color_8.png",
        "blush_tip_color_9.png"

    };

    void Start()
    {
        eventBus = ServiceLocator.Instance.Get<EventBus>();
        uiModel = ServiceLocator.Instance.Get<UIModel>();

        tipImage = GetComponent<Image>();

        eventBus.blushColorSelected += OnBlushColorSelected;
        eventBus.brushTouchedColor += OnBrushTouchedColor;
        eventBus.blushAnimationStarted += OnBlushAnimationStarted;
        eventBus.blushColorReset += OnBlushColorReset;
    }

    private void OnBlushColorSelected()
    {
        LoadAsync(uiModel.color, tipImage);
    }

    private void OnBrushTouchedColor()
    {
        StartCoroutine(Utils.FadeInRoutine(takeColorTime, tipImage));
    }

    private void OnBlushAnimationStarted()
    {
        StartCoroutine(Utils.FadeOutRoutine(releaseColorTime, tipImage));
    }

    private void OnBlushColorReset()
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

        tipHandle = Addressables.LoadAssetAsync<Sprite>(folder + tipImages[index-1]);
        Sprite blushSprite = (Sprite)await tipHandle.Task;
        if (blushSprite != null)
        {
            holder.sprite = blushSprite;
        }
        else
        {
            Debug.Log("Failed to load sprite");
        }
    }

    private void OnDestroy()
    {
        eventBus.blushColorSelected -= OnBlushColorSelected;
        eventBus.brushTouchedColor -= OnBrushTouchedColor;
        eventBus.blushAnimationStarted -= OnBlushAnimationStarted;
        eventBus.blushColorReset -= OnBlushColorReset;
    }
}
