using UnityEngine;

public class BlushPage : MonoBehaviour, IPage
{
    public GameObject instrumentObject;

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
