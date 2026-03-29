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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
