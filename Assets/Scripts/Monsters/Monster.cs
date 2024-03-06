using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AnimationObject))]
[RequireComponent(typeof(MoveObject))]
[RequireComponent(typeof(Heallth))]

abstract public class Monster : MonoBehaviour, IDie
{
    [SerializeField] protected float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] protected float _attackDamage;
    [SerializeField] private TargetBox[] _targetGoalBoxs;
    [SerializeField] private TargetBox _attackBox;

    protected AnimationObject _animationObject;
    protected StatesAnim _state;

    protected bool _isFlipX = false;
    protected bool _isGoal = false;
    protected bool _isGround = false;
    protected bool _isMove = false;
    protected bool _isReturn = false;
    protected bool _isAttack = false;

    protected MoveObject _moveObject;
    protected Vector2 _returnPoint;
    private Point[] _movementPoints;

    private int _directionPath;
    private int _currentPoint;
    private int _nextPoint;
    private float _timeNextAttack;
    private Transform _goal;
    private Vector2 _target;

    private void Awake()
    {
        _animationObject = GetComponent<AnimationObject>();
        _moveObject = GetComponent<MoveObject>();
        _attackDamage = 1;
        _timeNextAttack = Time.deltaTime;
    }

    public void SetStartParameters(Path path, int numberStartPosition, int directionPath)
    {
        _movementPoints = path.GetComponentsInChildren<Point>();
        _currentPoint = numberStartPosition;
        _directionPath = directionPath;
        _nextPoint = GetNextPoint();
    }

    public void Die()
    {
        Debug.Log("Die");
        Destroy(gameObject);
    }

    protected void Action()
    {
        _isGround = _moveObject.CheckGround();
        _state = StatesAnim.Idle;

        if ((_isGoal || _isReturn) && !_isAttack)
        {
            GoalOrReturn();
        }

        if (!_isGoal && !_isReturn && !_isAttack)
        {
            MoveToPath();
            _state = StatesAnim.Run;
        }

        if (_isAttack)
        {            
            if (_timeNextAttack <= Time.time && _isGround)
            {
                Attack();
            }
        }

        if (_isGround == false)
        {
            _state = StatesAnim.Jump;
        }

        if (_moveObject.IsJump)
        {
            _state = StatesAnim.Jump;
        }
    
        _moveObject.Flip(transform.position.x - _target.x, _isFlipX);
        _isFlipX = _moveObject.IsFlip;
    }

    protected void MoveToPath()
    {
        float accuracy = 0.7f;       
        _target = _movementPoints[_nextPoint].transform.position;

        if (Mathf.Abs(transform.position.x - _target.x) < accuracy)
        {
            _currentPoint = _nextPoint;
            _nextPoint = GetNextPoint();
            _target = _movementPoints[_nextPoint].transform.position;
        }

        _moveObject.MoveToTarget(_target, _speed, _jumpForce);
    }

    protected void Attack()
    {
        float waitTime = 1f;
        _isAttack = _attackBox.Target != null;

        if (_isAttack)
        {
            _state = StatesAnim.Attack;
            _attackBox.Target.GetComponent<Heallth>()?.TakeDamage(_attackDamage);
            _timeNextAttack = Time.time + waitTime;
        }
    }

    private void GoalOrReturn()
    {       
        float accuracy = 0.5f;

        if (_isGoal)
        {
            _target = _goal.position;
        }
        else
        {
            _target = _returnPoint;

            if (Mathf.Abs(transform.position.x - _returnPoint.x) < accuracy && !_isGoal)
            {
                _isReturn = false;
            }
        }

        _moveObject.MoveToTargetNotJump(_target, _speed);
        _state = StatesAnim.Run;
    }

    private int GetNextPoint()
    {
        int nextPointPath = _currentPoint + _directionPath;

        if (nextPointPath < 0 || nextPointPath >= _movementPoints.Length)
            _directionPath *= -1; ;

        return _currentPoint + _directionPath;
    }

    private IEnumerator WaitWhile(bool isWait)
    {
        yield return new WaitWhile(() => isWait);
    }

    private void FindTargetGoal()
    {
        StartCoroutine(WaitWhile(!_isGround));
        StartCoroutine(WaitWhile(!_isAttack));

        foreach (var pointTarget in _targetGoalBoxs)
        {
            if (pointTarget.Target != null)
            {
                if (!_isReturn)
                {
                    _returnPoint = transform.position;
                    _isReturn = true;
                }

                _goal = pointTarget.Target;
                _isGoal = true;

                break;
            }

            _isGoal = false;
        }
    }

    private void isAttack()
    {
        _isAttack = true;
    }

    private void OnEnable()
    {
        foreach (var targetGoalBox in _targetGoalBoxs)
            targetGoalBox.Worked += FindTargetGoal;

        _attackBox.Worked += isAttack;
    }

    private void OnDisable()
    {
        foreach (var pointTarget in _targetGoalBoxs)
            pointTarget.Worked -= FindTargetGoal;

        _attackBox.Worked -= isAttack;
    }
}

