using System.Linq;
using System.Threading;
using UnityEngine;

public class PointSpawnMonsters : MonoBehaviour
{
    [SerializeField] private Path[] _paths;
    [SerializeField] private Monster[] _monstersPrefab;
    [SerializeField] private int _maxCountMonsters;

    private Monster[] _monsters;
    private int _countMonsters;

    public void Start()
    {        
        Spawn();
    }

    private void Spawn()
    {
        int distanceBetweenObject = 1;

        while (_countMonsters < _maxCountMonsters)
        {
            Path selectPath = _paths[Random.Range(0, _paths.Length)];
            Monster selectMonster = _monstersPrefab[Random.Range(0, _monstersPrefab.Length)];

            Point[] points = selectPath.GetComponentsInChildren<Point>();
            int numbeSelectrPoint = Random.Range(0, points.Length);

            _monsters = GetComponentsInChildren<Monster>();

            if (_monsters.Any(monster => Mathf.Abs(monster.transform.position.x -
                points[numbeSelectrPoint].transform.position.x) <= distanceBetweenObject) == false)
            {
                Monster monster = Instantiate(selectMonster, points[numbeSelectrPoint].transform.position, Quaternion.identity);

                monster.transform.SetParent(transform);
                monster.SetPathParameters(selectPath, numbeSelectrPoint, ChooseRandomDirection());
                _countMonsters++;
            }            
        }
    }

    private int ChooseRandomDirection()
    {
        int countDirection = 2;

        return (Random.Range(0, countDirection) == 0) ? 1 : -1;
    }
}
