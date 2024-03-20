using UnityEngine;
using UnityEngine.Events;

abstract public class Item : MonoBehaviour
{
    [SerializeField] private UnityEvent _worked = new();
    private IActionItem actionItem;   

    private void Awake()
    {
        actionItem = GetComponent<IActionItem>(); 
    }

    public event UnityAction Worked
    {
        add => _worked.AddListener(value);
        remove => _worked.RemoveListener(value);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.TryGetComponent<Player>(out Player hero))
        {
            actionItem?.AddItem(hero);            
            _worked.Invoke();
            Destroy(gameObject);          
        }
    }
}
