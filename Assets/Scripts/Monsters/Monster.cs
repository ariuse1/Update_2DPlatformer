using UnityEngine;

[RequireComponent(typeof(AnimationObject))]
[RequireComponent(typeof(MoveObject))]
[RequireComponent(typeof(MoveToRoute))]
[RequireComponent(typeof(Pursuit))]
[RequireComponent(typeof(CombatMonster))]

abstract public class Monster : MonoBehaviour, IDie
{
    [SerializeField] protected float _speed;
    [SerializeField] protected float _jumpForce;  

    protected AnimationObject _animationObject;
    protected StatesAnim _state;
    protected bool _isFlipX = false;

    protected MoveToRoute _movementRoute;
    protected Pursuit _pursuit;
    protected CombatMonster _combat;
    protected MoveObject _moveObject;
    protected Vector2 _target;    

    private bool _isJump = false;

    private void Awake()
    {
        _animationObject = GetComponent<AnimationObject>();
        _moveObject = GetComponent<MoveObject>();
        _movementRoute = GetComponent<MoveToRoute>();
        _pursuit = GetComponent<Pursuit>();
        _combat = GetComponent<CombatMonster>();
    }

    public void SetPathParameters(Path path, int numberStartPosition, int directionPath)
    {
        _movementRoute.SetPathParameters(path, numberStartPosition, directionPath);
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    protected void Action()
    {      
        bool IsOnGround = _moveObject.IsOnGround();
        bool isGoalOrReturn = _pursuit.IsWork;
        bool isAttack = _combat.IsWork;

        _state = StatesAnim.Idle;
        _isJump = _moveObject.IsJump;

        if (_moveObject.IsJump || _isJump)
        {
            _isJump = true;
            _state = StatesAnim.Jump;

            if (IsOnGround)
            {
                _isJump = false;
            }            
        }

        if (isAttack && IsOnGround)
        {
            _combat.Attack();
            _target = _combat.Target;

            if (_combat.IsAttack)
            {
                _state = StatesAnim.Attack;
            }          
        }

        if (isGoalOrReturn && !isAttack && IsOnGround)
        {
            _pursuit.GoalOrReturn(_speed);
            _target = _pursuit.Target;
            _state = StatesAnim.Run;
        }

        if (!isGoalOrReturn && !isAttack)
        {           
            _movementRoute.MoveToPath(_speed, _jumpForce);
            _target = _movementRoute.Target;
            _state = StatesAnim.Run;
        }        

        _moveObject.Flip(transform.position.x - _target.x, _isFlipX);
        _isFlipX = _moveObject.IsFlip;
    }
}

