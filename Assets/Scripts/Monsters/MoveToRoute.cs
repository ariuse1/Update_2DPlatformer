using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MoveObject))]

public class MoveToRoute : MonoBehaviour
{   
    private MoveObject _moveObject;
    private Point[] _movementPoints;
    private int _nextPoint;
    private int _currentPoint;
    private int _directionPath;

    public Vector2 Target { get; private set; }

    private void Awake()
    {
        _moveObject = GetComponent<MoveObject>();
    }

    public void SetPathParameters(Path path, int numberStartPosition, int directionPath)
    {
        _movementPoints = path.GetComponentsInChildren<Point>();
        _currentPoint = numberStartPosition;
        _directionPath = directionPath;
        _nextPoint = GetNextPoint();
    }

    public void MoveToPath(float speed, float jumpForce)
    {      
        float accuracy = 0.7f;
        Target = _movementPoints[_nextPoint].transform.position;

        if (Mathf.Abs(transform.position.x - Target.x) < accuracy)
        {
            _currentPoint = _nextPoint;
            _nextPoint = GetNextPoint();
            Target = _movementPoints[_nextPoint].transform.position;
        }

        _moveObject.MoveToTarget(Target, speed, jumpForce);
    }

    private int GetNextPoint()
    {
        int nextPointPath = _currentPoint + _directionPath;

        if (nextPointPath < 0 || nextPointPath >= _movementPoints.Length)
            _directionPath *= -1; ;

        return _currentPoint + _directionPath;
    }   
}
