using UnityEngine;

public class SpawnAreas : MonoBehaviour
{  
    private Area[] _areas;
    private int _numberArea;
    
    private void Awake()
    {
        _areas = GetComponentsInChildren<Area>();
    }

    public Vector2 GetSpawnPosition()
    {       
        float positionX;     

        _numberArea = Random.Range(0, _areas.Length);

        float positionAreaX = _areas[_numberArea].transform.position.x;
        float edgeDistance = _areas[_numberArea].transform.localScale.x / 2;
      
        positionX = Random.Range(positionAreaX - edgeDistance, positionAreaX + edgeDistance);             

        return new Vector2(positionX, _areas[_numberArea].transform.position.y);
    } 
}
