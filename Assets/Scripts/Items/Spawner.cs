using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Item _item;
    [SerializeField] private int _maxCountItem;
    [SerializeField] private SpawnerAreas _spawnPosition;
   
    private int _itemsCount;
    private bool _isWork;

    private void Start()
    {
        _itemsCount = 0;
        _isWork = true;
        StartCoroutine(Create());
    }

    private void StartSpawn()
    {
        if (_itemsCount > 0)
        {
            _itemsCount--;
        }

        if (!_isWork)
        {
            StartCoroutine(Create());
        }
    }

    private IEnumerator Create()
    {
        float spawnSeconds = 5;
        WaitForSeconds waitTime = new(spawnSeconds);   

        while (_itemsCount < _maxCountItem)
        {           
            yield return waitTime;

            Vector2 position = _spawnPosition.GetSpawnPosition();
            Item newItem = Instantiate(_item, position, Quaternion.identity);
            newItem.transform.SetParent(gameObject.transform);
            newItem.Worked += StartSpawn;
            _itemsCount++;
        }

        _isWork = false;
    }

    private void OnDisable()
    {      
        Item[] items = GetComponentsInChildren<Item>(); 

        foreach (Item item in items)
        {
            item.Worked -= StartSpawn;
        }        
    }
}
