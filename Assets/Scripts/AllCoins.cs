using System.Collections;
using UnityEngine;

public class AllCoins : MonoBehaviour
{
    [SerializeField] private Coin _coin;
    [SerializeField] private int _maxCountCoin;    
    [SerializeField] private SpawnAreas _spawnPosition;    

    private Coin[] _coins;    
    private bool _isAllCoins = false;

    private void Start()
    {
        StartCoroutine(Creat());
    }    

    public void StartSpawn()
    {
        if (_isAllCoins)
        {
            _isAllCoins = false;
            StartCoroutine(Creat());
        }            
    }

    private IEnumerator Creat()
    {
        float spawnSeconds = 5;        
        WaitForSeconds waitTime = new(spawnSeconds);

        while (_isAllCoins == false)
        {
            yield return waitTime;

            _coins = GetComponentsInChildren<Coin>();            

            if (_coins.Length < _maxCountCoin)
            {               
                Vector2 position = _spawnPosition.GetSpawnPosition();                
                Coin newCoin = Instantiate(_coin, position, Quaternion.identity);

                newCoin.transform.SetParent(gameObject.transform);
            }
            else
            {
                _isAllCoins = true;
                break;
            }            
        }
    }  
}
