using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class DollController : MonoBehaviour
{
    EventBus eventBus;

    public Image blushHolder;

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
    }

    private void ApplyBlushColor(int color)
    {
        if (color > 0)
        {
            LoadBlushAsync(color - 1);
        }
        else
        {
            blushHolder.enabled = false;
        }
    }

    async void LoadBlushAsync(int index)
    {
        if (blushHandle.IsValid())
        {
            Addressables.Release(blushHandle);
        }

        blushHandle = Addressables.LoadAssetAsync<Sprite>(blushFolder + blushImages[index]);
        Sprite blushSprite = (Sprite)await blushHandle.Task;
        if (blushSprite != null)
        {
            blushHolder.sprite = blushSprite;
            blushHolder.enabled = true;
        }
        else
        {
            Debug.Log("Failed to load sprite");
        }
        
    }


    private void OnDisable()
    {
        eventBus.blushColorSet -= ApplyBlushColor;
    }

}
