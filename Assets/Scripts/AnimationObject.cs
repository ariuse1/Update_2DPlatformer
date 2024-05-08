using UnityEngine;

[RequireComponent(typeof(Animator), typeof(MoveObject))]

public class AnimationObject : MonoBehaviour
{
    private Animator _animator;     
    private MoveObject _moveObject;
    private int _stateHash;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _moveObject = GetComponent<MoveObject>();
    }

    private void Start()
    {
        _stateHash = Animator.StringToHash("State");
    }

    public void Run(StatesAnim state )
    {
        if (_moveObject.IsOnGround() == false && state != StatesAnim.Jump)
            state = StatesAnim.Jump;

        _animator.SetInteger(_stateHash, (int)state);
    }    
}

public enum StatesAnim
{
    Idle,
    Run,
    Jump,
    Attack
}
