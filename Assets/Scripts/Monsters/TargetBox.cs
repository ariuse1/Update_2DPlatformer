using UnityEngine;
using UnityEngine.Events;

public class TargetBox : MonoBehaviour
{   
    public Transform Target { get; private set; }

    public event UnityAction Worked;

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.TryGetComponent(out Player hero))
        {           
            Target = hero.transform;
            Worked?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.TryGetComponent(out Player hero))
        {           
            Target = null;
            Worked?.Invoke();
        }
    }
}
