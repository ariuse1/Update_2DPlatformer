using System.Linq;
using UnityEngine;

public class PointSpawnMonsters : MonoBehaviour
{
    [SerializeField] private Path[] _paths;
    [SerializeField] private Monster[] _monstersPrefab;
    [SerializeField] private int _countMonsters;

    private Monster[] _monsters;    

    public void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        int _distanceBetweenObject = 1;

        while (CheckQuantity())
        {
            Path selectPath = _paths[Random.Range(0, _paths.Length)];
            Monster selectMonster = _monstersPrefab[Random.Range(0, _monstersPrefab.Length)];

            Point[] points = selectPath.GetComponentsInChildren<Point>();
            int numbeSelectrPoint = Random.Range(0, points.Length);

            if (_monsters.Any(monster => Mathf.Abs(monster.transform.position.x - 
                points[numbeSelectrPoint].transform.position.x) <= _distanceBetweenObject) == false)
            {
                Monster monster = Instantiate(selectMonster, points[numbeSelectrPoint].transform.position, Quaternion.identity);
               
                monster.transform.SetParent(transform);
                monster.SetStartParameters(selectPath, numbeSelectrPoint, ChooseRandomDirection());
            }                
        }
    }

    private int ChooseRandomDirection()
    {
        int countDirection = 2;

        if (Random.Range(0, countDirection) == 0)
            return 1;
        else return -1;
    }

    private bool CheckQuantity()
    {
        _monsters = GetComponentsInChildren<Monster>();

        if (_monsters.Length >= _countMonsters)
            return false;
        else return true;
    }
}
