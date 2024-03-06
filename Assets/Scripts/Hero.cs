using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AnimationObject))]
[RequireComponent(typeof(MoveObject))]
[RequireComponent(typeof(Heallth))]

public class Hero : MonoBehaviour, IDie
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private AttackPoint _atttackPoint;
    [SerializeField] private float _attackRange = 0.5f;
    [SerializeField] private float _attackDamage = 20;
    [SerializeField] private LayerMask _enemyLayers;

    private const string StringHorizontal = "Horizontal";
    private const string StringJump = "Jump";
    private const string StringAttack = "Fire1";

    private AnimationObject _animationObject;
    private MoveObject _moveObject;

    private bool _isGrounded = false;
    private StatesAnim _stateAnim;
    private bool _isFlipX = false;
    private float _timeAttackPause = 2f;
    private float _actionRate= 2f;
    private float _nextAttackTime = 0f;
    private float _nextActionTime = 0f;
       
    private void Awake()
    {
        _animationObject = GetComponent<AnimationObject>();  
        _moveObject = GetComponent<MoveObject>();
        _enemyLayers = LayerMask.GetMask("Monsters");
    }

    private void FixedUpdate()
    {
        _isGrounded = _moveObject.CheckGround();
    }

    private void Update()
    {
        if(Time.time >= _nextActionTime)
            Action();
    } 

    public void Die()
    {
    }

    private void Action()
    {
        _stateAnim = StatesAnim.Idle;
        _isFlipX = _moveObject.IsFlip;

        if (Input.GetButton(StringHorizontal))
        {
            Run();
        }

        if (_isGrounded && Input.GetButtonDown(StringJump))
        {
            _moveObject.Jump(_jumpForce, ForceMode2D.Impulse);
            _stateAnim = StatesAnim.Jump;
        }

        if (_isGrounded && Input.GetButtonDown(StringAttack) && Time.time >= _nextAttackTime)
        {
            _stateAnim = StatesAnim.Attack;
            Attack();
            _nextActionTime = Time.time + _timeAttackPause / _actionRate;            
        }

        _isFlipX = _moveObject.IsFlip;
        _animationObject.Run(_stateAnim);
    }

    private void Run()
    {           
        Vector3 vector = transform.right * Input.GetAxisRaw(StringHorizontal);
        _moveObject.Move(transform.position + vector, _speed);
        _moveObject.Flip(vector.x, _isFlipX);        
        _stateAnim = StatesAnim.Run;
    }
    
    private void Attack()
    {
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_atttackPoint.transform.position, 
            _attackRange, _enemyLayers);
        
        foreach(Collider2D enemy in hitEnemies)
            enemy.GetComponent<Heallth>()?.TakeDamage(_attackDamage);
    }

    private void OnDrawGizmosSelected()
    {
        if (_atttackPoint == null)
            return;

        Gizmos.DrawWireSphere(_atttackPoint.transform.position, _attackRange);
    }
}
