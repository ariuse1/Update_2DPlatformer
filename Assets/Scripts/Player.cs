using UnityEngine;

[RequireComponent(typeof(AnimationObject))]
[RequireComponent(typeof(MoveObject))]
[RequireComponent(typeof(CombatPlayer))]
[RequireComponent(typeof(MovePlayer))]

public class Player : MonoBehaviour, IDie
{
    private const string StringHorizontal = "Horizontal";
    private const string StringJump = "Jump";
    private const string StringAttack = "Fire1";

    private AnimationObject _animationObject;
    private MoveObject _moveObject;
    private CombatPlayer _combatPlayer;
    private MovePlayer _movePlayer;

    private bool _isOnGround = false;
    private bool _isJump = false;   

    private void Awake()
    {
        _animationObject = GetComponent<AnimationObject>();
        _moveObject = GetComponent<MoveObject>();
        _combatPlayer = GetComponent<CombatPlayer>();
        _movePlayer = GetComponent<MovePlayer>();
    }

    private void FixedUpdate()
    {
        _isOnGround = _moveObject.IsOnGround();
    }

    private void Update()
    {
        if (_isOnGround && _isJump)
        {
            _isJump = false;
        }

        Action();
    }

    public void Die()
    {
    }

    private void Action()
    {
        bool isAttack = _combatPlayer.IsWork;
        StatesAnim stateAnim = StatesAnim.Idle;

       
        if (Input.GetButton(StringHorizontal) && !isAttack)
        {
            _movePlayer.Run();
            stateAnim = StatesAnim.Run;
        }

        if (_isOnGround && Input.GetButtonDown(StringJump) && !isAttack)
        {
            _movePlayer.Jump();
            _isJump = true;
            stateAnim = StatesAnim.Jump;
        }

        if (_isOnGround && Input.GetButtonDown(StringAttack))
        {
            _combatPlayer.Attack();

            if (_combatPlayer.IsAttack)
            {
                stateAnim = StatesAnim.Attack;
            }
        }

        _animationObject.Run(stateAnim);
    }
}
