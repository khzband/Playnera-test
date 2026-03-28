using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AcneController : MonoBehaviour
{
    EventBus eventBus;
    Model model;

    public Image acneImage;

    float animationTime = 0.5f;
    float removeTime = 0.5f;

    void Start()
    {
        eventBus = ServiceLocator.Instance.Get<EventBus>();
        model = ServiceLocator.Instance.Get<Model>();

        eventBus.acneAnimationStarted += AcneAnimation;
        eventBus.creamRemoved += RemoveCream;
    }

    private void AcneAnimation()
    {
        if (model.acne)
        {
            StartCoroutine(Utils.FadeInRoutine(animationTime, acneImage));
        }
        else
        {
            if (!model.acneAlreadyRemoved)
            {
                StartCoroutine(Utils.FadeOutRoutine(animationTime, acneImage));
            }
            
        }
    }

    private void RemoveCream()
    {
        StartCoroutine(Utils.FadeInRoutine(removeTime, acneImage));
    }


    private void OnDestroy()
    {
        eventBus.acneAnimationStarted -= AcneAnimation;
        eventBus.creamRemoved -= RemoveCream;
    }

}
