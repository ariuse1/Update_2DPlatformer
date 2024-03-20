using UnityEngine;
using UnityEngine.Events;


public class TargetBox : MonoBehaviour
{
    private UnityEvent _worked = new UnityEvent();
    public Transform Target { get; private set; }
 

    public event UnityAction Worked
    {
        add => _worked.AddListener(value);
        remove => _worked.RemoveListener(value);
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.TryGetComponent(out Player hero))
        {           
            Target = hero.transform;
          
            _worked.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.TryGetComponent(out Player hero))
        {           
            Target = null;
          
            _worked.Invoke();
        }
    }
}
