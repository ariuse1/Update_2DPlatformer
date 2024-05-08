using System;
using UnityEngine;

abstract public class Item : MonoBehaviour
{
    public event Action Worked;

    public virtual void PickUp()
    {
        Worked.Invoke();
        Destroy(gameObject);
    }   
}
