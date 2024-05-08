using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerItemPicker : MonoBehaviour
{
    Heallth _heallth;

    private void Awake()
    {
        _heallth = GetComponent<Heallth>();
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.TryGetComponent(out Coin coin))
        {
            coin.PickUp();
        }

        if (collider2D.TryGetComponent(out Heart heart))
        {
            _heallth.Treat(heart.Hit);
            heart.PickUp();
        }
    }
}
