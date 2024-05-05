using UnityEngine;
using UnityEngine.Events;

abstract public class Item : MonoBehaviour
{
    private IActionItem _actionItem;

    private void Awake()
    {
        _actionItem = GetComponent<IActionItem>();
    }

    public event UnityAction Worked;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player hero))
        {
            _actionItem?.AddItem(hero);
            Worked.Invoke();
            Destroy(gameObject);
        }
    }
}
