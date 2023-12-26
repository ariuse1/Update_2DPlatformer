using UnityEngine;

public class Coin : MonoBehaviour
{   
    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.TryGetComponent<Hero>(out Hero hero))
        {    
            GetComponentInParent<AllCoins>().StartSpawn();
            Destroy(gameObject);
        }
    }
}
