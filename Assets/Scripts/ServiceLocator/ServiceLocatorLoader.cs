using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ServiceLocatorLoader_Menu : MonoBehaviour
{
    private UIModel uiModel;
    private Model model;
    private Presenter presenter;
    private EventBus eventBus;
    private PagesManager pagesManager;
    private InstrumentController instrumentController;
    

    private void Awake()
    {
        uiModel = new UIModel();
        model = new Model();
        presenter = new Presenter();

        eventBus = new EventBus();
        pagesManager = GetComponent<PagesManager>();
        instrumentController = GetComponent<InstrumentController>();

        RegisterServices();

        StartCoroutine(InitAddressables());
        InitServices();
    }

    /// <summary>
    /// Инициирует сервис локатор и регистрирует необходимые сервисы
    /// </summary>
    private void RegisterServices()
    {
        ServiceLocator.Init();
        ServiceLocator.Instance.Register(uiModel);
        ServiceLocator.Instance.Register(model);
        ServiceLocator.Instance.Register(eventBus);
        ServiceLocator.Instance.Register(presenter);
        ServiceLocator.Instance.Register(pagesManager);
        ServiceLocator.Instance.Register(instrumentController);
    }

    /// <summary>
    /// Запускает процедуры инициализации у сервисов, где она есть
    /// </summary>
    private void InitServices()
    {
        uiModel.Init();
        model.Init();
        presenter.Init();
        pagesManager.Init();
        instrumentController.Init();
    }

    IEnumerator InitAddressables()
    {
        // Начинаем прогрев системы Addressables
        var initHandle = Addressables.InitializeAsync();

        yield return initHandle;

        if (initHandle.IsValid())
        {
            if (initHandle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
            {
                Debug.Log("Успешная инициализация");
            }
        }
        else
        {
            // Если IsValid == false, значит инициализация уже завершена 
            // и ресурсы хэндла были освобождены автоматически.
            Debug.Log("Инициализация завершена (хэндл уже освобожден)");
        }
    }

}
