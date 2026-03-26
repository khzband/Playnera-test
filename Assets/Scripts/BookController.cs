using Unity.VisualScripting;
using UnityEngine;

public class BookController : IService
{
    /*    
    // Код открытой страницы книги
    private int _bookPage;

    // Допустимые значения страницы книги
    public int bookPage
    {
        get { return _bookPage; }

        set 
        { 
            if (value >= 0 && value < 4)
            {
                _bookPage = value;
            }
            
        }
    }
    */

    public Page bookPage;

    private EventBus eventBus;

    /// <summary>
    /// Инициализация книги при загрузке
    /// </summary>
    public void Init()
    {
        bookPage = Page.Powder;
        eventBus = ServiceLocator.Instance.Get<EventBus>();
        //eventBus.onServicesLoaded += ;
    }
}
