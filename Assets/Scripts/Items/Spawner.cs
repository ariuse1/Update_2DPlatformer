using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Item _item;
    [SerializeField] private int _maxCountItem;
    [SerializeField] private SpawnAreas _spawnPosition;

    private int _itemsCount;
    private bool _isWork;

    private void Start()
    {
        _itemsCount = 0;
        _isWork = true;
        StartCoroutine(Creat());
    }

    private void StartSpawn()
    {
        if (_itemsCount > 0)
        {
            _itemsCount--;
        }

        if (!_isWork)
        {
            StartCoroutine(Creat());
        }
    }

    private IEnumerator Creat()
    {
        float spawnSeconds = 5;
        WaitForSeconds waitTime = new(spawnSeconds);   

        while (_itemsCount < _maxCountItem)
        {           
            yield return waitTime;

            Vector2 position = _spawnPosition.GetSpawnPosition();
            Item newCoin = Instantiate(_item, position, Quaternion.identity);
            newCoin.transform.SetParent(gameObject.transform);
            newCoin.Worked += StartSpawn;
            _itemsCount++;
        }

        _isWork = false;
    }       
}
