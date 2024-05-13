using UnityEngine;

[RequireComponent(typeof(AnimationObject), typeof(MoveObject), typeof(CombatPlayer))]
[RequireComponent(typeof(PlayerMover))]

public class Player : MonoBehaviour
{
    private AnimationObject _animationObject;
    private MoveObject _moveObject;
    private CombatPlayer _combatPlayer;
    private PlayerMover _movePlayer;
       
    private void Awake()
    {
        _animationObject = GetComponent<AnimationObject>();
        _moveObject = GetComponent<MoveObject>();
        _combatPlayer = GetComponent<CombatPlayer>();
        _movePlayer = GetComponent<PlayerMover>();
    }

    private void FixedUpdate()
    {
        Action();
    }
     
    public void Die()
    {
    }

    private void Action()
    {
        bool isAttack = _combatPlayer.IsWork;
        StatesAnim stateAnim = StatesAnim.Idle;


        if (_movePlayer.isDownHorizontal && !isAttack)
        {
            _movePlayer.Run();
            stateAnim = StatesAnim.Run;
        }
            
        if (_movePlayer.isDownJump && !isAttack)
        {
            _movePlayer.Jump();            
            stateAnim = StatesAnim.Jump;
        }
     
        if (_movePlayer.isDownAttack)
        {
            _combatPlayer.Attack();

            if (_combatPlayer.IsAttack)
            {
                stateAnim = StatesAnim.Attack;
            }
        }

        _animationObject.Run(stateAnim);
    }

    private void OnEnable()
    {
        if (TryGetComponent(out Heallth heallth))
        {
            heallth.Died += Die;
        }
    }

    private void OnDisable()
    {
        if (TryGetComponent(out Heallth heallth))
        {
            heallth.Died -= Die;
        }
    }
}
