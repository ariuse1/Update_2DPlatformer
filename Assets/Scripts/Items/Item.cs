using UnityEngine;

abstract public class Item : MonoBehaviour
{
    protected void RunAction() { }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.TryGetComponent<Hero>(out Hero hero))
        {
            GetComponent<IActionItem>()?.Action(hero);
            GetComponentInParent<AllItems>()?.StartSpawn();
            Destroy(gameObject);
        }
    }
}
