using UnityEngine;

[RequireComponent(typeof(MoveObject))]

public class Pursuit : MonoBehaviour
{
    [SerializeField] private TargetBox[] _targetGoalBoxs;

    private bool _isGoal;
    private bool _isReturn;
    private Vector2 _returnPoint;
    private MoveObject _moveObject;

    public bool IsWork { get; private set; }
    public Vector2 Target { get; private set; }

    private void Awake()
    {
        _moveObject = GetComponent<MoveObject>();     
    }

    public void GoalOrReturn(float speed)
    {
        float accuracy = 0.5f;

        if (_isGoal )
        {
            FindTargetGoal();
        }

        if(_isReturn && !_isGoal) 
        {
            Target = _returnPoint;

            if (Mathf.Abs(transform.position.x - _returnPoint.x) < accuracy && !_isGoal)
            {
                _isReturn = false;
            }
        }

        if(!_isReturn && !_isGoal)
        {
            IsWork = false;
        }
        
        _moveObject.MoveToTargetNotJump(Target, speed);        
    }

    private void FindTargetGoal()
    {
        IsWork = true;

        foreach (var targetGoalBox in _targetGoalBoxs)
        {
            if (targetGoalBox.Target != null)
            {
                if (!_isReturn)
                {
                    _returnPoint = transform.position;
                    _isReturn = true;
                }

                Target = targetGoalBox.Target.position;
                _isGoal = true;

                break;
            }

            _isGoal = false;
        }
    }

    private void OnEnable()
    {
        foreach (var targetGoalBox in _targetGoalBoxs)
            targetGoalBox.Worked += FindTargetGoal;
    }

    private void OnDisable()
    {
        foreach (var targetGoalBox in _targetGoalBoxs)
            targetGoalBox.Worked -= FindTargetGoal;     
    }
}
